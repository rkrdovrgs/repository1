using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using WebPortfolio.Core.DataAccess;

namespace WebPortfolio.Core.Repositories
{
    public interface IDbRepository
    {
        IList<object> NamedQuery(Dictionary<string, object> SqlParameters, string query, DataTableQuery[] OrderDataTables, string IDFieldName = "", string NameFieldName = "");

        IList<T> NamedQuery<T>(Dictionary<string, object> SqlParameters, string query);

        IList<dynamic> DynamicNamedQuery(Dictionary<string, object> SqlParameters, string query);
    }
}
