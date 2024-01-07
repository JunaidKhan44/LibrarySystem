using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    public class UserController : Controller
    {
        public static string con1 = "Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(con1);
        [HttpGet]
        public ActionResult Signup()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Signup(User u1)
        {
            u1.save(u1);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["uid"]!=null&&Session["name"]!=null)
            {
                return RedirectToAction("HomePage");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u2)
        {

            con.Open();
            string q = "select * from User1 where UID='" + u2.uid + "' and Password='" + u2.password + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            if (sdr.HasRows)
            {
                Session["uid"] = sdr["UID"].ToString();
                Session["name"] = sdr["Name"].ToString();
            }
            sdr.Close();
            con.Close();
            return RedirectToAction("WellCome");
        }
        [HttpGet]
        public ActionResult HomePage()
        {
            if (Session["uid"]!=null&&Session["name"]!=null)
            {

                return View();
            }

            return RedirectToAction("Signup");
        }
        [HttpGet]
        public ActionResult WellCome()
        {

            return View();
        }
    }
}
