using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataAccess
{
    public class SanPhamDAO
    {
        public static List<SanPhamDTO> GetAllProduct()
        {
            List<SanPhamDTO> rs = new List<SanPhamDTO>();

            string query = "select * from shop.product";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SanPhamDTO sp = new SanPhamDTO();
               
                    sp.ID = (string)dt.Rows[i]["ID"];
                    sp.Name = (string)dt.Rows[i]["Name"];
                    sp.Publisher = (string)dt.Rows[i]["Publisher"];
                    sp.Price = (int)dt.Rows[i]["Price"];
                    sp.ExtraInfor = (string)dt.Rows[i]["ExtraInfor"];
                    sp.Category = (string)dt.Rows[i]["Category"];
                    sp.Image = (string)dt.Rows[i]["Image"];
                

                rs.Add(sp);
            }

            return rs;
        }

        public static SanPhamDTO GetProductByID(string id)
        {
            SanPhamDTO rs = new SanPhamDTO();

            string query = "select * from shop.product where ID like '" + id + "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
               
                    rs.ID = (string)dt.Rows[i]["ID"];
                    rs.Name = (string)dt.Rows[i]["Name"];
                    rs.Publisher = (string)dt.Rows[i]["Publisher"];
                    rs.Price = (int)dt.Rows[i]["Price"];
                    rs.ExtraInfor = (string)dt.Rows[i]["ExtraInfor"];
                    rs.Category = (string)dt.Rows[i]["Category"];
                    rs.Image = (string)dt.Rows[i]["Image"];
                

                
            }

            return rs;
        }

        public static List<CategoryDTO> GetAllCategory()
        {
            List<CategoryDTO> rs = new List<CategoryDTO>();

            string query = "select * from shop.category";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CategoryDTO sp = new CategoryDTO();
                
                    sp.ID = (string)dt.Rows[i]["ID"];
                    sp.Name = (string)dt.Rows[i]["Name"];
                    
                

                rs.Add(sp);
            }


            return rs;
        }

        public static List<SanPhamDTO> GetAllProduct(string categoryID)
        {
            List<SanPhamDTO> rs = new List<SanPhamDTO>();

            string query = "select * from shop.product "
                + "where Category like " + categoryID;

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SanPhamDTO sp = new SanPhamDTO();
              
                    sp.ID = (string)dt.Rows[i]["ID"];
                    sp.Name = (string)dt.Rows[i]["Name"];
                    sp.Publisher = (string)dt.Rows[i]["Publisher"];
                    sp.Price = (int)dt.Rows[i]["Price"];
                    sp.ExtraInfor = (string)dt.Rows[i]["ExtraInfor"];
                    sp.Category = (string)dt.Rows[i]["Category"];
                    sp.Image = (string)dt.Rows[i]["Image"];
                

                rs.Add(sp);
            }



            return rs;
        }

        internal static int GetPrice(string ProductID)
        {
            int rs = 0;
            string query = "select * from shop.product where ID like '" + ProductID + "'";

            DataTable dt = DataProvider.ExecuteQuery(query);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                
                    rs = (int)dt.Rows[i]["Price"];
                    
                

                
            }

            return rs;
        }

        public static void Remove(string id)
        {
            ;
            string query = "delete  from shop.product where ID like '" + id + "'" ;

            DataProvider.ExecuteNonQuery(query);
        }


        private static string NextProductID()
        {
            int maxid = 1;
            string query = "select * from shop.product ";

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

        public static string AddProduct(SanPhamDTO dto)
        {
            string id = NextProductID();
            string query = "insert into shop.product "
                            + "value ('" + id + "','" + dto.Name + "','" + dto.Publisher + "','" + dto.Price + "','" + dto.Image + "','" + dto.ExtraInfor + "','" + dto.Category +"')";

            if (DataProvider.ExecuteNonQuery(query))
            {
                return id;
            }
            else
            {
                return "";
            }
        }

        public static void Update(string id, string colname, object value)
        {
            string query = "update  shop.product set " + colname + " = '" + value + "' where ID like '" + id + "'";


            DataProvider.ExecuteNonQuery(query);

        }
    }
}
