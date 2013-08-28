using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPortfolio.Core.Repositories
{
    public interface IFileRepository
    {

        File get(string name);

        void Insert(File file);

    }
}
