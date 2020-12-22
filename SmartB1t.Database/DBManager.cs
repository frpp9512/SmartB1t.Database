using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SmartB1t.Database
{
    public sealed class DBManager : IDisposable
    {
        MySQLQueryExec query_exec;

        public MySQLQueryExec Query_exec
        {
            get { return query_exec; }
            set { query_exec = value; }
        }

        public DBManager(string host, string db_name, string username, string password)
        {
            query_exec = new MySQLQueryExec(host, username, password, db_name);
            password = "";
        }

        /// <summary>
        /// Inserts a DBObject into the database returning inserted's unique identifier
        /// </summary>
        /// <param name="dbo">Object to insert</param>
        public void InsertDBObject(DBObject dbo)
        {
            var values = from v in dbo.Values
                         select $"'{v.ToString().Replace("'", "\'")}'";
            string query = $"INSERT INTO {dbo.Tablename} ({string.Join(",", dbo.Fields)}) VALUES ({string.Join(", ", values)})";
            query_exec.OpenConnectionAndKeepAlive();
            query_exec.RunQuery(query);
            dbo.PrimaryKeyValue = query_exec.GetInstertedId();
            query_exec.CloseAliveConnection();
        }

        /// <summary>
        /// Updates a DBObject with new given values
        /// </summary>
        /// <param name="dbo">Object to update</param>
        public void UpdateDBObject(DBObject dbo)
        {
            if (dbo.PrimaryKeyValue == null)
                throw new Exception("Primary key not set. Object cannot be updated. Was you trying to insert it?!");
            string query = $"UPDATE {dbo.Tablename} SET ";
            List<string> args = new List<string>();
            for (int i = 0; i < dbo.Fields.Length; i++)
                args.Add($"{dbo.Fields[i]} = '{dbo.Values[i]}'");
            query += $"{string.Join(", ", args)} WHERE {dbo.PrimaryKeyField} = '{dbo.PrimaryKeyValue.ToString().Replace("'", "\'")}'";
            query_exec.RunQuery(query);
        }

        public object[] LoadById(DBObject dbo)
        {
            if (dbo.PrimaryKeyValue == null)
                throw new Exception("Unique identifier cannot be null.");
            return query_exec.SelectFromDB("*", dbo.Tablename, $"{dbo.PrimaryKeyField} = '{dbo.PrimaryKeyValue.ToString().Replace("'", "\'")}'").Rows[0].ItemArray;
        }

        public void Delete(DBObject dbo)
        {
            query_exec.DeleteFromDB(dbo.Tablename, $"{dbo.PrimaryKeyField} = '{dbo.PrimaryKeyValue.ToString().Replace("'", "\'")}'");
        }

        public void Dispose()
        {
            query_exec.Dispose();
        }
    }
}
