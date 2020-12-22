using System.Collections.Generic;

namespace SmartB1t.Database.ClassGeneratorUtil.XML
{
    public class XMLTag
    {
        int enclosing_level;
        string tagName;
        string body;
        List<XMLTag> children;
        List<XMLParam> xml_params;

        /// <summary>
        /// The XMLTag Constructor
        /// </summary>
        /// <param name="tagName">The name of the tag</param>
        /// <param name="encapsulationLevel">Amount of tabulations depends of the enclosing level</param>
        public XMLTag(string tagName, int enclosing_level)
        {
            this.tagName = tagName;
            this.enclosing_level = enclosing_level;
            children = new List<XMLTag>();
            xml_params = new List<XMLParam>();
        }

        /// <summary>
        ///  Constructor of the XML Tag. Assumed the enclosing level to be zero.
        /// </summary>
        /// <param name="tagName">The name of the tag</param>
        public XMLTag(string tagName)
        {
            this.tagName = tagName;
            this.enclosing_level = 0;
            children = new List<XMLTag>();
            xml_params = new List<XMLParam>();
        }

        /// <summary>
        /// The XMLTag Constructor
        /// </summary>
        /// <param name="tagName">The name of the tag</param>
        /// <param name="body">The body of the tag</param>
        public XMLTag(string tagName, string body)
        {
            this.tagName = tagName;
            this.enclosing_level = 0;
            this.body = body;
            children = new List<XMLTag>();
            xml_params = new List<XMLParam>();
        }

        /// <summary>
        /// The XMLTag Constructor
        /// </summary>
        /// <param name="tagName">The name of the tag</param>
        /// <param name="body">The body of the tag</param>
        /// <param name="enclosing_level">Amount of tabulations depends of the enclosing level</param>
        public XMLTag(string tagName, string body, int enclosing_level)
        {
            this.tagName = tagName;
            this.enclosing_level = enclosing_level;
            this.body = body;
            children = new List<XMLTag>();
            xml_params = new List<XMLParam>();
        }

        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        public int Enclosing_Level
        {
            get { return enclosing_level; }
            set { enclosing_level = value; }
        }

        public List<XMLTag> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Adds a new Main Tag Child. Enclosing Level will be set to zero.
        /// </summary>
        /// <param name="child_name">The child name</param>
        /// <returns>XMLTag Object</returns>
        public XMLTag AddChild(string child_name)
        {
            children.Add(new XMLTag(child_name, enclosing_level+1));
            return children[children.Count - 1];
        }

        /// <summary>
        /// Adds a new XMLTag child
        /// </summary>
        /// <param name="child_name">The child name</param>
        /// <param name="parent">If true, enclosing level will be set to zero</param>
        /// <returns>XMLTag child</returns>
        public XMLTag AddChild(string child_name, bool parent)
        {
            children.Add(new XMLTag(child_name, parent ? 0 : enclosing_level + 1));
            return children[children.Count - 1];
        }

        /// <summary>
        /// Adds a new XMLTag child
        /// </summary>
        /// <param name="child_name">The child name</param>
        /// <param name="body">Tag value</param>
        /// <returns>XMLTag child</returns>
        public XMLTag AddChild(string child_name, string body)
        {
            children.Add(new XMLTag(child_name, body, enclosing_level + 1));
            return children[children.Count - 1];
        }

        /// <summary>
        /// Adds a new XMLTag child
        /// </summary>
        /// <param name="child_name">The child name</param>
        /// <param name="xml_params">Tag params</param>
        /// <returns>XMLTag child</returns>
        public XMLTag AddChild(string child_name, List<XMLParam> xml_params)
        {
            children.Add(new XMLTag(child_name, enclosing_level + 1));
            for (int i = 0; i < xml_params.Count; i++)
                children[children.Count - 1].Params.Add(xml_params[i]);
            return children[children.Count - 1];
        }

        /// <summary>
        /// Adds a new XMLTag child
        /// </summary>
        /// <param name="child_name">The child name</param>
        /// <param name="body">Tag value</param>
        /// <param name="xml_params">Tag params</param>
        /// <returns>XMLTag child</returns>
        public XMLTag AddChild(string child_name, string body, List<XMLParam> xml_params)
        {
            children.Add(new XMLTag(child_name, body, enclosing_level + 1));
            for (int i = 0; i < xml_params.Count; i++)
                children[children.Count - 1].Params.Add(xml_params[i]);
            return children[children.Count - 1];
        }

