using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace SmartB1t.Database
{
    public static class DBUtil
    {
        /// <summary>
        /// Determines if an object exists if other object in database matches the specified field and value params given.
        /// </summary>
        /// <param name="tb_name">Table name</param>
        /// <param name="field">Field to compare</param>
        /// <param name="value">Value to compare</param>
        /// <param name="pk_field">Current object primary key field name</param>
        /// <param name="pk_value">Current object primary key value</param>
        /// <returns>True if object matches criteria; otherwise false</returns>
        public static bool ObjectExists(string tb_name, string field, string value, string pk_field, object pk_value)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = $"select {field} from {tb_name} where {field} = @value and {pk_field} != @pk_value limit 0,1";
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@pk_value", pk_value);
            DataResult dr = new DataResult(MySQLConnector.CurrentConnection.GetTable(cmd));
            return dr.RowsCount > 0;
        }

        /// <summary>
        /// Determines if an object exists if other object in database matches the specified field and value given in the list of parameters
        /// </summary>
        /// <param name="tb_name">Table name</param>
        /// <param name="parameters">List of parameters. Param name matches field name; and param value, field value</param>
        /// <param name="pk_field">Current object primary key field name</param>
        /// <param name="pk_value">Current object primary key value</param>
        /// <returns>True if object matches criteria; otherwise false</returns>
        public static bool ObjectExists(string tb_name, List<Parameter> parameters, string pk_field, object pk_value)
        {
            if (parameters != null)
            {
                if (parameters.Count > 0)
                {
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Parameters.AddWithValue("@tb_name", tb_name);
                    cmd.Parameters.AddWithValue("@pk_field", pk_field);
                    cmd.Parameters.AddWithValue("@pk_value", pk_value);
                    string criteria = $"{parameters[0].Name} = '{parameters[0].Value}'";
                    for (int i = 1; i < parameters.Count; i++)
                        criteria += $" and {parameters[i].Name} = '{parameters[i].Value}'";
                    cmd.CommandText = $"select @field from @tb_name where {criteria} and @pk_field != @pk_value limit 0,1";
                    DataResult dr = new DataResult(MySQLConnector.CurrentConnection.GetTable(cmd));
                    return dr.RowsCount > 0;
                }
                else
                    throw new Exception("Parameters list can't be empty.");
            }
            else
                throw new Exception("Parameters list can't be null.");
        }
    }
}
