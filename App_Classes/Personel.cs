using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace softptoject.App_Classes
{
    public class Personel
    {
        public int PersonelId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public static implicit operator List<object>(Personel v)
        {
            throw new NotImplementedException();
        }
    }
}