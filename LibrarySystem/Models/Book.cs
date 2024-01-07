using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.ComponentModel;
namespace LibrarySystem.Models
{
    public class Book
    {
        static string constring = "Data Source=DESKTOP-N4791LV;Initial Catalog=LibDB;Integrated Security=True";
        SqlConnection con = new SqlConnection(constring);
        // [ReadOnly(true)]
         public int B_id { get; set; }
        [Required]
        public string title { get; set;}      //validation of TextBox using Required
        [Required(ErrorMessage="Author Required")]   //Error Message 
        [StringLength(15)]                          //String Length validation
        public string author { get; set;}    
        [Range(2,4000)]                         //Int Validation  2 to 4000 Range
        public int Nop { get; set; }

        //compare two text box

        //[Required]
        //public string pwd { get; set; }
        //[Compare("pwd")]
        //public string cpwd { get; set; }


        // [RegularExpression()]     Email Validation
     //   public string areacode { get; set;}

        // Price Validation

       // [Range(typeof(decimal), "0.00", "50.00")]
        //public decimal price { get; set; }



        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();
            con.Open();
            string query = "select * from Book";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Book m = new Book();
                m.B_id = int.Parse(sdr["B_id"].ToString());
                m.title = sdr["Title"].ToString();
                m.author = sdr["Author"].ToString();
                m.Nop = int.Parse(sdr["Nop"].ToString());
                list.Add(m);
            }
            sdr.Close();
            con.Close();
            return list;
        }
        public void BookInsert(Book t)
        {
            con.Open();
            string query = "insert into Book (Title,Author,Nop) values ('"+t.title+"','"+t.author+"','"+t.Nop+"')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void delete(int i)
        {
            con.Open();
            string query = "delete from Book where B_id='"+i+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public Book SingleBook(int bid)
        {
            Book y = new Book();
            con.Open();
            string q = "select * from Book where B_id='"+bid+"'";
            SqlCommand cmd = new SqlCommand(q,con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            
                y.B_id = int.Parse(sdr["B_id"].ToString());
                y.title = sdr["Title"].ToString();
                y.author = sdr["Author"].ToString();
                y.Nop = int.Parse(sdr["Nop"].ToString());
            sdr.Close();
            con.Close();
            return y;
        }
        public void Update(Book u)
        {
            con.Open();
            string q ="update Book set Title='"+u.title+"' , Author='"+u.author+"' , Nop='"+u.Nop+"' where B_id='"+u.B_id+"'";
            SqlCommand cmd = new SqlCommand(q,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}