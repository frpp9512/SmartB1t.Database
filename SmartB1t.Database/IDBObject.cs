namespace SmartB1t.Database
{
    public interface IDBObject
    {
        /// <summary>
        /// The name of the table where the DBObject is stored.
        /// </summary>
        string Tablename { get; set; }
        
        /// <summary>
        /// An array of the fields names of the table where the DBObject is stored, without the Primary Key field.
        /// </summary>
        string[] Fields { get; set; }

        /// <summary>
        /// An array of the values of the DBObject in the same order that the array of Fields.
        /// </summary>
        object[] Values { get; }

        /// <summary>
        /// The Primary Key field name of the table where the DBObject is stored.
        /// </summary>
        string PrimaryKeyField { get; set; }

        /// <summary>
        /// The value of the primary key field.
        /// </summary>
        object PrimaryKeyValue { get; set; }
    }
}