using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace LibrarySystem.Models
{
    public class User
    {
        public static string con1 = "Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(con1);
        [Required]
        public int uid { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }


        public void save(User u)
        {
            con.Open();
            string q = "insert into User1 values ("+u.uid+",'"+u.name+"','"+u.password+"')";
            SqlCommand cmd = new SqlCommand(q,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}