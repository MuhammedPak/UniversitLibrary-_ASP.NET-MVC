using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softptoject.App_Classes;

namespace softptoject.Controllers
{
    public class PersonelController : Controller
    {
       List<Personel> staffs=new List<Personel>();
           // GET: Personel
        SqlConnection cnnct = new SqlConnection("Data Source = .; Initial Catalog = Library; Integrated Security = True");
        public ActionResult Profil()
        {
            cnnct.Open();
            int personelid = Convert.ToInt32(TempData["id"]);
            SqlCommand select = new SqlCommand("Select *From Staffs where StafNumber=@snumber ", cnnct);
            select.Parameters.AddWithValue("@snumber", personelid);
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
        public ActionResult KullanıcıEkle()
        {
            Personel staf = new Personel();
            ViewBag.name = staf.Name;
            return View();
        }
       
        [HttpPost]
        public ActionResult KullanıcıEkle(string name,string surname,string adress,string phone,string password,string schoolnumber,string role)
        {
            cnnct.Open();
            int control = 0;
            //Aynı okul numarasına sahip başka kullanıcı var mı diye schoolnumber la kontrol ediyorum
            SqlCommand cntrl=new SqlCommand("Select *from Members where Schoolnumber=@number",cnnct);
            cntrl.Parameters.AddWithValue("@number", schoolnumber);
            using (SqlDataReader reader = cntrl.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (schoolnumber== reader["Schoolnumber"].ToString())
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
                    "Insert into Members (Name,Surname,Adress,Role,SchoolNumber,Password,Phone)VALUES(@name,@surname,@adress,@role,@schoolnumber,@password,@phone)",
                    cnnct);
                addmember.Parameters.AddWithValue("@name", name);
                addmember.Parameters.AddWithValue("@surname", surname);
                addmember.Parameters.AddWithValue("@adress", adress);
                addmember.Parameters.AddWithValue("@role", role);
                addmember.Parameters.AddWithValue("@phone", phone);
                addmember.Parameters.AddWithValue("@password", password);
                addmember.Parameters.AddWithValue("@schoolnumber", schoolnumber);

                addmember.ExecuteNonQuery();

                cnnct.Close();
            }
            return View();
        }

        public ActionResult MateryalEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MateryalEkle(string isim, string yazar, string yayınevi, string ısbn, string dil, string kategori, string basımyılı, string type)
        {cnnct.Open();
            
            int pcontrol = 0;

            //Publishers
            int publisher_id = 0;
            SqlCommand slcpublisher1 = new SqlCommand("Select *from Publishers where PublisherName=@name", cnnct);
            slcpublisher1.Parameters.AddWithValue("@name", yayınevi);
            using (SqlDataReader reader = slcpublisher1.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (yayınevi== reader["PublisherName"].ToString())
                    {
                        pcontrol = 1;
                        publisher_id = Convert.ToInt32(reader["PublisherId"]);
                    }
                    
                }
            }
            if (pcontrol == 0)
            {
                SqlCommand publisher = new SqlCommand("Insert into Publishers(PublisherName)VALUES(@name)", cnnct);
                publisher.Parameters.AddWithValue("@name", yayınevi);
                publisher.ExecuteNonQuery();
                SqlCommand slc = new SqlCommand("Select *from Publishers where PublisherName=@name", cnnct);               
                slc.Parameters.AddWithValue("@name", yayınevi);
          
                using (SqlDataReader reader = slc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        publisher_id= Convert.ToInt32(reader["PublisherId"]);

                    }

                }

            }

