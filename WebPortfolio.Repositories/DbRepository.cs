using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Configuration;
using WebPortfolio.Core.Repositories;
using WebPortfolio.Core.DataAccess;
using WebPortfolio.Core.Extensions;

namespace WebPortfolio.Repositories
{
    public class DbRepository : IDbRepository
    {
        //public ICache cache { get; set; }
        private string _ConnectionName;

        public string ConnectionName
        {
            get
            {
                if (string.IsNullOrEmpty(_ConnectionName))
                    _ConnectionName = "DefaultConnection";
                return _ConnectionName;
            }
            set { _ConnectionName = value; }
        }


        public virtual IList<object> NamedQuery(Dictionary<string, object> SqlParameters, string query, DataTableQuery[] OrderDataTables, string IDFieldName = "", string NameFieldName = "")
        {
            List<object> datatable = new System.Collections.Generic.List<object>();
            string key = NamedQueryKey(SqlParameters, query);
            /*
            if (cache != null && cache.Enabled)
            {
                datatable = cache.Get<List<object>>(key);
            }
            
            if (datatable == null || cache == null || !cache.Enabled)
            {
            */
            datatable = new System.Collections.Generic.List<object>();

            SqlConnection conn = GetConnection();

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var item in SqlParameters.Keys)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = item;
                    parameter.Value = SqlParameters[item].ValueOrNull();

                    cmd.Parameters.Add(parameter);
                }
                /*
                if (Session != null && Session.Transaction != null && Session.Transaction.IsActive)
                    Session.Transaction.Enlist(cmd);
                */

                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    int count = 0;

