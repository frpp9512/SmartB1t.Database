using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace SmartB1t.Database
{
    using System.Data;

    /// <summary>
    /// MySQL querying util class. 
    /// </summary>
    public sealed class MySQLConnector : IDisposable
    {
        static MySQLConnector conn;

        public static MySQLConnector CurrentConnection { get { return conn ?? (conn = new MySQLConnector("localhost", "root", "", "")); } }

        MySqlConnection connection;

        public MySQLConnector(string host, string user, string password, string dbname)
        {
            SetConnection(host, user, password, dbname);
        }

        public void SetConnection(string host, string user, string password, string dbname)
        {
            MySqlConnectionStringBuilder cb = new MySqlConnectionStringBuilder();
            cb.Server = host;
            cb.UserID = user;
            cb.Password = password;
            cb.Database = dbname;
            connection = new MySqlConnection(cb.ConnectionString);
        }

        internal void ExecuteCommand(MySqlCommand cmd)
        {
            cmd.Connection = connection;
            OpenConnection();
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        private void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void InsertDBObject(DBObject dbo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"INSERT INTO {dbo.Tablename}({string.Join(", ", dbo.Fields)}) VALUES({string.Join(", ", dbo.Fields.Select(f => $"@{f}"))})";
            for (int i = 0; i < dbo.Fields.Length; i++)
                cmd.Parameters.AddWithValue($"@{dbo.Fields[i]}", dbo.Values[i]);
            try
            {
                ExecuteCommand(cmd);
                dbo.PrimaryKeyValue = cmd.LastInsertedId;
            }
            catch
            {
                throw;
            }
        }        

        public void UpdateDBObject(DBObject dbo)
        {
            if (dbo.PrimaryKeyValue != null)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"UPDATE {dbo.Tablename} SET {string.Join(", ", dbo.Fields.Select(f => $"{f}=@{f}_value"))} WHERE {dbo.PrimaryKeyField}=@pkeyvalue";
                for (int i = 0; i < dbo.Fields.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"@{dbo.Fields[i]}_value", dbo.Values[i]);
                }
                cmd.Parameters.AddWithValue("@pkeyvalue", dbo.PrimaryKeyValue);
                try
                {
                    ExecuteCommand(cmd);
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new Exception($"The DBObject has not PrimaryKeyValue. Exception sender: {nameof(UpdateDBObject)}.");
        }

        public void DeleteDBObject(DBObject dbo)
        {
            if (dbo.PrimaryKeyValue != null)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"DELETE FROM {dbo.Tablename} WHERE {dbo.PrimaryKeyField}=@pkeyvalue";
                cmd.Parameters.AddWithValue("@pkeyvalue", dbo.PrimaryKeyValue);
                try
                {
                    ExecuteCommand(cmd);
                }
                catch
                {
                    throw;
                }
            }
            else
                throw new Exception($"The DBObject has not PrimaryKeyValue. Exception sender: {nameof(DeleteDBObject)}.");
        }        

        public DataResult SelectDBObject(DBObject dbo)
        {
            if (dbo.PrimaryKeyValue != null)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = $"SELECT * FROM {dbo.Tablename} WHERE {dbo.PrimaryKeyField}=@pkeyvalue";
                cmd.Parameters.AddWithValue("@pkeyvalue", dbo.PrimaryKeyValue);
                try
                {
                    return new DataResult(GetTable(cmd));
                }
                catch
                {
                    throw;
                }
            }
            throw new Exception($"The DBObject has not PrimaryKeyValue. Exception sender: {nameof(SelectDBObject)}.");
        }

        public DataResult SelectTableData<T>() 
            where T : DBObject, new()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"SELECT * FROM {new T().Tablename}";
            return new DataResult(GetTable(cmd));
        }

        public DataResult SelectTableData<T>(int starts, int count)
            where T : DBObject, new()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"SELECT * FROM {new T().Tablename} LIMIT {starts}, {count}";
            return new DataResult(GetTable(cmd));
        }

        public DataResult ExecuteStoredProcedure(string procedureName, string[] paramsNames, object[] paramsValues)
        {
            if (paramsNames.Length != paramsValues.Length)
                throw new Exception($"The arrays must be of the same length. Exception sender: {nameof(ExecuteStoredProcedure)}.");
            List<Parameter> parameters = new List<Parameter>();
            for (int i = 0; i < paramsNames.Length; i++)
            {
                Parameter param = new Parameter { Name = paramsNames[i], Value = paramsValues[i] };
                parameters.Add(param);
            }
            return ExecuteStoredProcedure(procedureName, parameters);
        }

        public DataResult ExecuteStoredProcedure(string procedureName, List<Parameter> parameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = procedureName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue($"@{param.Name}", param.Value);
            }            
            try
            {
                return new DataResult(GetTable(cmd));
            }
            catch
            {
                throw;
            }
        }

        public DataResult ExecuteStoredProcedure(string procedureName, params Parameter[] parameters)
        {
            MySqlCommand mySqlCommand = new MySqlCommand
            {
                CommandText = procedureName,
                CommandType = CommandType.StoredProcedure
            };
            for (int i = 0; i < parameters.Length; i++)
            {
                Parameter parameter = parameters[i];
                mySqlCommand.Parameters.AddWithValue(string.Format("@{0}", parameter.Name), parameter.Value);
            }
            DataResult result;
            try
            {
                result = new DataResult(GetTable(mySqlCommand));
            }
            catch
            {
                throw;
            }
            return result;
        }

        public DataResult ExecuteStoredProcedure(string procedureName)
        {
            return ExecuteStoredProcedure(procedureName, new List<Parameter>());
        }

        public T ExecuteFunction<T>(string functionName)
        {
            return ExecuteFunction<T>(functionName, new Parameter[] { });
        }

        public T ExecuteFunction<T>(string functionName, params Parameter[] parameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"SELECT {functionName}({(string.Join(", ", parameters.Select(p => $"@{p.Name}")))})";
            foreach (var param in parameters)
            {
                cmd.Parameters.AddWithValue($"@{param.Name}", param.Value);
            }
            cmd.Connection = connection;
            OpenConnection();
            object result = cmd.ExecuteScalar();
            CloseConnection();
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), result.ToString());
            return (T)result;
        }

        internal DataTable GetTable(MySqlCommand cmd)
        {
            cmd.Connection = connection;
            MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            OpenConnection();
            adapt.Fill(dt);
            CloseConnection();
            return dt;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
