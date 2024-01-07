using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibrarySystem.Models;
using System.Data.SqlClient;

namespace LibrarySystem.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/
        [HttpGet]
        public ActionResult MemberEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberEntry(Member m)
        {
            Member md = new Member();
            m.M_ID = m.EMAIL.Split('@')[0];
            md.AddMember(m);
            return View(m);
        }
        [HttpGet]
        public ActionResult AllMemberSearch()
        {
            Member obj = new Member();
          List<Member>  ml=obj.AllMembers();
            return View(ml);
        }
        [HttpGet]
        public ActionResult search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult search(string genderrb, string typedd)
        {
            Member md = new Member();
            List<Member> list1 = md.searchresult(genderrb, typedd);
            return View(list1);
        }

     
    }
}