                    foreach (DataTableQuery table in OrderDataTables)
                    {

                        if (table.IsDynamic)
                        {
                            var list = new List<dynamic>();


                            while (reader.Read())
                            {
                                var row = new ExpandoObject();

                                for (int f = 0; f < reader.FieldCount; f++)
                                {
                                    Fill(f, row, reader);

                                }

                                list.Add(row);

                            }

                            datatable.Add(list);

                        }
                        else
                        {



                            Type type = table.DataTable;
                            var list = type.CreateList();

                            if (!string.IsNullOrEmpty(table.PivotField))
                            {

                                ConstructorInfo magicConstructor = type.GetConstructor(Type.EmptyTypes);
                                object pivClassObject = magicConstructor.Invoke(new object[] { });

                                MethodInfo formtter = type.GetMethod("PivotFormatter", BindingFlags.Instance | BindingFlags.NonPublic);
                                list = (IList)formtter.Invoke(pivClassObject, new object[] { reader, IDFieldName, NameFieldName });

                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    var o = Activator.CreateInstance(type);


                                    for (int f = 0; f < reader.FieldCount; f++)
                                    {
                                        var name = reader.GetName(f).ToLower();

                                        PropertyInfo propinfo = null;

                                        object so = null;

                                        object po = o;

                                        // Allow nested properties - Currently only one level allowed i.e Column SectorType.Id ->> Company -> SectorType -> Id
                                        if (name.Contains("."))
                                        {
                                            var names = name.Split('.');

                                            //Allow inner-nested properties
                                            for (int i = 0; i < names.Length - 1; i++)
                                            {
                                                propinfo = po.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower() == names[i]);

                                                // check if null
                                                if (propinfo.GetValue(po, null) == null)
                                                {
                                                    so = Activator.CreateInstance(propinfo.PropertyType);
                                                    propinfo.SetValue(po, so, null);

                                                }
                                                else
                                                {
                                                    so = propinfo.GetValue(po, null);
                                                }

                                                po = so;
                                            }


                                            propinfo = propinfo.PropertyType.GetProperties().FirstOrDefault(x => x.Name.ToLower() == names[names.Length - 1]);
                                            Fill(so, f, propinfo, reader);
                                        }
                                        else
                                        {
                                            propinfo = o.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower() == name);
                                            Fill(o, f, propinfo, reader);
                                        }
                                    }
                                    list.Add(o);
                                }
                            }

                            datatable.Add(list);
                        }
                        if (OrderDataTables.Length > count)
                        {
                            reader.NextResult();
                            count++;
                        }

                    }
                }
            }

            /*
            if (cache != null && cache.Enabled)
                cache.Put(key, datatable);
                

        }
        */
            return datatable;
        }



        private void Fill(object o, int f, PropertyInfo propinfo, SqlDataReader reader)
        {
            if (propinfo != null)
            {
                if (!reader.IsDBNull(f))
                {
                    switch (reader.GetProviderSpecificFieldType(f).Name)
                    {
                        case "SqlInt32":
                            propinfo.SetValue(o, reader.GetInt32(f), null);
                            break;

                        case "SqlString":
                            propinfo.SetValue(o, reader.GetString(f), null);
                            break;

                        case "SqlInt64":
                            propinfo.SetValue(o, reader.GetInt64(f), null);
                            break;

                        case "SqlBinary":
                            byte[] bytes = reader[f] as byte[];
                            var ms = new MemoryStream();
                            ms.Write(bytes, 0, bytes.Length);
                            propinfo.SetValue(o, ms, null);
                            //ms.Close();                            
                            break;

                        case "SqlMoney":
                            propinfo.SetValue(o, reader.GetDecimal(f), null);
                            break;

                        case "SqlDateTime":
                            propinfo.SetValue(o, reader.GetDateTime(f), null);
                            break;

                        case "SqlDecimal":
                            propinfo.SetValue(o, reader.GetDecimal(f), null);
                            break;

                        case "SqlBoolean":
                            propinfo.SetValue(o, reader.GetBoolean(f), null);
                            break;
                    }
                }
            }
            else
            {
                propinfo = o.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower() == "dynamicproperties");
                if (propinfo != null)
                {
                    if (propinfo.GetValue(o, null) == null)
                    {
                        var exObj = new ExpandoObject();
                        propinfo.SetValue(o, exObj, null);
                    }
                    var currentExpObj = (ExpandoObject)propinfo.GetValue(o, null);
                    Fill(f, currentExpObj, reader);
                }
            }

        }

        private void Fill(int f, ExpandoObject row, SqlDataReader reader)
        {
            var name = reader.GetName(f);

            if (!reader.IsDBNull(f))
            {
                switch (reader.GetProviderSpecificFieldType(f).Name)
                {
                    case "SqlInt32":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetInt32(f));
                        break;

                    case "SqlString":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetString(f));
                        break;

                    case "SqlInt64":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetInt64(f));
                        break;
                    /*
                    case "SqlBinary":
                        ((IDictionary<String, Object>)row).Add(name, new SqlReaderStream(reader, f));
                        break;
                    */
                    case "SqlMoney":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetDecimal(f));
                        break;

                    case "SqlDateTime":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetDateTime(f));
                        break;

                    case "SqlDecimal":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetDecimal(f));
                        break;

                    case "SqlBoolean":
                        ((IDictionary<String, Object>)row).Add(name, reader.GetBoolean(f));
                        break;
                }
            }
            else
            {
                ((IDictionary<String, Object>)row).Add(name, null);
            }
        }

        private string NamedQueryKey(Dictionary<string, object> SqlParameters, string query)
        {
            var key = new StringBuilder();
            key.Append(query);

            foreach (var item in SqlParameters.Keys)
            {
                key.Append(string.Format("_{0}_{1}", item, SqlParameters[item]));
            }

            return key.ToString();
        }

        private SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder(
                                ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString);

            scsb.Pooling = true;
            SqlConnection conn = new SqlConnection(scsb.ConnectionString);
            conn.Open();
            return conn;
        }

        public void SetConnectionName(string connectionName)
        {
            ConnectionName = connectionName;
        }


        public virtual IList<T> NamedQuery<T>(Dictionary<string, object> SqlParameters, string query)
        {

            List<DataTableQuery> datatables = new List<DataTableQuery>();

            datatables.Add(new DataTableQuery
            {
                DataTable = typeof(T)
            });

            return (List<T>)NamedQuery(SqlParameters, query, datatables.ToArray())[0];

        }

        public virtual IList<dynamic> DynamicNamedQuery(Dictionary<string, object> SqlParameters, string query)
        {
            List<DataTableQuery> datatables = new List<DataTableQuery>();

            datatables.Add(new DataTableQuery
            {
                IsDynamic = true
            });

            return NamedQuery(SqlParameters, query, datatables.ToArray())[0] as List<dynamic>;
        }

    }
}
