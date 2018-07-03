using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using softptoject.App_Classes;

namespace softptoject.Controllers
{
    
    public class UyeController : Controller
    {
        public static int key;
        SqlConnection cnnct = new SqlConnection(
            "Data Source = .; Initial Catalog = Library; Integrated Security = True");
        List<Members> uyeler = new List<Members>();
        // GET: Uye
        List<MaterialC> matList=new List<MaterialC>();

       int userid;
        public ActionResult Profil()
        {cnnct.Open();
            userid = key;
            SqlCommand select = new SqlCommand("Select *From Members where SchoolNumber=@snumber ", cnnct);
            select.Parameters.AddWithValue("@snumber", userid);
            SqlDataReader reader = select.ExecuteReader();
            while (reader.Read())
            {
                Members uye=new Members();
                uye.Name =(reader["Name"]).ToString();
                uye.Surname = (reader["Surname"]).ToString();
                uye.Adress = (reader["Adress"]).ToString();
                uye.Phone = (reader["Phone"]).ToString();
                uye.MemberId = Convert.ToInt32(reader["MemberId"]);
                uye.SchoolNumber = (reader["SchoolNumber"]).ToString();

                uyeler.Add(uye);

                ViewData["Name"] = uye.Name;
                ViewData["Surname"] = uye.Surname;
                ViewData["Adress"] = uye.Adress;
                ViewData["Phone"] = uye.Phone;
                ViewData["MemberId"] = uye.MemberId;
                ViewData["SchoolNumber"] =key;
            }
            
            

            cnnct.Close();
            
            return View();
        }
        public ActionResult HesapGecmisi()
        {
            ViewData["SchoolNumber"] = key;
            return View(); 
        }
        public ActionResult Reserve()
        {
            ViewData["SchoolNumber"] = key;
            return View();
        }
        
        public ActionResult Istek()
        {
            ViewData["SchoolNumber"] = key;
            return View();
        }
        [HttpPost]
        public ActionResult Istek(string name, string author, string date)
        {
            ViewData["SchoolNumber"] = key;
            SqlCommand addmember = new SqlCommand(
                "Insert into Members (Name,Surname,Adress)VALUES(@name,@surname,@adress)",
                cnnct);
            addmember.Parameters.AddWithValue("@MaterialName", name);
            addmember.Parameters.AddWithValue("@AuthorName", author);
            addmember.Parameters.AddWithValue("@RequestDate", DateTime.Now);
            addmember.Parameters.AddWithValue("Member_MemberId", userid);
            return View();
        }

        public ActionResult ReserveYap()
        {
            ViewData["SchoolNumber"] = key;
            return View();
        }
        public ActionResult Sonuc(string arama, string kriter, string kategori, string language, string type)
        {
            ViewData["SchoolNumber"] = key;
            if (kriter == "Anahtar Kelime" && kategori == null && language == null && type == null)
            {

                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' or Authors.Name LIKE '%" + arama +
                              "%' or Categories.CategoryName LIKE'%" + arama + "%' or Publishers.PublisherName LIKE'%" +
                              arama + "%' or Languages.LanguageName LIKE'%" + arama + "%' or Types.TypeName LIKE'%" +
                              arama + "%')\n";


                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }

            }
            else if (kriter == "Anahtar Kelime" && kategori != null && type == null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Categories.CategoryName LIKE'%" + kategori +
                              "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Anahtar Kelime" && kategori != null && type != null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Categories.CategoryName LIKE'%" + kategori +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Anahtar Kelime" && kategori != null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Categories.CategoryName LIKE'%" + kategori +
                              "%'  and Types.TypeName LIKE'%" + type +
                              "%' and Languages.LanguageName LIKE '%" + language + "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Anahtar Kelime" && kategori == null && type == null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Languages.LanguageName LIKE '%" + language +
                              "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Anahtar Kelime" && kategori == null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Languages.LanguageName LIKE '%" + language +
                              "%' \n" +
                              "and Types.TypeName LIKE '%" + type + "%' ";




                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Anahtar Kelime" && kategori == null && type == null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%')and Types.TypeName LIKE '%" + type + "%' ";





                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori == null && type != null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%'and Types.TypeName LIKE'%" + type +
                              "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori != null && type == null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori != null && type != null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori != null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%'and Languages.LanguageName LIKE'%" + language +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori == null && type == null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }

