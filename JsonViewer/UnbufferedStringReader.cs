using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EPocalipse.Json.Viewer
{
    [Serializable]
    public class UnbufferedStringReader : TextReader
    {
        // Fields
        private int _length;
        private int _pos;
        private string _s;

        // Methods
        public UnbufferedStringReader(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            this._s = s;
            this._length = (s == null) ? 0 : s.Length;
        }

        public override void Close()
        {
            this.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            this._s = null;
            this._pos = 0;
            this._length = 0;
            base.Dispose(disposing);
        }

        public override int Peek()
        {
            if (this._s == null)
            {
                throw new Exception("object closed");
            }
            if (this._pos == this._length)
            {
                return -1;
            }
            return this._s[this._pos];
        }

        public override int Read()
        {
            if (this._s == null)
            {
                throw new Exception("object closed");
            }
            if (this._pos == this._length)
            {
                return -1;
            }
            return this._s[this._pos++];
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            if ((buffer.Length - index) < count)
            {
                throw new ArgumentException("invalid offset length");
            }
            if (this._s == null)
            {
                throw new Exception("object closed");
            }
            int num = this._length - this._pos;
            if (num > 0)
            {
                if (num > count)
                {
                    num = count;
                }
                this._s.CopyTo(this._pos, buffer, index, num);
                this._pos += num;
            }
            return num;
        }

        public override string ReadLine()
        {
            if (this._s == null)
            {
                throw new Exception("object closed");
            }
            int num = this._pos;
            while (num < this._length)
            {
                char ch = this._s[num];
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        {
                            string text = this._s.Substring(this._pos, num - this._pos);
                            this._pos = num + 1;
                            if (((ch == '\r') && (this._pos < this._length)) && (this._s[this._pos] == '\n'))
                            {
                                this._pos++;
                            }
                            return text;
                        }
                }
                num++;
            }
            if (num > this._pos)
            {
                string text2 = this._s.Substring(this._pos, num - this._pos);
                this._pos = num;
                return text2;
            }
            return null;
        }

        public override string ReadToEnd()
        {
            string text;
            if (this._s == null)
            {
                throw new Exception("object closed");
            }
            if (this._pos == 0)
            {
                text = this._s;
            }
            else
            {
                text = this._s.Substring(this._pos, this._length - this._pos);
            }
            this._pos = this._length;
            return text;
        }

        public int Position
        {
            get
            {
                return _pos;
            }
        }
    }

}
