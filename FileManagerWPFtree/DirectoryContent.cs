using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FileManagerWPFtree
{
    public class DirectoryContent
    {
        private BitmapImage _image;
        private String _name;
        private long? _totalSize;
        private long? _freeSpace;
        private DateTime? _lastWriteTime;

        public DirectoryContent()
        {
            Image = null;
            Name = null;
            TotalSize = null;
            FreeSpace = null;
            _lastWriteTime = null;
        }

        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long? TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; }
        }

        public long? FreeSpace
        {
            get { return _freeSpace; }
            set { _freeSpace = value; }
        }

        public DateTime? LastWriteTime
        {
            get { return _lastWriteTime; }
            set { _lastWriteTime = value; }
        }
    }
}
