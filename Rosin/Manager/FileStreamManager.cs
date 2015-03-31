using System;
using System.Collections.Generic;
using System.Text;

using Rosin.Item;

namespace Rosin.Manager
{
    class FileStreamManager
    {
        static private FileStreamManager iFileStreamInstance = null;

        static public FileStreamManager Instance()
        {
            if (iFileStreamInstance == null)
            {
                iFileStreamInstance = new FileStreamManager();
            }

            return iFileStreamInstance;
        }

        List<FileStreamItem> FileStreamList = new List<FileStreamItem>();

        public FileStreamManager()
        { 
          if (iFileStreamInstance != null)
            {
                throw new ArgumentException();
            }  
        }

        private FileStreamItem GetStreamItem(string key, string file)
        {
            FileStreamItem target = null;

            foreach (FileStreamItem item in this.FileStreamList)
            {
                if (item.sKey == key)
                {
                    target = item;
                    break;
                }
            }

            if (target == null)
            {
                target = new FileStreamItem();
                target.sKey = key;
                target.sFileDir = file;

                this.FileStreamList.Add(target);
            }

            return target;
        }

        public void Write(string key, string file, string content)
        {
            FileStreamItem item = this.GetStreamItem(key, file);
            item.Write(content);
        }
    }
}
