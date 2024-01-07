using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LibrarySystem.Models
{
    public class Member
    {
        public string M_ID { get; set; }
        public string F_NAME { get; set; }
        public string L_NAME { get; set; }
        public char GENDER { get; set; }
        public string TYPE { get; set; }
        public bool PAKISTANI { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
     static  string constring = "Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(constring);

         public void AddMember(Member m1)
         {
             con.Open();
             string query = "insert into Member values ('"+m1.M_ID+"','"+m1.F_NAME+"','"+m1.L_NAME+"','"+m1.GENDER+"','"+m1.TYPE+"','"+m1.PAKISTANI+"','"+m1.EMAIL+"','"+m1.PASSWORD+"')";
             SqlCommand cmd = new SqlCommand(query,con);
             cmd.ExecuteNonQuery();
         con.Close();
          }

         public List<Member> AllMembers()
         {
             List<Member> list = new List<Member>();
             con.Open();
             string query = "select * from Member";
             SqlCommand cmd = new SqlCommand(query, con);
             SqlDataReader sdr=cmd.ExecuteReader();
             while (sdr.Read())
             {
                 Member m = new Member();
                 m.M_ID = sdr["M_id"].ToString();
                 m.F_NAME = sdr["fname"].ToString();
                 m.L_NAME = sdr["lname"].ToString();
                 m.GENDER = char.Parse(sdr["gender"].ToString());
                 m.TYPE = sdr["type"].ToString();
                 m.PAKISTANI = bool.Parse(sdr["pakistani?"].ToString());
                 m.EMAIL = sdr["Email"].ToString();

                 list.Add(m);
             }
             sdr.Close();
             con.Close();
             return list;

         }

      public List<Member> searchresult(string gender, string type)
      {
          List<Member> list = new List<Member>();
          
             con.Open();
             string query = "select * from Member where type='"+type+"' and gender='"+gender+"'";
             SqlCommand cmd = new SqlCommand(query,con);
             SqlDataReader sdr = cmd.ExecuteReader();
             while (sdr.Read())
             {
                 Member m = new Member();
                 m.M_ID = sdr["M_id"].ToString();
                 m.F_NAME = sdr["fname"].ToString();
                 m.L_NAME = sdr["lname"].ToString();
                 m.GENDER = char.Parse(sdr["gender"].ToString());
                 m.TYPE = sdr["type"].ToString();
                 m.PAKISTANI = bool.Parse(sdr["pakistani?"].ToString());
                 m.EMAIL = sdr["Email"].ToString();
                 list.Add(m);
             }
             sdr.Close();
         con.Close();
         return list;
      }
    } 
}
public enum type
{
    Student,Teacher,Admin
};