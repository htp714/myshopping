using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BillDTO
    {
        string _AccountID;

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        int _Price;

        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        string _Date;
        public string Validated;

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
    }
}
