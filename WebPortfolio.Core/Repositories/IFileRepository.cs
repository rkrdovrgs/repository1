using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.DataAccess.Abstract;

namespace WebPortfolio.Core.Repositories
{
    public interface IFileRepository
    {
        IFile Get(int id, string name);

        int Insert(byte[] content, string fileName, string contentType);

    }
}
