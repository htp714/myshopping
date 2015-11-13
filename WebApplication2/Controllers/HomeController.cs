using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using SendMailCenter;
namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private const int PAGEPRODUCT = 6;


        
        public ActionResult TradeSuccess()
        {
            string id = "";
            
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;

            }

            if (Request["userID"] != null)
            {
                id = Request["userID"];
            }

            string price = Request["Price"];

            BillDTO dto = new BillDTO();
            dto.AccountID = id;
            dto.Price = int.Parse(price);
            dto.Date = DateTime.Now.ToString();
            dto.Validated = "not";

            TradeDAO.SaveBill(dto);

            ViewBag.userID = id;


            var userCookie = new HttpCookie("userID", id);
            userCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(userCookie);


            return View();
        }

        public ActionResult Contact()
        {
            if (Request["name"] != null)
            {
                string name = Request["name"] ;
                string email = Request["email"];
                string subject = Request["subject"];
                string message = Request["message"];

                MailSender sender = new MailSender();
                sender.Send(name, email, subject, message);

                ViewBag.success = "ok";
            }

            return View();
        }

        public ActionResult ChiTiet()
        {
            SanPhamDTO dto = new SanPhamDTO();
            if (Request["productID"] != null)
            {
                dto = DataAccess.SanPhamDAO.GetProductByID(Request["productID"]);

            }

            ViewBag.product = dto;

            return View();
        }

        public ActionResult Index()
        {
            //cookie========================================
            string id = "";
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;
                
            }

            string searchText = "";
            string searchBy = "";
            string searchOption = "";

            if (Request.Cookies.AllKeys.Contains("searchText"))
            {
                searchText = Request.Cookies["searchText"].Value;
                searchBy = Request.Cookies["searchBy"].Value;
                searchOption = Request.Cookies["searchOption"].Value;
            }

            string categoryID = "0";
            if (Request.Cookies.AllKeys.Contains("category"))
            {
                categoryID = Request.Cookies["category"].Value;
            }


            //Request

            if (Request["userID"] != null)
            {
                id = Request["userID"];
            }

            int page = 1;
            
            if (Request["page"] != null)
            {
                 page = int.Parse(Request["page"]);
                
            }


            if (Request["searchText"] != null)
            {
                searchText = Request["searchText"];
                searchBy = Request["searchBy"];
                searchOption = Request["searchOption"];
            }
            
            if (Request["category"] != null)
            {
                categoryID = Request["category"];
            }

            //ViewBag=============================================================
            ViewBag.UserID = id;

            if (id == "2")
            {
                return Redirect("/admin/index");
            }
             
            ViewBag.page = page;
            //=============================
            List<CategoryDTO> Menu = SanPhamDAO.GetAllCategory();
            ViewBag.Category = Menu;
             
            //===========================
            

            List<SanPhamDTO> LSanPham = null;
            if (categoryID != "0")
            {
               LSanPham = DataAccess.SanPhamDAO.GetAllProduct(categoryID);
            }
            else
            {
               LSanPham = DataAccess.SanPhamDAO.GetAllProduct();
            }


            if (searchText != "")
            {
                LSanPham = FiltWithSearchInfor(LSanPham, searchText, searchBy, searchOption);
            }
            

            ViewBag.Pages = LSanPham.Count/6 + 1;


            
            
            //====================================

            ViewBag.SanPham = LSanPham.GetRange((page - 1) * 6, Math.Min(6, (LSanPham.Count) - (page - 1) * 6));


            //send cookie
            //
            var userCookie = new HttpCookie("userID", id);
            userCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(userCookie);

            var categoryCookie = new HttpCookie("category", categoryID);
            userCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(categoryCookie);

            var searchTextCookie = new HttpCookie("searchText", searchText);
            searchTextCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(searchTextCookie);

            var searchByCookie = new HttpCookie("searchBy", searchBy);
            searchByCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(searchByCookie);

            var searchOptionCookie = new HttpCookie("searchOption", searchOption);
            searchOptionCookie.Expires.AddHours(1);
            HttpContext.Response.Cookies.Add(searchOptionCookie);

            return View();
        }

        private List<SanPhamDTO> FiltWithSearchInfor(List<SanPhamDTO> LSanPham, string searchText, string searchBy, string searchOption)
        {
            List<SanPhamDTO> rs = null;

            switch (searchOption)
            {
                case "Allword": { rs = SearchAllWord(LSanPham, searchText, searchBy); break; }
                case "Anyword": { rs = SearchAnyWord(LSanPham, searchText, searchBy);  break; }
            }

            return rs;
        }

        private List<SanPhamDTO> SearchAnyWord(List<SanPhamDTO> LSanPham, string searchText, string searchBy)
        {
            List<SanPhamDTO> rs = new List<SanPhamDTO>();

            switch (searchBy)
            {
                case "name":
                    {
                        foreach (SanPhamDTO sp in LSanPham)
                        {
                            if (sp.Name.ToLower().Contains(searchText.ToLower()))
                            {
                                rs.Add(sp);
                            }
                        }
                        break;
                    }
                case "publisher":
                    {
                        foreach (SanPhamDTO sp in LSanPham)
                        {
                            if (sp.Publisher.ToLower().Contains(searchText.ToLower()))
                            {
                                rs.Add(sp);
                            }
                        }
                        break;
                    }
            }
            return rs;
        }

        private List<SanPhamDTO> SearchAllWord(List<SanPhamDTO> LSanPham, string searchText, string searchBy)
        {
            List<SanPhamDTO> rs = new List<SanPhamDTO>();

            switch(searchBy)
            {
                case "name":
                    {
                        foreach (SanPhamDTO sp in LSanPham)
                        {
                            if (sp.Name.ToLower() == searchText.ToLower())
                            {
                                rs.Add(sp);
                            }
                        }
                        break;
                    }
                case "publisher":
                    {
                        foreach (SanPhamDTO sp in LSanPham)
                        {
                            if (sp.Publisher.ToLower() == searchText.ToLower())
                            {
                                rs.Add(sp);
                            }
                        }
                        break;
                    }
            }
            return rs;
        }

        
        
    }
}