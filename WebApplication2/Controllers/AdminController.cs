
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.IO;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult ManageBill()
        {
            string id = "";
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;

            }
            
            if (id == "")
            {
                return Redirect("../Account/DangNhap");
            }

            List<BillDTO> bills = TradeDAO.GetAllBill();

            ViewBag.bills = bills;

            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }

        public ActionResult Result()
        {
            int[] rs = TradeDAO.GetResultEachMonth();

            ViewBag.result = rs;

            return View();
        }
        public ActionResult Index()
        {
            //get cookie
            string id = "";
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;

            }

            if (id == "")
            {
                return Redirect("../Account/DangNhap");
            }

           

            //update
            if (Request["newBirthDay"] != null)
            {
                DataAccess.KhachHangDAO.Update(id, "Birthday", Request["newBirthDay"]);
                DataAccess.KhachHangDAO.Update(id, "Phone", Request["newPhoneNo"]);
                DataAccess.KhachHangDAO.Update(id, "Email", Request["newEmail"]);
                DataAccess.KhachHangDAO.Update(id, "Address", Request["newAddress"]);

                

            }

            DataAccess.KhachHangDTO account = KhachHangDAO.GetAccountByID(id);

            ViewBag.account = account;
            return View();
        }

        public ActionResult ManagerUsers()
        {


            if (Request["banID"] != null)
            {


                DataAccess.KhachHangDAO.Update(Request["banID"], "Status", "banned");

            }

            if (Request["unbanID"] != null)
            {


                DataAccess.KhachHangDAO.Update(Request["unbanID"], "Status", "normal");

            }

            List<KhachHangDTO> customers = KhachHangDAO.GetAllCustomer();
            ViewBag.customer = customers;

            return View();
        }

        public ActionResult AddMatHang()
        {
            SanPhamDTO dto = new SanPhamDTO();
            if (Request["productID"] != null)
            {
                dto = DataAccess.SanPhamDAO.GetProductByID( Request["productID"]);
                
            }
            else
            {
                dto.ID = "";
                dto.Name = "";
                dto.Publisher = "";
                dto.Price = 9999;
                dto.Image = "";
                dto.ExtraInfor = "";
                dto.Category = "1";
            }


            ViewBag.product = dto;

            List<CategoryDTO> category = SanPhamDAO.GetAllCategory();
            ViewBag.category = category;
            return View();
        }

        public ActionResult QuanLyKho(HttpPostedFileBase imageFile)
        {
            if (Request["removeID"] != null)
            {
                DataAccess.SanPhamDAO.Remove(Request["removeID"]);
            }

            

            if (Request["productID"] != null)
            {
                string productID = "";
                if (Request["productID"] == "")
                {
                    SanPhamDTO dto = new SanPhamDTO();
                    dto.ID = Request["productID"];
                    dto.Name = Request["productName"];
                    dto.Publisher = Request["productPublisher"];
                    dto.Price = int.Parse(Request["productPrice"]);
                    dto.Image = "";
                    dto.ExtraInfor = Request["productExtraInfor"];
                    dto.Category = Request["productCategory"];
                    productID = SanPhamDAO.AddProduct(dto);
                }
                else
                {
                    SanPhamDAO.Update(Request["productID"], "Name", Request["productName"]);
                    SanPhamDAO.Update(Request["productID"], "Publisher", Request["productPublisher"]);
                    SanPhamDAO.Update(Request["productID"], "Price",int.Parse( Request["productPrice"]));
                    SanPhamDAO.Update(Request["productID"], "ExtraInfor", Request["productExtraInfor"]);
                    SanPhamDAO.Update(Request["productID"], "Category", Request["productCategory"]);
                    productID = Request["productID"];
                }


                //check for new image
             
                    if (Request.Files["imageFile"] != null)
                    {
                        imageFile = Request.Files["imageFile"];
                        var path = Path.Combine(Server.MapPath("~/Source/products"), "product" + productID + "." + imageFile.ContentType.Substring(6));
                        imageFile.SaveAs(path);

                        SanPhamDAO.Update(productID, "Image", "..//Source//products/product" + productID + "." + imageFile.ContentType.Substring(6));
                    }
                    
            }

            List<SanPhamDTO> products = SanPhamDAO.GetAllProduct();
            ViewBag.products = products;
            return View();
        }

        public ActionResult ThayDoiThongTinTaiKhoan()
        {
            //get cookie
            string id = "";
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;

            }

            if (id == "")
            {
                return Redirect("../Account/DangNhap");
            }

            DataAccess.KhachHangDTO account = KhachHangDAO.GetAccountByID(id);

            ViewBag.account = account;

            return View();
        }


    }
        
}