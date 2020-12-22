using System.Text;
using System.IO;

namespace SmartB1t.Database.ClassGeneratorUtil.XML
{
    public class XMLTree
    {
        Encoding encoding;
        string version;
        XMLTag main;

        /// <summary>
        /// Creates a new instance of XMLTree
        /// </summary>
        /// <param name="version">XML version</param>
        /// <param name="encoding">File encoding</param>
        public XMLTree(string version, Encoding encoding)
        {
            this.version = version;
            this.encoding = encoding;
            main = new XMLTag("xml");
            main.Params.Add(new XMLParam("version", version));
            main.Params.Add(new XMLParam("encoding", encoding.WebName));
            main.Enclosing_Level = -1;
        }

        #region OldFuncionsCommented
        /*public XMLTree(string file_path)
        {
            StreamReader sr = new StreamReader(file_path);
            string line = "";
            do
            {
               line = sr.ReadLine();
            } while (!line.Contains("<?xml"));
            main = new XMLTag("xml");
            List<XMLParam> prms = LoadParamsFromXMLTag(line);
            for (int i = 0; i < prms.Count; i++)
            {
                if (prms[i].Name == "version")
                    this.version = prms[i].Value;
                else
                    if (prms[i].Name == "encoding")
                        this.encoding = GetEncoding(prms[i].Value);
            }
            main.Params.AddRange(prms);
            sr.Close();
        }*/

        /*public List<XMLParam> LoadParamsFromXMLTag(string line)
        {
            bool reading_name = false;
            bool reading_value = false;
            bool reading_value_starts = false;
            bool isString = false;
            string name = "";
            string value = "";
            List<XMLParam> xml_params = new List<XMLParam>();
            for (int i = line.IndexOf("<?xml") + 5; i < line.Length; i++)
            {
                if (line[i] == ' ')
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
                            AddXMLParam(ref name, ref value, ref reading_value, ref isString, line[i], xml_params);
                            continue;
                        }
                        else
                        {
                            value += (int)32;
                            continue;
                        }
                        if (!isString)
                            reading_value = false;
                    }
                }
                if (line[i] == '\'' || line[i] == '\"')
                {
                    if (reading_value_starts && reading_value)
                    {
                        reading_value_starts = false;
                        isString = true;
                        value += line[i];
                    }
                    else
                    {
                        AddXMLParam(ref name, ref value, ref reading_value, ref isString, line[i], xml_params);
                        reading_value = false;
                        if (isString)
                            value += line[i];
                        isString = false;
                        xml_params.Add(new XMLParam(name, value));
                        name = value = "";
                    }
                    continue;
                }
                if (reading_name)
                {
                    if (line[i] == '=')
                    {
                        reading_value = true;
                        reading_value_starts = true;
                        reading_name = false;
                        continue;
                    }
                    name += line[i];
                    continue;
                }
                else
                    if (reading_value)
                    {
                        value += line[i];
                        continue;
                    }
                if (line[i] == '>' || line[i] == '?')
                    break;
            }
            return xml_params;
        }*/

        /*private Encoding GetEncoding(string header_name)
        {
            switch (header_name)
            {
                case "utf-8":
                    return Encoding.UTF8;
                case "us-ascii":
                    return Encoding.ASCII;
                case "unicodeFFFE":
                    return Encoding.BigEndianUnicode;
                case "utf-16":
                    return Encoding.Unicode;
                default:
                    return Encoding.Default;
            }
        }*/

        /*private void AddXMLParam(ref string name, ref string value, ref bool reading_value, ref bool isString, char c, List<XMLParam> xml_params)
        {
            reading_value = false;
            if (isString)
                value += c;
            isString = false;
            xml_params.Add(new XMLParam(name, value));
            name = value = "";
        }*/
        #endregion

        public Encoding Encoding
        {
            get { return encoding; }
        }

        public string Version
        {
            get { return version; }
        }

        public XMLTag MainTag
        {
            get { return main; }
            set { main = value; }
        }

        /// <summary>
        /// Loads XMLTree object from a file
        /// </summary>
        /// <param name="file_path">File path</param>
        /// <returns>A XMLTree</returns>
        public static XMLTree LoadFrom(string file_path)
        {
            XMLTag tag = new XMLTag("xml");
            string version = "";
            Encoding enc = Encoding.Default;
            StreamReader sr = new StreamReader(file_path);
            string line = "";
            do
            {
                line = sr.ReadLine();
            } while (!line.Contains("<?xml"));
            tag.Params.AddRange(XMLTag.LoadParamsFromXMLTag(line));
            for (int i = 0; i < tag.Params.Count; i++)
            {
                if (tag.Params[i].Name == "version")
                    version = tag.Params[i].Value;
                if (tag.Params[i].Name == "encoding")
                    enc = Encoding.GetEncoding(tag.Params[i].Value);
            }
            XMLTree tree = new XMLTree(version, enc);
            XMLTag last = tree.MainTag; /*new XMLTag("xml");*/
            sr.Close();
            sr = new StreamReader(file_path, enc);
            do
            {
                line = sr.ReadLine();
            } while (!line.Contains("<?xml"));
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line.Contains("<") && !IsTagClosing(line))
                {
                    string name = "";
                    string val = "";
                    if (line.Contains(" ") && line.IndexOf(' ') < line.IndexOf('>'))
                        name = line.Substring(line.IndexOf('<') + 1, line.IndexOf(' ') - (line.IndexOf('<') + 1)); //antes -1
                    else
                        name = line.Substring(line.IndexOf('<') + 1, line.IndexOf('>') - line.IndexOf('<') - 1);
                    val = XMLTag.LoadValueFromXMLTag(line);
                    //XMLTag child = tree.MainTag.AddChild(name, val, LoadParamsFromXMLTag(line));
                    int i = 0;
                    if (line.StartsWith("\t"))
                    {
                        while (line[i] == '\t')
                            i++;
                    }
                    if (last.Enclosing_Level < i)
                        last = last.AddChild(name, val, XMLTag.LoadParamsFromXMLTag(line));
                    else
                    {
                        if (i == 0)
                        {
                            last = tree.MainTag.AddChild(name, (i == 0 ? true : false));
                            last.Body = val;
                            last.Params.AddRange(XMLTag.LoadParamsFromXMLTag(line));
                        }
                        else
                        {
                            XMLTag tagIterator = tree.MainTag;
                            for (int j = 0; j < tagIterator.Children.Count; j++)
                            {
                                if (tagIterator.Enclosing_Level == i - 1)
                                {
                                    last = tagIterator.AddChild(name, val, XMLTag.LoadParamsFromXMLTag(line));
                                    break;
                                }
                                else
                                {
                                    tagIterator = tagIterator.Children[tagIterator.Children.Count - 1 - j];
                                    j--;
                                }
                            }
                        }
                    }
                }
            }
            sr.Close();
            return tree;
        }

        /// <summary>
        /// Saves the XMLTree into a file
        /// </summary>
        /// <param name="file_path">File path</param>
        public void SaveTo(string file_path)
        {
            StreamWriter sw = new StreamWriter(file_path, false, encoding);
            sw.Write(main.ToString());
            sw.Close();
        }

        /// <summary>
        /// Gets if the tag is closing
        /// </summary>
        /// <param name="line">String line</param>
        /// <returns>True if tag is closing; otherwise false.</returns>
        private static bool IsTagClosing(string line)
        {
            while (line.StartsWith("\t"))
                line = line.Remove(0, 1);
            return line.StartsWith("</");
        }
    }
}
