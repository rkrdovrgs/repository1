using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPortfolio.Core.Repositories;
using System.Configuration;


namespace WebPortfolio.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application";

            return View();
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
        [HttpPost]
        public void prueba(HttpPostedFileBase imagen)
        {
            int lenphoto = Convert.ToInt16(imagen.InputStream.Length);
            byte[] image = new byte[lenphoto]; 

            string DtsConection = "Data Source=rkrdo-pc;Initial Catalog=WebPortfolio; user id=adminwebportfolio; Password= adminwebportfolio;";
            //SqlConnection Con = new SqlConnection(DtsConection);
            
            //try
            //{
            //    SqlCommand cmd = new SqlCommand("UPDATE UserProfile SET PhotoProfile = @Foto WHERE UserId = 36", Con);
            //    //cmd.ExecuteNonQuery();
            //    //Crear parámetros para la declaración de inserción que contiene la imagen ..
            //    SqlParameter parametros = new SqlParameter("@Foto", SqlDbType.Binary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
                                
            //    cmd.Parameters.Add(parametros);
            //    Con.Open();
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //   ///dddf
           
            //}
            //finally
            //{
            //    if (Con.State != ConnectionState.Closed)
            //    { Con.Close(); }
            //}
            try
            {
                MemoryStream ms = new MemoryStream();
                //FileStream fs = new FileStream(imagen.InputStream.ToString(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // ms.SetLength(fs.Length);
                // fs.Read(ms.GetBuffer(), 0, (int)fs.Length);

                // byte[] arrphoto = ms.GetBuffer();
                //  ms.Flush();
                // fs.Close();

                using (SqlConnection Con = new SqlConnection(DtsConection))
                using (SqlCommand cmd = Con.CreateCommand())
                {
                    Con.Open();
                    cmd.CommandText = "UPDATE UserProfile SET PhotoProfile = @PhotoProfile WHERE UserId = 35";
                    cmd.Parameters.Add("@PhotoProfile", SqlDbType.VarBinary).Value = image;
                    cmd.ExecuteNonQuery();
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                //
            }
            
            
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
