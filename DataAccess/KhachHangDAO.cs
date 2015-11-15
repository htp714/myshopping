using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataAccess
{
    public class KhachHangDAO
    {

        public static string Register(KhachHangDTO dto)
        {
            string id = NextAccountID();
            string query = "insert into shop.account "
                            + "value ('" + id + "','" + dto.UserName + "','" + dto.Password + "','" + dto.BirthDay + "','" + dto.Sex + "','" + dto.Email + "','" + dto.Phone + "','" + dto.Address + "','" + dto.Type + "','" + dto.Status +"')";

            if (DataProvider.ExecuteNonQuery(query))
            {
                return id;
            }
            else
            {
                return "";
            }
        }

        private static string NextAccountID()
        {
            int maxid = 1;
            string query = "select * from shop.account ";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                    int t = int.Parse((string)dt.Rows[i]["ID"]);
                    if (t > maxid)
                    {
                        maxid = t;
                    }


                


            }
            maxid++;
            return maxid.ToString();

        }

        public static string Validate(string userName, string password)
        {
            string rs = "";

            string query = "select * from shop.account "
                            + "where UserName like '" + userName + "' and Password like '" + password + "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                    rs = (string)dt.Rows[i]["ID"];
                    

                

               
            }


            return rs;
        }

        public static KhachHangDTO GetAccountByID(string id)
        {
            KhachHangDTO cs = new KhachHangDTO();

            string query = "select * from shop.account where ID like '" + id + "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
               
                    cs.ID = (string)dt.Rows[i]["ID"];
                    cs.UserName = (string)dt.Rows[i]["UserName"];
                    cs.Password = (string)dt.Rows[i]["Password"];
                    cs.BirthDay = (string)dt.Rows[i]["BirthDay"];
                    cs.Sex = (string)dt.Rows[i]["Sex"];
                    cs.Email = (string)dt.Rows[i]["Email"];
                    cs.Phone = (string)dt.Rows[i]["Phone"];
                    cs.Address = (string)dt.Rows[i]["Address"];
                    cs.Type = (string)dt.Rows[i]["Type"];
                    cs.Status = (string)dt.Rows[i]["Status"];

                
              
            }

            return cs;
        }

        public static void Update(string id, string colname, object value)
        {
            string query = "update  shop.account set " + colname + " = '" + value + "' where ID like '" + id + "'";
                            

            DataProvider.ExecuteNonQuery(query);

        }

        public static List<KhachHangDTO> GetAllCustomer()
        {
            List<KhachHangDTO> rs = new List<KhachHangDTO>();

            string query = "select * from shop.account";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                KhachHangDTO cs = new KhachHangDTO();
               
                    cs.ID = (string)dt.Rows[i]["ID"];
                    cs.UserName = (string)dt.Rows[i]["UserName"];
                    cs.Password = (string)dt.Rows[i]["Password"];
                    cs.BirthDay = (string)dt.Rows[i]["BirthDay"];
                    cs.Sex = (string)dt.Rows[i]["Sex"];
                    cs.Email = (string)dt.Rows[i]["Email"];
                    cs.Phone = (string)dt.Rows[i]["Phone"];
                    cs.Address = (string)dt.Rows[i]["Address"];
                    cs.Type = (string)dt.Rows[i]["Type"];
                    cs.Status = (string)dt.Rows[i]["Status"];

                
                if (cs.Type == "customer")
                {
                    rs.Add(cs);
                }
            }

            return rs;
        }

        
    }
}
