using CRUD25_10_2026.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CRUD25_10_2026.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Registration()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegisterModel model)
        {
            if (model.c_password == model.password)
            {
                SqlCommand cmd = new SqlCommand("insert into register values(@name,@email,@phone,@gender,@dob,@password,@address)", con);
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@phone", model.phone);
                cmd.Parameters.AddWithValue("@gender", model.gender);
                cmd.Parameters.AddWithValue("@dob", model.dob);
                cmd.Parameters.AddWithValue("@password", model.password);
                cmd.Parameters.AddWithValue("@address", model.address);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if (res > 0)
                {
                    return Content("<script>alert('Registered Successfully');location.href='/home/Login';</script>");
                }
                else
                {

                    return Content("<script>alert('Registered Successfully');location.href='/home/Login';</script>");
                }
            }
            else {
                return Content("<script>alert('password and Confirm password Should be same');location.href='/home/registration';</script>");

            }
        }
        public ActionResult Login()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Login(string password,string email)
        {
            SqlCommand cmd = new SqlCommand("select id,email,password from register;", con);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            
            if(dt.Rows.Count > 0)
            {
               string Email= dt.Rows[0]["email"].ToString();
               string pass= dt.Rows[0]["password"].ToString();
               int id= Convert.ToInt32(dt.Rows[0]["id"]);
                
                if (email.Equals(Email) && password.Equals(pass)) {
                    Session["id"] = id;
                    return Content("<script>alert('Login Successfully');location.href='/home/userDashboard';</script>");
                } else {
                    return Content("<script>alert('Invalid User');location.href='/home/Login';</script>");
                }
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AdminDashboard()
        {
            SqlCommand cmd = new SqlCommand("select * from register;", con);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);
            ViewBag.Message = "Your contact page.";


            return View(dt);
        }
        public ActionResult UserDashboard()
        {
           
                int id = Convert.ToInt32(Session["id"]);
                SqlCommand cmd = new SqlCommand("select * from register where id=@id;", con);
                cmd.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);

                return View(dt);
           
        }
        public ActionResult delete(int? id)
        {

          
            SqlCommand cmd = new SqlCommand("Delete from register where id=@id;", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res= cmd.ExecuteNonQuery();
            con.Close();
            if (res > 0) {
                return Content("<script>alert('User Deleted');location.href='/home/AdminDashboard';</script>");
            }
            else { 
                return Content("<script>alert('Invalid User');location.href='/home/AdminDashboard';</script>");
            }

        }
        public ActionResult Update(int? id)
        {

           
            SqlCommand cmd = new SqlCommand("select * from register where id=@id;", con);
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            ad.Fill(dt);

            return View(dt);

        }
        [HttpPost]

        public ActionResult Update(RegisterModel model)
        {


            if (model.c_password == model.password)
            {
                SqlCommand cmd = new SqlCommand("update register set name=@name,email=@email,phone=@phone,gender=@gender,dob=@dob,password=@password,address=@address where id=@id", con);
                cmd.Parameters.AddWithValue("@id", model.id);
                cmd.Parameters.AddWithValue("@name", model.name);
                cmd.Parameters.AddWithValue("@email", model.email);
                cmd.Parameters.AddWithValue("@phone", model.phone);
                cmd.Parameters.AddWithValue("@gender", model.gender);
                cmd.Parameters.AddWithValue("@dob", model.dob);
                cmd.Parameters.AddWithValue("@password", model.password);
                cmd.Parameters.AddWithValue("@address", model.address);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                if (res > 0)
                {
                    return Content("<script>alert('updated Successfully');location.href='/home/AdminDashboard';</script>");
                }
                else
                {

                    return Content("<script>alert('Invalid userid');location.href='/home/AdminDashboard';</script>");
                }
            }
            else
            {
                return Content("<script>alert('password and Confirm password Should be same');location.href='/home/AdminDashboard';</script>");

            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        } 

        }
}