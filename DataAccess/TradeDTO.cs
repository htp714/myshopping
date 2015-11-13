using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TradeDTO
    {



        string _AccountID;

        public string AccountID
        {
            get { return _AccountID; }
            set { _AccountID = value; }
        }
        string _ProductID;

        public string ProductID
        {
            get { return _ProductID; }
            set { _ProductID = value; }
        }
        int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
        int _TotalPrice;

        public int TotalPrice
        {
            get { return SanPhamDAO.GetPrice(ProductID) * Quantity; }
          
        }
        string _Date;

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        string _Validated;

        

        public string Validated
        {
            get { return _Validated; }
            set { _Validated = value; }
        }



        public TradeDTO(string id, string p)
        {
            // TODO: Complete member initialization
            AccountID = id;
            ProductID = p;
            Quantity = 1;
            Date = DateTime.Now.ToString("dd/mm/yyyy");
            Validated = "not";
        }
    }
}