        /// <summary>
        /// Adds a new XMLTag child
        /// </summary>
        /// <param name="child">XMLTag child</param>
        public void AddChild(XMLTag child)
        {
            child.Enclosing_Level = enclosing_level + 1;
            children.Add(child);
        }

        public List<XMLParam> Params
        {
            get { return xml_params; }
            set { xml_params = value; }
        }

        /// <summary>
        /// XMLTag object's string representation
        /// </summary>
        /// <returns>String representing the xml object</returns>
        public override string ToString()
        {
            bool xmlmainTag = tagName.ToLower() == "xml";
            string tabs = "";
            for (int k = 0; k < enclosing_level; k++)
                tabs += "\t";
            string str = tabs + "<" + (xmlmainTag ? "?" : "") + tagName;
            for (int i = 0; i < xml_params.Count; i++)
                str += " " + xml_params[i].ToString();
            str += (xmlmainTag ? "?" : "") + ">";
            str += body;
            for (int j = 0; j < children.Count; j++)
                str += "\r\n" /*+ tabs*/ + children[j].ToString();
            if (children.Count > 0)
                str += "\r\n" + tabs + "</" + tagName + ">";
            else
                str += "</" + tagName + ">";
            return xmlmainTag ? str.Remove(str.Length - 6, 6) : str;
        }

        /// <summary>
        /// Loads params from a xml string line
        /// </summary>
        /// <param name="line">Line read from xml file</param>
        /// <returns>List of tag params</returns>
        public static List<XMLParam> LoadParamsFromXMLTag(string line)
        {
            bool reading_name = false;
            bool reading_value = false;
            bool reading_value_starts = false;
            bool isString = false;
            string name = "";
            string value = "";
            List<XMLParam> xml_params = new List<XMLParam>();
            if (!line.Contains(" "))
                return xml_params;
            int stIndex = line.StartsWith("<?xml") ? 5 : line.IndexOf(' ') < line.IndexOf('>') ? line.IndexOf(' ') : line.Length;
            for (int i = stIndex; i < line.Length; i++)
            {
                char chr = line[i];
                if (chr == ' ')
                {
                    if (!reading_value)
                    {
                        reading_name = true;
                        continue;
                    }
                    else
                    {
                        if (!isString)
                        {
                            reading_name = true;
                            AddXMLParam(ref name, ref value, ref reading_value, ref isString, xml_params);
                            continue;
                        }
                        else
                        {
                            value += ' ';
                            continue;
                        }
                    }
                }
                if (chr == '\'' || chr == '\"')
                {
                    if (reading_value_starts && reading_value)
                    {
                        reading_value_starts = false;
                        isString = true;
                        //value += line[i];
                    }
                    else
                        AddXMLParam(ref name, ref value, ref reading_value, ref isString, xml_params);
                    continue;
                }
                if (chr == '>' || chr == '?')
                {
                    if (name != "")
                        AddXMLParam(ref name, ref value, ref reading_value, ref isString, xml_params);
                    break;
                }
                if (reading_name)
                {
                    if (chr == '=')
                    {
                        reading_value = true;
                        reading_value_starts = true;
                        reading_name = false;
                        continue;
                    }
                    name += chr;
                    continue;
                }
                else
                    if (reading_value)
                {
                    value += chr;
                    continue;
                }
            }
            return xml_params;
        }

        /// <summary>
        /// Adds a new param to the current tag
        /// </summary>
        /// <param name="name">Param name</param>
        /// <param name="value">Param value</param>
        /// <param name="reading_value">Indicates if reading value when iterating the xml string content</param>
        /// <param name="isString">Indicates if the object value is a string type</param>
        /// <param name="xml_params">List of tag params</param>
        private static void AddXMLParam(ref string name, ref string value, ref bool reading_value, ref bool isString, List<XMLParam> xml_params)
        {
            reading_value = false;
            isString = false;
            xml_params.Add(new XMLParam(name, value));
            name = value = "";
        }

        /// <summary>
        /// Loads param value from a string xml line
        /// </summary>
        /// <param name="line">String line</param>
        /// <returns>Value of the param</returns>
        public static string LoadValueFromXMLTag(string line)
        {
            int openTagEnds = line.IndexOf('>');
            int openTagBegins = line.IndexOf('<');
            int i = line.IndexOf('<', openTagBegins + 1);
            if (i != -1)
                return line.Substring(openTagEnds + 1, i - openTagEnds - 1);
            else
                return line.Substring(openTagEnds + 1, line.Length - openTagEnds - 1);
        }
    }
}
