using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class childrengarden

    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int phone { get; set; }
        public string description { get; set; }
        public string nom { get; set; }
        public string governate { get; set; }
        public string city { get; set; }
        public string location { get; set; }
        public object photo { get; set; }
        public int phone_number { get; set; }
        public int price { get; set; }
        public string email_garden { get; set; }
        public List<object> likes { get; set; }
        public int count_like { get; set; }
        public int count_dislike { get; set; }
        public List<object> reclamations { get; set; }
        public List<object> activities { get; set; }
        public List<object> parents { get; set; }
        public List<object> events { get; set; }
        public object message { get; set; }
        public List<object> feedbacks { get; set; }
        public DateTime creation_Date { get; set; }
        public List<object> rdvs { get; set; }
    }
}