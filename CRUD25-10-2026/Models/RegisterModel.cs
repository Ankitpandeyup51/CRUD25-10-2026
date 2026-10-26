using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD25_10_2026.Models
{
    public class RegisterModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string c_password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime dob   { get; set; }
        public string gender { get; set; }

    }
}