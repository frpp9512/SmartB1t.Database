using System.Collections.Generic;

namespace SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses
{
    public class SmartB1tCSClass
    {
        public string ClassName { get; set; }
        public List<SmartB1tCSVar> Fields { get; set; }

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
            string str = $"\tclass {ClassName} : DBObject\r\n\t{{\r\n";
            string fieldnames = "", varNames = "", varsInit = "", get_values = "";
            foreach (SmartB1tCSVar f in Fields)
            {
                str += $"{f.ToString()}\r\n";
                fieldnames += $", \"{f.VarName}\"";
                varNames += $"{f.VarName}, ";
                varsInit += f.IsCustomType ? $"\t\t\t{f.DataType} {f.VarName} = new {f.DataType}() {{ PrimaryKeyValue = dr.GetValue<object>(\"id{f.DataType}\") }};\r\n\t\t\t{f.VarName}.LoadMe();\r\n"
                    : $"\t\t\t{f.VarName} = dr.GetValue<{f.DataType}>(\"{f.VarName}\");\r\n";
                get_values += $"{(f.IsCustomType ? $"{f.VarName}.PrimaryKeyValue" : $"{f.VarName}")}, ";
            }
            if (varNames.Length > 2)
            {
                varNames = varNames.Remove(varNames.Length - 2, 2);
                get_values = get_values.Remove(get_values.Length - 2, 2);
            }
            str += $"\r\n\t\tpublic {ClassName}() : base(\"{ClassName.ToLower()}\", new string[] {{\"id{ClassName}\"{fieldnames}}}) {{}}\r\n\r\n";
            str += $"\t\tprotected override object[] Values\r\n\t\t{{\r\n\t\t\tget {{ return new object[] {{{get_values}}}; }}\r\n\t\t}}\r\n\r\n";
            str += $"\t\tprotected override void SetValues(DataResult dr)\r\n\t\t{{\r\n{varsInit}\t\t}}\r\n\t}}";
            return str;
        }
    }
}
