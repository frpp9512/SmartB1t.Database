namespace SmartB1t.Database.ClassGeneratorUtil.XML
{
    public class XMLParam
    {
        string name;
        string value;

        /// <summary>
        /// The XMLParam Constructor
        /// </summary>
        /// <param name="name">Param name</param>
        /// <param name="value">Param value</param>
        public XMLParam(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets the string representation of XMLParam
        /// </summary>
        /// <returns>String XMLParam</returns>
        public override string ToString()
        {
            try 
            { 
                int.Parse(value);
                return name + "=" + value; 
            }
            catch
            {
                if (value.Contains("\'"))
                    return name + "=\"" + value + "\"";
                else
                    if (value.Contains("\""))
                        return name + "=\'" + value + "'";
            }
            return name + "='" + value + "'";
        }
    }
}
