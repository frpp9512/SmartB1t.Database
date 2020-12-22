using System.Collections.Generic;

namespace SmartB1t.Database.ClassGenerator
{
    internal class SmartB1tCSClass
    {
        public string ClassName { get; set; }
        List<SmartB1tCSVar> Fields { get; set; }

        public SmartB1tCSClass(string className)
        {
            ClassName = className;
            Fields = new List<SmartB1tCSVar>();
        }

        public SmartB1tCSClass(string className, List<SmartB1tCSVar> fields)
        {
            ClassName = className;
            Fields = fields;
        }

        public override string ToString()
        {
            string str = $"\tclass {ClassName} : SmartB1t.Database.DBObject\r\n\t{{";
            string fieldnames = "", varNames = "", varsInit = "";
            foreach (SmartB1tCSVar f in Fields)
            {
                str += $"{f.ToString()}\r\n";
                fieldnames += $", \"{varNames += f.VarName}\"";
                varsInit += $"\t\t\t{f.VarName} = dr.GetValue<{f.DataType}>(\"{f.VarName}\");\r\n";
            }
            str += $"\t\tpublic {ClassName}() : base({ClassName.ToLower()}, new string[] {{id{ClassName}{fieldnames})}}) {{}}\r\n\r\n";
            str += $"\t\tprotected override object[] Values\r\n\t\t{{\r\n\t\t\tget {{ return new object[] {{{varNames}}} }}\r\n\t\t}}\r\n\r\n";
            str += $"\t\tprotected override void SetValues(SmartB1t.Database.DataResult dr)\r\n\t\t{{\r\n{varsInit}\t\t}}\r\n\t}}";
            return str;
        }
    }
}
