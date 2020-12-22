using System;
using System.Linq;

namespace SmartB1t.Database
{
    /// <summary>
    /// Represents an object stored in a database.
    /// </summary>
    public abstract class DBObject
    {
        protected internal string Tablename { get; set; }
        protected internal string[] Fields { get; set; }
        protected internal abstract object[] Values { get; }
        protected internal string PrimaryKeyField { get; set;  }
        protected internal object PrimaryKeyValue { get; set; }

        /// <summary>
        /// Creates a new instance of DBObject.
        /// </summary>
        /// <param name="tablename">The name of the table where the object is stored.</param>
        /// <param name="fields">The fields of the table. It sets the first field given as the primary key.</param>
        public DBObject(string tablename, string[] fields) : this(tablename, fields, 0) { }

        /// <summary>
        /// Creates a new instance of DBObject.
        /// </summary>
        /// <param name="tablename">The name of the table where the object is stored.</param>
        /// <param name="fields">The fields of the table.</param>
        /// <param name="pkeyIndex">The index corresponding to the primary key value in the fields array.</param>
        public DBObject(string tablename, string[] fields, int pkeyIndex)
        {
            Tablename = tablename;
            PrimaryKeyField = fields[pkeyIndex];
            Fields = fields.Where(f => f != fields[pkeyIndex]).ToArray();
        }

        /// <summary>
        /// Sets the values to the object from the given DataResult.
        /// </summary>
        /// <param name="dr">The object data.</param>
        protected internal abstract void SetValues(DataResult dr);

        /// <summary>
        /// Defines behavior to determine if object exists in database. By default is true if object has PrimaryKeyValue property initialized.
        /// </summary>
        /// <returns>True if object exists; otherwise false</returns>
        protected internal virtual bool ExistsInDB
        {
            get
            {
                if (PrimaryKeyValue == null)
                    return false;
                return true;
            }
        }
    }
}
