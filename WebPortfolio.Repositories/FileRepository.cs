using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.DataAccess.Abstract;
using WebPortfolio.Core.Extensions;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Models.Entities;



namespace WebPortfolio.Repositories
{
    public class FileRepository : IFileRepository
    {
        private string _connectionName;

        public FileRepository()
            : this("DefaultConnection")
        {

        }

        public FileRepository(string connectionName)
        {
            _connectionName = connectionName;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[_connectionName].ToString());
        }



        public IFile Get(int id, string name)
        {
            IFile file = null;
            using (SqlConnection conn = GetConnection())
            {
                // WindowsIdentity newid = SqlContext.WindowsIdentity;
                // WindowsImpersonationContext impersonatedUser = newid.Impersonate();

                //conn.Open();
                //SqlTransaction trn = conn.BeginTransaction();
                //using (SqlCommand cmd = new SqlCommand(@"SELECT ContentLength, ContentType, Content.PathName() as FilePath, GET_FILESTREAM_TRANSACTION_CONTEXT()
                //      FROM [File]  WHERE Id = @id and Name = @name", conn, trn))
                using (SqlCommand cmd = new SqlCommand(@"SELECT ContentLength, ContentType, Content
                         FROM [File]  WHERE Id = @id and Name = @name", conn))
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
                                int contentLength = reader.GetInt32(0);
                                string contentType = reader.GetString(1);
                                //string filepath = reader.GetString(2);
                                byte[] content = reader.GetSqlBytes(2).Buffer;
                                //string contentDisposition = reader.GetString(0);                                
                                // string contentCoding = reader.IsDBNull(2) ? null : reader.GetString(2);                               
                                //  string path = reader.GetString(4);                                

                                //SqlFileStream sfs = new SqlFileStream(filepath, context, System.IO.FileAccess.Read);

                                //byte[] buffer = new byte[(int)sfs.Length];
                                //sfs.Read(buffer, 0, buffer.Length);
                                //sfs.Close();
                                file = new File
                                {
                                    Content = content,
                                    //ContentLength = contentLength,
                                    ContentType = contentType,
                                    Name = name

                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {

                            reader.Close();
                            //trn.Commit();
                            //trn.Dispose();
                            //conn.Close();
                            conn.Dispose();
                            //trn = null;
                            //impersonatedUser.Undo ();
                        }
                    }

                }
            }
            return file;

        }

        public IFile Insert(byte[] content, string fileName, string contentType)
        {
            string name = StringExtensions.GetRandom();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = "INSERT INTO [File] (Guid, Name, FileName, Content, ContentLength, ContentType) VALUES(newid(), @name, @filename, @content, @contentlen, @contenttype)" +
                                      "SELECT Id FROM [File] WHERE Name = @name";
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = fileName;
                    cmd.Parameters.Add("@content", SqlDbType.VarBinary).Value = content;
                    cmd.Parameters.Add("@contentlen", SqlDbType.Int).Value = content.Length;
                    cmd.Parameters.Add("@contenttype", SqlDbType.VarChar).Value = contentType;
                    //cmd.ExecuteNonQuery();

                    int id = Convert.ToInt16(cmd.ExecuteScalar());
                    conn.Close();



                    return new File { 
                        Name = name,
                        Id = id
                    };
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
    }
}
