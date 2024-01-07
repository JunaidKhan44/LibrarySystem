using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;

namespace LibrarySystem.Controllers
{
    public class ImageTestController : Controller
    {

        static string str="Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        SqlConnection con=new SqlConnection(str);
        [HttpGet]
        public ActionResult Images()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Images(string Usertb,HttpPostedFileBase postedFile)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(postedFile.InputStream);
           bytes= br.ReadBytes(postedFile.ContentLength);
            string contenttype = postedFile.ContentType;

            con.Open();

            string q = "insert into ImageTest values (@un,@img,@typ)";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@un",Usertb);
            cmd.Parameters.AddWithValue("@img", bytes);
            cmd.Parameters.AddWithValue("@typ", contenttype);

            cmd.ExecuteNonQuery();
            con.Close();
            return View();
        }

    }
}
