using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace softptoject.App_Classes
{
    public class MaterialC
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public  int ISBN { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public string LanguageName { get; set; }
        public string PublisherName { get; set; }
        public string AuthorsName { get; set; }
    }

    

    public class Member
    {
        public int  MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Role { get; set; }
        public string SchoolNumber { get; set; }
        public string Phone { get; set; }
        
        
    }
}