using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser;
using softptoject.App_Classes;


namespace softptoject.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection cnnct = new SqlConnection(
            "Data Source = .; Initial Catalog = Library; Integrated Security = True");//Sql bağlantı stringi vdatabes yolunu gösterir

        List<MaterialC> matList = new List<MaterialC>();//Veritabanında verileri çekeceğimiz liste

        // GET: Home
        public ActionResult AnaSayfa()
        {
            return View();
        }

        public ActionResult Arama()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult Sonuc(string arama, string kriter, string kategori, string language, string type)
        {
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
                    MaterialC mac = new MaterialC();//Veritabanında selecten sonra çetiğimiz bilgileri aktardığımız list tipindeki nesne
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
            else if (kriter == "Anahtar Kelime" && kategori != null && type == null && language == null)//Kategori null olmadığı durumlarda buraya girer
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

        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(string kullanıcınumarası, string sifre)
        {
            
           
            cnnct.Open();
            int control = 0;
            SqlCommand slcgiris = new SqlCommand("Select *FROM Staffs where StafNumber=@snum", cnnct);
            slcgiris.Parameters.AddWithValue("@snum", kullanıcınumarası);
            

            using (SqlDataReader reader = slcgiris.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (kullanıcınumarası.ToString() == reader["StafNumber"].ToString() && sifre.ToString()==reader["Password"].ToString())
                    {
                        if ("Personel" == reader["Role"].ToString())
                        {
                            control = 1;
                        }
                        else
                        {
                            control = 2;
                        }
                    }



                }
            }
            if (control == 1)
            {
                TempData["id"] = kullanıcınumarası;
                return RedirectToAction("Profil", "Personel");
            }
            if (control == 2)
            {
                TempData["id"] = kullanıcınumarası;
                return RedirectToAction("Profil", "Yönetici");
            }
            
            SqlCommand membergiris=new SqlCommand("Select *from Members where SchoolNumber=@snumber",cnnct);
            membergiris.Parameters.AddWithValue("@snumber", kullanıcınumarası);
            int member = 1;

            using (SqlDataReader reader = membergiris.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (kullanıcınumarası == reader["SchoolNumber"].ToString() && sifre == reader["Password"].ToString())
                    {

                        member = 2;
                    }
                }
            }
            if (member==2)
            {
              UyeController.key = Convert.ToInt32(kullanıcınumarası);
                return RedirectToAction("Profil", "Uye");
                
            }

            cnnct.Close();




            return View("Giris");
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Hakkımızda()
        {
            return View();
        }


    }
}

 
   