            else if (kriter == "Başlık" && kategori == null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%'and Language.LanguageName LIKE'%" + language +
                              "%'";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Başlık" && kategori == null && type == null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Materials.MaterialName LIKE'%" + arama +
                              "%'and Language.LanguageName LIKE'%" + language +
                              "%'";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            //YAZARA GÖRE ARAMA
            else if (kriter == "Yazar" && kategori == null && type != null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%'and Types.TypeName LIKE'%" + type +
                              "%' \n";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Yazar" && kategori != null && type == null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Yazar" && kategori != null && type != null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Yazar" && kategori != null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%'and Categories.CategoryName LIKE'%" + kategori +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%'and Languages.LanguageName LIKE'%" + language +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Yazar" && kategori == null && type == null && language == null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%' ";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }

            else if (kriter == "Yazar" && kategori == null && type != null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name'%" + arama +
                              "%' and Types.TypeName LIKE'%" + type +
                              "%'and Language.LanguageName LIKE'%" + language +
                              "%'";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }
            else if (kriter == "Yazar" && kategori == null && type == null && language != null)
            {
                string ssql = "select * from Materials\n" +
                              "inner JOIN Categories ON Materials.Categories_CategoryId = Categories.CategoryId\n" +
                              "inner JOIN Languages ON Materials.Languages_LanguageId = Languages.LanguageId\n" +
                              "inner JOIN Types ON Materials.Types_TypeId = Types.TypeId\n" +
                              "inner JOIN Publishers ON Materials.Publishers_PublisherId=Publishers.PublisherId\n" +
                              "inner JOIN MaterialAuthors ON Materials.MaterialId = MaterialAuthors.Material_MaterialId\n" +
                              "inner JOIN Authors ON Authors.AuthorId = MaterialAuthors.Authors_AuthorId\n" +
                              "Where (Materials.MaterialName LIKE '%" + arama + "%' \n" +
                              "or Authors.Name LIKE '%" + arama + "%' \n" +
                              " or Categories.CategoryName LIKE'%" + arama + "%' \n" +
                              "or Publishers.PublisherName LIKE'%" + arama + "%' \n" +
                              " or Languages.LanguageName LIKE'%" + arama + "%' \n" +
                              "or Types.TypeName LIKE'%" + arama + "%') and Authors.Name LIKE'%" + arama +
                              "%'and Language.LanguageName LIKE'%" + language +
                              "%'";



                cnnct.Open();
                SqlCommand cmmnd = new SqlCommand(ssql, cnnct);
                SqlDataReader sql = cmmnd.ExecuteReader();
                while (sql.Read())
                {
                    MaterialC mac = new MaterialC();
                    mac.MaterialId = Convert.ToInt32((sql["MaterialId"]));
                    mac.MaterialName = (sql["MaterialName"]).ToString();
                    mac.ISBN = Convert.ToInt32(sql["ISBN"]);
                    mac.AuthorsName = sql["Name"].ToString();
                    mac.CategoryName = sql["CategoryName"].ToString();
                    mac.LanguageName = sql["LanguageName"].ToString();
                    mac.PublisherName = sql["PublisherName"].ToString();
                    mac.TypeName = sql["TypeName"].ToString();

                    matList.Add(mac);
                }
            }

            cnnct.Close();
            return View(matList);
        }
        public ActionResult Arama()
        {
            ViewData["SchoolNumber"] = key;

            return View();
        }
       
        public ActionResult IstekYap()
        {
            ViewData["SchoolNumber"] = key;
            return View();
        }
        


    }

    
}