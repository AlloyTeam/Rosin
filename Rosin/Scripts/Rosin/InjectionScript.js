/**
 * Created by yunshengli on 2014/10/22.
 */
(function(window) {
    var LOG_LEVELS = ["DEBUG", "LOG", "INFO", "WARN", "ERROR"], //日志级别
        //TODO 是否可以做成队列阈值和倒计时时间由UI配置？
        THRESHOLD = 200, // 触发日志上传的阈值，设置大一点，避免高频的请求导致并发问题
        TIMER = 1000, // 倒计时，间隔发送时间
        URL = location.protocol + "//__rosin__.qq.com",
        KEY = +new Date() + '_' + parseInt(Math.random() * 1e8); // 每个页面生成一个唯一key

    // https的请求，使用另外的通信协议
    if(location.protocol === "https:") {
        URL = "https://" + location.host + '/?__rosin__';
    }

    /**
     * 上传任务队列
     * 队列长度达到阈值触发POST日志
     * @type {{_queueArr: Array, add: add, _post: _post}}
     */
    var queue = {
        _queueArr: [], //数组模拟队列
        add: function() {
            Array.prototype.push.apply(this._queueArr, arguments);
            clock.start();

            //队列达到阈值就触发上传
            if (this._queueArr.length >= THRESHOLD) {
                this._post(this._queueArr.splice(0, this._queueArr.length));
                return;
            }
        },
        _post: function(logArr) {
            var headers = {};
            var setHeader = function(name, value) {
                headers[name.toLowerCase()] = [name, value]
            };
            var xhr = new XMLHttpRequest();
            var nativeSetHeader = xhr.setRequestHeader;

            setHeader('Accept', '*/*');

            setHeader('Content-Type', 'application/x-www-form-urlencoded');

            xhr.setRequestHeader = setHeader;
            xhr.onreadystatechange = function(){}; // do nothing
            xhr.open('POST', URL, true);
            // chrome 控制台会看到一个报错
            // A wildcard '*' cannot be used in the 'Access-Control-Allow-Origin' header when the credentials flag is true.
            // xhr.withCredentials = true;

            for (name in headers) nativeSetHeader.apply(xhr, headers[name]);
            
            xhr.send(JSON.stringify(logArr));
        }
    },

    /**
     * 自定义日志对象
     * @type {{DELIMITER: string, _record: _record, debug: debug, log: log, info: info, warn: warn, error: error}}
     */
    FConsole = {
        DELIMITER: " ", //分隔符
        JSON_TAG_LEFT: '\uFFFE', // 用于标识json对象字符串的等宽字符
        JSON_TAG_RIGHT: '\uFEFF',
        _record: function(logLevel) {
            var log = {
                "key": KEY,
                "level": logLevel, //日志级别
                "time": new Date().getTime() //日志时间，时间戳
                // "url"  : window.location.href //记录日志的URL
            },
            content = ""; //日志内容

            for (var i = 1; i < arguments.length; i++) {
                if (Array.isArray(arguments[i])) {
                    content += (i === 1 ? "" : FConsole.DELIMITER) + FConsole.JSON_TAG_LEFT + "[" + arguments[i] + "]" + FConsole.JSON_TAG_RIGHT;
                } else if (typeof arguments[i] === "object") {
                    content += (i === 1 ? "" : FConsole.DELIMITER) + FConsole.JSON_TAG_LEFT + JSON.stringify(arguments[i]) + FConsole.JSON_TAG_RIGHT;
                } else {
                    content += (i === 1 ? "" : FConsole.DELIMITER) + arguments[i];
                }
            }
            log["content"] = content;

            //日志记录加入队列
            queue.add(log)
        },
        debug: function() {
            Array.prototype.unshift.call(arguments, LOG_LEVELS[0]);
            FConsole._record.apply(FConsole, arguments);
        },
        log: function() {
            Array.prototype.unshift.call(arguments, LOG_LEVELS[1]);
            FConsole._record.apply(FConsole, arguments);
        },
        info: function() {
            Array.prototype.unshift.call(arguments, LOG_LEVELS[2]);
            FConsole._record.apply(FConsole, arguments);
        },
        warn: function() {
            Array.prototype.unshift.call(arguments, LOG_LEVELS[3]);
            FConsole._record.apply(FConsole, arguments);
        },
        error: function() {
            Array.prototype.unshift.call(arguments, LOG_LEVELS[4]);
            FConsole._record.apply(FConsole, arguments);
        }
    },

    /**
     * 全局定时器，监听队列
     * @type {{timeout: null, start: Function, clear: Function}}
     */
    clock = {
        timeout: null,
        start: function() {
            var self = this;
            if (!this.timeout) this.timeout = setInterval(function() {
                if (queue._queueArr.length > 0) {
                    queue._post(queue._queueArr.splice(0, queue._queueArr.length));
                }
            }, TIMER);
        },
        clear: function() {
            clearTimeout(this.timeout);
            this.timeout = null;
        }
    };


    function _extendObj(obj) {
        if (typeof obj !== 'object') return obj;
        var source, prop;
        for (var i = 1, length = arguments.length; i < length; i++) {
            source = arguments[i];
            for (prop in source) {
                if (Object.prototype.hasOwnProperty.call(source, prop)) {
                    // 不覆盖原方法执行，只是加个壳
                    (function(obj, prop) {
                        if (typeof obj[prop] === "function") {
                            var oldFun = obj[prop].bind(obj);
                            obj[prop] = function() {
                                source[prop].apply(source, arguments);
                                // oldFun.apply(obj, arguments);
                                oldFun(arguments[0]);
                            };
                        } else {
                            obj[prop] = source[prop];
                        }
                    })(obj, prop);
                }
            }
        }
        return obj;
    };

    //将自定义的日志API覆盖到原生console中
    _extendObj(window.console, FConsole);

    // script error监听解析
    (function(win) {
        var scriptErrorHander = function(errorMsg, url, lineNumber) {
            var errorObj = {};
            errorObj.errorMsg = errorMsg;
            errorObj.url = url || '';
            errorObj.lineNumber = lineNumber || 0;

            FConsole.error('script error,', errorObj);
        };

        if (win.addEventListener) {
            win.addEventListener('error', function(e){
                scriptErrorHander(e.message, e.filename, e.lineno);
            }, false);
        } else if (win.attachEvent) {
            win.attachEvent('error', function(e){
                scriptErrorHander(e.message, e.filename, e.lineno);
            });
        } else {
            win.onerror = scriptErrorHander;
        }
    })(window);

})(window)