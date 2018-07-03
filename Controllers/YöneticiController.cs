using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softptoject.App_Classes;

namespace softptoject.Controllers
{
    public class YöneticiController : Controller
    {
        SqlConnection cnnct = new SqlConnection("Data Source = .; Initial Catalog = Library; Integrated Security = True");
        
        List<Personel> staffs=new List<Personel>();
        // GET: Yönetici
        public ActionResult Profil()
        {
            cnnct.Open();
            int personelid = Convert.ToInt32(TempData["id"]);
            SqlCommand select = new SqlCommand("Select *From Staffs where StafNumber=@snum ", cnnct);
            select.Parameters.AddWithValue("@snum", personelid);
            SqlDataReader reader = select.ExecuteReader();
            while (reader.Read())
            {
                Personel staf = new Personel();
                staf.Name = (reader["Name"]).ToString();
                staf.Surname = (reader["Surname"]).ToString();
                staf.Adress = (reader["Adress"]).ToString();
                staf.Phone = (reader["Phone"]).ToString();


                staffs.Add(staf);

                ViewData["Name"] = staf.Name;
                ViewData["Surname"] = staf.Surname;
                ViewData["Adress"] = staf.Adress;
                ViewData["Phone"] = staf.Phone;

            }
            return View();
        }

        public ActionResult GörevliEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GörevliEkle(string isim,string soyisim,string görevliid,string adres,string telefon,string sifre)
        {
            cnnct.Open();
            int control = 0;
            //Aynı okul numarasına sahip başka kullanıcı var mı diye schoolnumber la kontrol ediyorum
            SqlCommand cntrl = new SqlCommand("Select *from Staffs where StafNumber=@snumber2", cnnct);
            cntrl.Parameters.AddWithValue("@snumber2", görevliid);
            using (SqlDataReader reader = cntrl.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (görevliid == reader["Stafnumber"].ToString())
                    {

                        control = 1;
                    }

                }
            }
            if (control == 1)
            {
                ViewBag.control = false;
            }
            else
            {


                SqlCommand addmember = new SqlCommand(
                    "Insert into Staffs (Name,Surname,Adress,Role,StafNumber,Password,Phone)VALUES(@name,@surname,@adress,@role,@snum,@password,@phone)",
                    cnnct);
                addmember.Parameters.AddWithValue("@name", isim);
                addmember.Parameters.AddWithValue("@surname", soyisim);
                addmember.Parameters.AddWithValue("@adress", adres);
                addmember.Parameters.AddWithValue("@role", "Personel");
                addmember.Parameters.AddWithValue("@phone", telefon);
                addmember.Parameters.AddWithValue("@password", sifre);
                addmember.Parameters.AddWithValue("@snum", görevliid);

                addmember.ExecuteNonQuery();

                cnnct.Close();
            }
            
            return View();
        }
    }
}