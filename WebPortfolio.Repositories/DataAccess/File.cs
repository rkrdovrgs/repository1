using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Repositories
{
    public class File: IFile
    {
        public byte[] Content { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }
    }
}