            //Categories
            int ccontrol = 0;
            int categoryid=0;
            SqlCommand slccategory = new SqlCommand("Select *from Categories where CategoryName=@name", cnnct);
            slccategory.Parameters.AddWithValue("@name", kategori);
            using (SqlDataReader reader = slccategory.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (kategori == reader["CategoryName"].ToString())
                    {
                        ccontrol = 1;
                        categoryid = Convert.ToInt32(reader["CategoryId"]);
                    }

                }
            }
            if (ccontrol == 0)
            {
            SqlCommand categories=new SqlCommand("Insert into Categories(CategoryName)VALUES(@name)",cnnct);
                categories.Parameters.AddWithValue("@name", kategori);
                categories.ExecuteNonQuery();
       
                SqlCommand slcctgry2 = new SqlCommand("Select *from Categories where CategoryName=@name", cnnct);
                slcctgry2.Parameters.AddWithValue("@name", kategori);

                using (SqlDataReader reader = slcctgry2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categoryid = Convert.ToInt32(reader["CategoryId"]);

                    }

                }

            }


            //Language

            int lcontrol = 0;
            int languageid = 0;
            SqlCommand slclanguage = new SqlCommand("Select *from Languages where LanguageName=@name", cnnct);
            slclanguage.Parameters.AddWithValue("@name",dil);
            using (SqlDataReader reader = slclanguage.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (dil == reader["LanguageName"].ToString())
                    {
                        lcontrol= 1;
                        languageid = Convert.ToInt32(reader["LanguageId"]);
                    }

                }
            }
            if (lcontrol == 0)
            {
                SqlCommand language = new SqlCommand("Insert into Languages(LanguageName)VALUES(@name)", cnnct);
                language.Parameters.AddWithValue("@name", dil); ;
                language.ExecuteNonQuery();
              
                SqlCommand slclanguage2 = new SqlCommand("Select *from Languages where LanguageName=@name", cnnct);
                slclanguage2.Parameters.AddWithValue("@name", dil);

                using (SqlDataReader reader = slclanguage2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        languageid = Convert.ToInt32(reader["LanguageId"]);

                    }

                }

            }

            //TYPE

            int tcontrol = 0;
            int typeid = 0;
            SqlCommand slctype = new SqlCommand("Select *from Types where Typename=@name", cnnct);
            slctype.Parameters.AddWithValue("@name", type);
            using (SqlDataReader reader = slctype.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (type == reader["Typename"].ToString())
                    {
                        tcontrol= 1;
                        typeid = Convert.ToInt32(reader["TypeId"]);
                    }

                }
            }
            if (tcontrol == 0)
            {
                SqlCommand typeınsert = new SqlCommand("Insert into Types(Typename)VALUES(@name)", cnnct);
                typeınsert.Parameters.AddWithValue("@name", type); ;
                typeınsert.ExecuteNonQuery();

                SqlCommand slctype2 = new SqlCommand("Select *from Types where Typename=@name", cnnct);
                slctype2.Parameters.AddWithValue("@name", type);

                using (SqlDataReader reader = slctype2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typeid = Convert.ToInt32(reader["TypeId"]);
                    }
                }
            }


            //AUTHORS
            int acontrol = 0;
            int authorid=0;
            SqlCommand slcauthor = new SqlCommand("Select *from Authors where Name=@name", cnnct);
            slcauthor.Parameters.AddWithValue("@name", yazar);
            using (SqlDataReader reader = slcauthor.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (yazar == reader["Name"].ToString())
                    {
                        acontrol = 1;
                        authorid= Convert.ToInt32(reader["AuthorId"]);
                    }
                }
            }
            if (acontrol == 0)
            {
                SqlCommand author = new SqlCommand("Insert into Authors(Name)VALUES(@name)", cnnct);
                author.Parameters.AddWithValue("@name", yazar); ;
                author.ExecuteNonQuery();

                SqlCommand slcauthor2 = new SqlCommand("Select *from Authors where Name=@name", cnnct);
                slcauthor2.Parameters.AddWithValue("@name", yazar);

                using (SqlDataReader reader = slcauthor2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        authorid = Convert.ToInt32(reader["AuthorId"]);
                    }
                }
            }

            //ISBN CONTROL
            int ısbncontrol = 0;
            SqlCommand slcısbn=new SqlCommand("Select *from Materials where ISBN=@ısbn",cnnct);
            slcısbn.Parameters.AddWithValue("@ısbn", ısbn);
            using (SqlDataReader reader = slcısbn.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (ısbn.ToString() == reader["ISBN"].ToString())
                    {
                        ısbncontrol = 1;
                    }
                }
            }

            int materialid = 0;
            if (ısbncontrol == 1)
            {
                ViewBag.ısbn = 1;
            }
            else
            {
                
                SqlCommand cmmnd = new SqlCommand("INSERT INTO Materials(MaterialName,ISBN,Year,Publishers_PublisherId,Categories_CategoryId,Types_TypeId,Languages_LanguageId)VALUES(@name,@ısbn,@year,@publisherid,@categoryid,@typeid,@languageid)", cnnct);
                cmmnd.Parameters.AddWithValue("@name", isim);
                cmmnd.Parameters.AddWithValue("@ısbn", ısbn);
                cmmnd.Parameters.AddWithValue("@year", basımyılı);
                cmmnd.Parameters.AddWithValue("@publisherid", publisher_id);
                cmmnd.Parameters.AddWithValue("@categoryid", categoryid);
                cmmnd.Parameters.AddWithValue("@languageid", languageid);
                cmmnd.Parameters.AddWithValue("@typeid", typeid);
                cmmnd.ExecuteNonQuery();
            }




            

            SqlCommand slcmaterialıd=new SqlCommand("Select *from Materials where ISBN=@ısbn",cnnct);
            slcmaterialıd.Parameters.AddWithValue("@ısbn" ,ısbn);
            using (SqlDataReader reader = slcmaterialıd.ExecuteReader())
            {
                while (reader.Read())
                {
                    materialid = Convert.ToInt32(reader["MaterialId"]);
                }
            }

            
            SqlCommand ınsertMA = new SqlCommand("Insert into MaterialAuthors(Material_MaterialId,Authors_AuthorId)VALUES(@materialid,@authorid)", cnnct);
            ınsertMA.Parameters.AddWithValue("@materialid", materialid);
            ınsertMA.Parameters.AddWithValue("@authorid", authorid);
            ınsertMA.ExecuteNonQuery();

            return View();



        }






    }
}