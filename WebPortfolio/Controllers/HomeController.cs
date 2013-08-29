using System;
using System.Collections.Generic;
using System.Linq;
//using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPortfolio.Core.Repositories;
using System.Configuration;
using System.Data.SqlTypes;
using WebPortfolio.Models.Entities;
//using WebPortfolio.Models.Entities;
//using System.Data.Entity;

namespace WebPortfolio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //public IWPRepository<UserProfile> userprofilerepository { get; set; }
        //public IWPRepository<File> filesrepository { get; set; }
                
        public ActionResult Index()
        {
           
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application";
            
            return  View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        string DtsConection = "Data Source=rkrdo-pc;Initial Catalog=WebPortfolio; Trusted_Connection=Yes;";



       
//        public void getphoto(int id)
//        {
           
//            SqlConnection Con = new SqlConnection(DtsConection);
//           // SqlConnection conn = GetConnection();
//            Con.Open();
//            SqlTransaction trn = Con.BeginTransaction();
//            try
//            {
//                SqlCommand cmd = new SqlCommand(
//                    @"SELECT Size, ContentType, PhotoProfile.PathName() as pathhh,
//                        GET_FILESTREAM_TRANSACTION_CONTEXT ()
//                    FROM [File], [UserProfile]
//                    WHERE Id = " + id, Con, trn);
//                //SqlParameter paramFilename = new SqlParameter(
//                          //  @"fileName", SqlDbType.VarChar, 256);
//               // paramFilename.Value = fileName;
//               // cmd.Parameters.Add(paramFilename);

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (false == reader.Read())
//                    {
//                        reader.Close();
//                        trn.Dispose();
//                        Con.Dispose();
//                        trn = null;
//                        Con = null;
//                        //file = null;
//                       // return false;
//                    }

//                   //string contentDisposition = reader.GetString(0);
//                    string contentType = reader.GetString(1);
//                    string filepath = reader.GetString(2);
//                   // string contentCoding = reader.IsDBNull(2) ? null : reader.GetString(2);
//                    long contentLength = reader.GetInt32(0);
//                  //  string path = reader.GetString(4);
//                    byte[] context = reader.GetSqlBytes(3).Buffer;

//                    SqlFileStream sfs = new SqlFileStream(filepath, context, System.IO.FileAccess.Read);
//                    //file = new FileDownloadModel
//                    //{
//                    //    FileName = contentDisposition,
//                    //    ContentCoding = contentCoding,
//                    //    ContentType = contentType,
//                    //    ContentLength = contentLength,
//                    //    Content = new MvcResultSqlFileStream
//                    //    {
//                    //        SqlStream = new SqlFileStream(path, context, FileAccess.Read),
//                    //        Connection = conn,
//                    //        Transaction = trn
//                    //    }
//                    //};
//                    byte[] buffer = new byte[(int)sfs.Length];
//                    sfs.Read(buffer, 0, buffer.Length);
//                    sfs.Close();
//                    Response.ContentType = contentType;
//                    Response.OutputStream.Write(buffer, 0, buffer.Length);
//                    //return File(buffer, contentType, "prueba.jpg");
//                    //conn = null; // ownership transfered to the stream
//                    //trn = null;
//                    //return true;
//                }
//            }
//            finally
//            {
//                if (null != trn)
//                {
//                    trn.Dispose();
//                }
//                if (null != Con)
//                {
//                    Con.Dispose();
//                }
//            }
//        }
        
        private SqlConnection GetConnection()
        {
            throw new NotImplementedException();
        }
        //protected bool saveFile(byte[] _file)
        //{
        //    bool Fl = false;
        //    string DtsConection = "Data Source=rkrdo-pc;Initial Catalog=WebPortfolio; user id=adminwebportfolio; Password= adminwebportfolio;";
        //    SqlConnection Con = new SqlConnection(DtsConection);

        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(
        //        ConfigurationManager.ConnectionStrings["DtsConection"].ToString());
        //        conn.Open();
        //        SqlCommand Query = new SqlCommand("savePhoto", conn);
        //        Query.CommandType = CommandType.StoredProcedure;

        //        Query.Parameters.Add("@photo", SqlDbType.VarBinary);
        //        Query.Parameters["@photo"].Value = _file;                

        //        Query.Parameters.Add("@Result", SqlDbType.Int, 4);
        //        Query.Parameters["@Result"].Direction = ParameterDirection.Output;

        //        Query.ExecuteNonQuery();
        //        conn.Close();

        //        if (int.Parse(Query.Parameters["@Result"].Value.ToString()) == 1)
        //        {
        //            Fl = true;
        //        }
        //        else { Fl = false; }
        //    }
        //    catch { Fl = false; }
        //    return Fl;
        //}
       
    }
}
