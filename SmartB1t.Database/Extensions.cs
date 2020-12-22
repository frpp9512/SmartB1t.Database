using SmartB1t.Database.Exceptions;

namespace SmartB1t.Database
{
    public static class Extensions
    {
        /// <summary>
        /// Loads object's data from the database (Needs the PrimaryKeyValue is set).
        /// </summary>
        /// <param name="dbo"></param>
        public static void LoadMe(this DBObject dbo)
        {
            dbo.SetValues(MySQLConnector.CurrentConnection.SelectDBObject(dbo));
        }

        /// <summary>
        /// Inserts object's data to the database.
        /// </summary>
        /// <param name="dbo"></param>
        /// <exception cref="ObjectExistsException">Object exists in database</exception>
        public static void InsertMe(this DBObject dbo)
        {
            if (!dbo.ExistsInDB)
            {
                MySQLConnector.CurrentConnection.InsertDBObject(dbo);
            }
            else
                throw new ObjectExistsException();
        }

        /// <summary>
        /// Saves object's data to the database (Needs the PrimaryKeyValue is set).
        /// </summary>
        /// <param name="dbo"></param>
        public static void UpdateMe(this DBObject dbo)
        {
            if (dbo.ExistsInDB)
            {
                MySQLConnector.CurrentConnection.UpdateDBObject(dbo);
            }
            else
                throw new ObjectNotExistsException();
        }

        /// <summary>
        /// Deletes object's data from the database (Needs the PrimaryKeyValue is set).
        /// </summary>
        /// <param name="dbo"></param>
        public static void DeleteMe(this DBObject dbo)
        {
            MySQLConnector.CurrentConnection.DeleteDBObject(dbo);
        }        
    }
}
