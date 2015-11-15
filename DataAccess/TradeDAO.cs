using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataAccess
{
    public class TradeDAO
    {




        public static List<TradeDTO> GetAllTradeWithAccountID(string id)
        {
            List<TradeDTO> rs = new List<TradeDTO>();

            string query = "select * from db3c04c35a9c6b45918ba3a551005e16ee.trade where AccountID like '" +id+ "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TradeDTO trade = new TradeDTO((string)dt.Rows[i]["AccountID"], (string)dt.Rows[i]["ProductID"]);

                trade.Quantity = (int)dt.Rows[i]["Quantity"];
                trade.Validated = (string)dt.Rows[i]["Validated"];
                trade.Date = (string)dt.Rows[i]["Date"];

                rs.Add(trade);
            }

            return rs;
        }

        public static void SaveBill(BillDTO dto)
        {
           
            string query = "insert into db3c04c35a9c6b45918ba3a551005e16ee.bill "
                            + "value ('" + dto.AccountID + "','" + dto.Price + "','" + dto.Date  + "','" + dto.Validated + "')";

            if (DataProvider.ExecuteNonQuery(query))
            {
               
            }
            else
            {
                
            }
        }

        public static List<BillDTO> GetBillByAccountID(string ID)
        {
            List<BillDTO> kq = new List<BillDTO>();

            string query = "select * from db3c04c35a9c6b45918ba3a551005e16ee.bill where AccountID like '" + ID + "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BillDTO bill = new BillDTO();

                bill.AccountID = (string)dt.Rows[i]["AccountID"];
                bill.Price = (int)dt.Rows[i]["Price"];
                bill.Date = (string)dt.Rows[i]["Date"];
                bill.Validated = (string)dt.Rows[i]["Validated"];

               kq.Add(bill);
            }

            return kq;
        }


        public static List<BillDTO> GetAllBill()
        {
            List<BillDTO> kq = new List<BillDTO>();

            string query = "select * from db3c04c35a9c6b45918ba3a551005e16ee.bill";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BillDTO bill = new BillDTO();

                bill.AccountID = (string)dt.Rows[i]["AccountID"];
                bill.Price = (int)dt.Rows[i]["Price"];
                bill.Date = (string)dt.Rows[i]["Date"];
                bill.Validated = (string)dt.Rows[i]["Validated"];

               kq.Add(bill);
            }

            return kq;
        }

        public static int[] GetResultEachMonth()
        {
            int[] rs = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
           

            string query = "select * from db3c04c35a9c6b45918ba3a551005e16ee.bill";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

               
                int price = (int)dt.Rows[i]["Price"];
                string date = (string)dt.Rows[i]["Date"];

                DateTime d = DateTime.Parse(date);

                int m = d.Month;

                rs[m - 1]+= price;
            }

            return rs;
        }


    }
}
