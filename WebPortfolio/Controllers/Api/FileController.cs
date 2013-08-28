using System;
using System.Collections.Generic;
using System.Linq;

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
using WebPortfolio.Core.DataAccess.Abstract;
using WebPortfolio.Models.Entities;

namespace WebPortfolio.Controllers.Api
{
    [Authorize]
    public class FileController : Controller
    {
        public IWPRepository<UserProfile> userprofilerepository { get; set; }
        public IWPRepository<File> filesfilerepository { get; set; }

        public IFileRepository filerepository { get; set; }

        [HttpPost]
        public void Upload(HttpPostedFileBase file)
        {
            byte[] content = new byte[file.ContentLength];

            file.InputStream.Read(content, 0, file.ContentLength);

            filerepository.Insert(content, file.FileName, file.ContentType);

            
        }
        

        public void Get(int id, string name)
        {
            IFile file = filerepository.Get(id, name);
            Response.ContentType = file.ContentType;
            Response.OutputStream.Write(file.Content, 0, file.Content.Length);
        }
    }
}
