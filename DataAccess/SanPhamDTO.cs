using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SanPhamDTO
    {
        string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        string _Publisher;

        public string Publisher
        {
            get { return _Publisher; }
            set { _Publisher = value; }
        }
        int _Price;

        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        string _Image;

        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        string _ExtraInfor;

        public string ExtraInfor
        {
            get { return _ExtraInfor; }
            set { _ExtraInfor = value; }
        }

        string _Category;

        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
    }
}
