using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystem.Controllers
{
    public class BookController : Controller
    {
        public string mid;
        public string name;
        public string type;
        public string link;
        public static string constring = "Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        public List<SelectListItem> list2=new List<SelectListItem>();
      public  SqlConnection con = new SqlConnection(constring);
        //
        // GET: /Book/
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShowBook()
        {
            Book b = new Book();
            List<Book> bl = b.GetAllBooks();

            return View(bl);
        }
        [HttpGet]
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Book b)
        {
            if (ModelState.IsValid)
            {
            Book b2 = new Book();
            b2.BookInsert(b);
            return RedirectToAction("ShowBook");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book t = new Book();
            t.delete(id);
            return RedirectToAction("ShowBook");
        }
        [HttpGet]
        public ActionResult Edit(int id1)
        {
            Book call = new Book();
            Book b=call.SingleBook(id1);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Book  b)
        {
             Book b2 = new Book();
             b2.Update(b);
          return RedirectToAction("ShowBook");

        }
        [HttpGet]
        public ActionResult IssueBook()
        {
           
            list2.Add(new SelectListItem{ Text = "Select", Value = "0" });
            ViewBag.lst1 = list2;
            return View();
        }
        [HttpPost]
        public ActionResult IssueBook(string bnts, string txts, int ddl,string txti,string txtr)
        {
            Member m=new Member();
            if (bnts.Equals("Search"))
            {
                m = single(txts);
                ViewBag.n = m.F_NAME;
                ViewBag.t = m.TYPE;
                list2.Add(new SelectListItem { Text = "Select", Value = "0" });
                ViewBag.lst1 = list2;
            }
            else if (bnts.Equals("Book"))
            {
                m = single(txts);
                ViewBag.n = m.F_NAME;
                ViewBag.t = m.TYPE;
               ViewBag.lst1 = list();
            }
            else
            {
                
                    insert(txts, ddl, txti, txtr);
                    ViewBag.lst1 = list();
               
            }
            return View();  
        }
        [HttpGet]
        public ActionResult IssueBookForm()
        {

            ViewBag.lst1 = list();
            return View();
        }
        [HttpGet]
        public ActionResult MemberIssueBook()
        {

            return View();
        }
        [HttpPost]
        public ActionResult MemberIssueBook(string txtn)
        {
            BookController b=new BookController();
            con.Open();
            string actionlink="@Html.ActionLink('Select', 'IssueBookForm', new {  id1=item.M_id })";
            List<BookController> mlist = new List<BookController>();
            string q = "select M_id,fname,type from Member where fname='"+txtn+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                b.mid= sdr["M_id"].ToString();
                b.name = sdr["fname"].ToString();
                b.type = sdr["type"].ToString();
                b.link = actionlink;
                mlist.Add(b);
            }
            sdr.Close();
            con.Close();
            return View(mlist);
        }
        private List<SelectListItem> list()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
             con.Open();
            string q = "select * from Book";
            SqlCommand cmd = new SqlCommand(q,con);
            SqlDataReader sdr=cmd.ExecuteReader();
            while (sdr.Read())
            {
                lst.Add(new SelectListItem {Text=sdr["Title"].ToString(),Value=sdr["B_id"].ToString()});
            }
            sdr.Close();
            con.Close();
            return lst;
        }
        private Member single(string reg)
        {
            Member m1 = new Member();
            con.Open();
            string q = "select fname,type from Member where M_id='"+reg+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            m1.F_NAME = sdr["fname"].ToString();
            m1.TYPE= sdr["type"].ToString();
            con.Close();
            return m1;
        }
        private void insert(string s,int bid,string i,string r)
        {

            int f = 0;
            con.Open();
            string q = "insert into IssueBook values ('"+s+"',"+bid+",'"+i+"','"+r+"',"+f+")";
            SqlCommand cmd = new SqlCommand(q,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
