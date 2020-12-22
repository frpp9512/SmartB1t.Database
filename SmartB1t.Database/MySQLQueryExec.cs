using System.Data;
using MySql.Data.MySqlClient;
using System;

namespace SmartB1t.Database
{
    /// <summary>
    /// Simple class for querying with MySQL
    /// </summary>
    public sealed class MySQLQueryExec : IDisposable
    {
        MySqlDataAdapter adapt;
        bool keepalive = false;

        /// <summary>
        /// Simple class for querying with MySQL
        /// </summary>
        /// <param name="host">Host name or address</param>
        /// <param name="user">User id to connect database</param>
        /// <param name="password">Password of user id</param>
        /// <param name="db">Database name</param>
        public MySQLQueryExec(string host, string user, string password, string db)
        {
            adapt = new MySqlDataAdapter("", new MySqlConnection("server='" + host + "';user id='" + user + "';password='" + password + "';database='" + db + "';pooling=false"));
        }

        public MySQLQueryExec(string connString)
        {
            adapt = new MySqlDataAdapter("", new MySqlConnection(connString));
        }

        public MySQLQueryExec(MySqlDataAdapter adapter)
        {
            this.adapt = adapter;
        }

        public MySqlDataAdapter Adapt
        {
            get
            {
                return adapt;
            }

            set
            {
                adapt = value;
            }
        }

        /// <summary>
        /// Opens the connection and keep it open
        /// </summary>
        public void OpenConnectionAndKeepAlive()
        {
            keepalive = true;
            OpenConnection();
        }

        /// <summary>
        /// Closes an open connection
        /// </summary>
        public void CloseAliveConnection()
        {
            keepalive = false;
            CloseConnection();
        }

        /// <summary>
        /// Open the connection to run queries
        /// </summary>
        private void OpenConnection()
        {
            if (adapt.SelectCommand.Connection.State == ConnectionState.Closed)
                adapt.SelectCommand.Connection.Open();
        }

        /// <summary>
        /// Closes the connection if it's not alive
        /// </summary>
        private void CloseConnection()
        {
            if (keepalive) return;
            if (adapt.SelectCommand.Connection.State == ConnectionState.Open)
                adapt.SelectCommand.Connection.Close();
        }

        /// <summary>
        /// Runs the specified MySQL query
        /// </summary>
        /// <param name="str_query">Query to run</param>
        /// <returns>DataTable filled with results</returns>
        public DataTable RunQuery(string str_query)
        {
            OpenConnection();
            adapt.SelectCommand.CommandText = str_query;
            if (str_query.StartsWith("select", StringComparison.CurrentCultureIgnoreCase))
            {
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                CloseConnection();
                return dt;
            }
            else
            {
                adapt.SelectCommand.ExecuteNonQuery();
                CloseConnection();
                return null;
            }
        }

        /// <summary>
        /// Runs a query to get a cell object
        /// </summary>
        /// <param name="str_query">Query to run</param>
        /// <returns>An object representing a table cell</returns>
        public object ExecuteScalar(string str_query)
        {
            OpenConnection();
            adapt.SelectCommand.CommandText = str_query;
            object obj = adapt.SelectCommand.ExecuteScalar();
            CloseConnection();
            return obj;
        }

        /// <summary>
        /// Gets last id in the table
        /// </summary>
        /// <param name="table">Table name</param>
        /// <param name="idFieldName">Field name</param>
        /// <returns>Returns an string representing the id</returns>
        public string GetLastID(string table, string idFieldName)
        {
            return ExecuteScalar(string.Format("select {0} from {1} order by {0} desc limit 0,1", idFieldName, table)).ToString();
        }

        /// <summary>
        /// Gets the last inserted object id for this connection
        /// </summary>
        /// <returns> Returns the last inserted object id</returns>
        public string GetInstertedId()
        {
            return ExecuteScalar("select @@IDENTITY").ToString();
        }

        /// <summary>
        /// A method to insert an object in db
        /// </summary>
        /// <returns>Returns the number of inserted rows</returns>
        public int InsertToDB(string fields, string table, string values)
        {
            OpenConnection();
            adapt.SelectCommand.CommandText = "insert into " + table + "(" + fields + ") values(" + values + ")";
            int res = adapt.SelectCommand.ExecuteNonQuery();
            var last = adapt.SelectCommand.LastInsertedId;
            CloseConnection();
            return res;
        }

        /// <summary>
        /// A method to update an object in db
        /// </summary>
        /// <returns>Returns the number of updated rows</returns>
        public int UpdateDBField(string table, string field, string value, string condition)
        {
            OpenConnection();
            adapt.SelectCommand.CommandText = "update " + table + " set " + field + "=\"" + value + "\"" + (condition != "" ? (" where " + condition) : "");
            int res = adapt.SelectCommand.ExecuteNonQuery();
            CloseConnection();
            return res;
        }

        /// <summary>
        /// A method to delete an object in db
        /// </summary>
        /// <returns>Returns the number of deleted rows</returns>
        public int DeleteFromDB(string table, string condition)
        {
            OpenConnection();
            adapt.SelectCommand.CommandText = "delete from " + table + (condition != "" ? (" where " + condition) : "");
            int res = adapt.SelectCommand.ExecuteNonQuery();
            CloseConnection();
            return res;
        }

        /// <summary>
        /// A method to select data from a table
        /// </summary>
        /// <param name="fields">Fields to select</param>
        /// <param name="table">Table for gathering data</param>
        /// <param name="condition">Condition for selection; leaving it empty means none should be specified</param>
        /// <returns>A DataTable containing selection result</returns>
        public DataTable SelectFromDB(string fields, string table, string condition)
        {
            OpenConnection();
            string cmd = "select " + fields + " from " + table + (condition != "" ? (" where " + condition) : "");
            adapt.SelectCommand.CommandText = cmd;
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            CloseConnection();
            return dt;
        }

        /// <summary>
        /// Dispose the MySQLDataAdapter object
        /// </summary>
        public void Dispose()
        {
            adapt.Dispose();
        }
    }
}
