(function($, window) {
    var pageVersion = window.pageVersion = '',
        syncFlag = false,
        rosin_host = 'rosin_debug.com',
        api = {
            getVersion: 'http://' + rosin_host + '/_Rosin_Request/?action=getVersion&cb=versioncb',
            httpPostLog: 'http://' + rosin_host + '/_Rosin_Request/?action=httpPostLog&cb=versioncb'
        };


    $(function () {
        console.info(api.getVersion);

        /*
        $.ajax(api.getVersion, {
            dataType: 'jsonp',
            jsonp: 'cb'
        });
        */
    });

    function versioncb(data) {
        console.info(data);
    }
}(__Rosin_Zepto, this));
