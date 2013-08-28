using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortfolio.Core.DataAccess.Abstract
{
    public interface IFile
    {
         string Name { get; set; }
         //string FileName { get; set; }
         //int ContentLength { get; set; }
         string ContentType { get; set; }
         byte[] Content { get; set; }

    }
}
