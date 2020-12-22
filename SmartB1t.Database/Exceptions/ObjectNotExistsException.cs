using System;

namespace SmartB1t.Database.Exceptions
{
    /// <summary>
    /// Exception that is thrown when updating an object that not exists in database.
    /// </summary>
    public class ObjectNotExistsException : Exception
    {
        public ObjectNotExistsException()
            : base("Cannot update object because it does not exist in database.")
        {

        }
    }
}
