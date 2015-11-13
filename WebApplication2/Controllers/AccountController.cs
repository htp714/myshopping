using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {


        public ActionResult ModifyProfile()
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

        public ActionResult Profile()
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


        public ActionResult History()
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

            List<BillDTO> bills= TradeDAO.GetBillByAccountID(id);

            ViewBag.bills= bills;

            return View();
        }

        public ActionResult DangKi()
        {
            ViewBag.userID = "";
            if (Request["newUserName"] != null)
            {
                KhachHangDTO dto = new KhachHangDTO();
                dto.UserName = Request["newUserName"];
                dto.Password = Request["password"];
                dto.Email = Request["email"];
                dto.Phone = Request["password"];
                dto.Address = Request["address"];
                dto.BirthDay = "12/12/2012";
                dto.Sex = "Male";
                dto.Type = "customer";
                dto.Status = "normal";
                string userID = KhachHangDAO.Register(dto);

                if (userID != "")
                {
                    ViewBag.userID = userID;

                    //send cookie
                    //
                    var userCookie = new HttpCookie("userID", userID);
                    userCookie.Expires.AddHours(1);
                    HttpContext.Response.Cookies.Add(userCookie);

                    return Redirect("../home/index");
                }


            }

           
          

            return View();
        }

        public ActionResult DangNhap()
        {
            ViewBag.userID = "";
            if (Request["userName"] != null)
            {
                string userName = Request["userName"];
                string password = Request["password"];

                string userID = KhachHangDAO.Validate(userName, password);

                if (userID != "" && userID != "2")
                {
                    ViewBag.userID = userID;

                    //send cookie
                    //
                    var userCookie = new HttpCookie("userID", userID);
                    userCookie.Expires.AddHours(1);
                    HttpContext.Response.Cookies.Add(userCookie);

                    Session.Add("cart", new List<TradeDTO>());



                    return Redirect("../home/index");
                }
                else
                {
                    ViewBag.userID = userID;

                    //send cookie
                    //
                    var userCookie = new HttpCookie("userID", userID);
                    userCookie.Expires.AddHours(1);
                    HttpContext.Response.Cookies.Add(userCookie);

                    return Redirect("../Admin/index");
                }
              



            }


            return View();
        }

        public ActionResult GioHang()
        {
            //get cookie
            string id ="";
            if (Request.Cookies.AllKeys.Contains("userID"))
            {
                id = Request.Cookies["userID"].Value;
                
            }

            if(id == "")
            {
                return Redirect("../Account/DangNhap");
            }


            List<TradeDTO> cart = (List<TradeDTO> )Session["cart"];
            if (cart == null)
            {
                cart = new List<TradeDTO>();
                Session.Add("cart", cart);
            }
            
                //update cart
                //add product
            if (Request["productID"] != null)
            {
                TradeDTO trade = new TradeDTO(id, Request["productID"]);
                cart.Add(trade);
                
            }
            //change quantity 
            if (Request["changeIndex"] != null)
            {
                int index = int.Parse(Request["changeIndex"]);
                int quantity = int.Parse(Request["quantity"]);

                cart[index].Quantity = quantity;
            }

            //delete 
            if (Request["deleteIndex"] != null)
            {
                int index = int.Parse(Request["deleteIndex"]);


                cart.RemoveAt(index);
            }
             
            //send
            Session["cart"] = cart;
            ViewBag.cart = cart;

            return View();
        }
    }
}