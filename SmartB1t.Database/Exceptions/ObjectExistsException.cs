using System;

namespace SmartB1t.Database.Exceptions
{
    /// <summary>
    /// Exception thrown when inserting an object that alrady exists in database.
    /// </summary>
    public class ObjectExistsException : Exception
    {
        public ObjectExistsException()
            : base("Cannot add object because it already exists in database")
        {

        }
    }
}
