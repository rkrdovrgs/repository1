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
using System.Data.SqlTypes;
using WebPortfolio.Core.Extensions;

namespace WebPortfolio.Controllers.Api
{
    [Authorize]
    public class FileController : Controller
    {

        private string connectionName = "FileConnection";
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ToString());
        }

        [HttpPost]
        public void Upload(HttpPostedFileBase file)
        {
            string name = StringExtensions.GetRandom();


            byte[] content = new byte[file.ContentLength];

            file.InputStream.Read(content, 0, file.ContentLength);

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "INSERT INTO [File] (Guid, Name, FileName, Content, ContentLength, ContentType) VALUES(newid(), @name, @filename, @content, @contentlen, @contenttype)";
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = file.FileName;
                    cmd.Parameters.Add("@content", SqlDbType.VarBinary).Value = content;
                    cmd.Parameters.Add("@contentlen", SqlDbType.Int).Value = file.ContentLength;
                    cmd.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = file.ContentType;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (null != conn)
                        conn.Dispose();
                }
            }
        }


        public void Get(int id, string name)
        {

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlTransaction trn = conn.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(@"SELECT ContentLength, ContentType, Content.PathName() as FilePath, GET_FILESTREAM_TRANSACTION_CONTEXT()
                         FROM [File]  WHERE Id = @id and Name = @name", conn, trn))
                {

                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    //cmd.ExecuteNonQuery();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                //string contentDisposition = reader.GetString(0);
                                string contentType = reader.GetString(1);
                                string filepath = reader.GetString(2);
                                // string contentCoding = reader.IsDBNull(2) ? null : reader.GetString(2);
                                long contentLength = reader.GetInt32(0);
                                //  string path = reader.GetString(4);
                                byte[] context = reader.GetSqlBytes(3).Buffer;

                                SqlFileStream sfs = new SqlFileStream(filepath, context, System.IO.FileAccess.Read);

                                byte[] buffer = new byte[(int)sfs.Length];
                                sfs.Read(buffer, 0, buffer.Length);
                                sfs.Close();
                                Response.ContentType = contentType;
                                Response.OutputStream.Write(buffer, 0, buffer.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            reader.Close();
                            trn.Dispose();
                            conn.Dispose();
                            trn = null;
                        }
                    }

                }
            }
        }
    }
}
