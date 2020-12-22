using System.Collections.Generic;
using System.IO;

namespace SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses
{
    public class SmartB1tCSProject
    {
        public string ProjectName { get; set; }
        public List<SmartB1tCSClass> Classes { get; set; }

        public SmartB1tCSProject(string projectName)
        {
            ProjectName = projectName;
            Classes = new List<SmartB1tCSClass>();
        }

        public SmartB1tCSProject(string projectName, List<SmartB1tCSClass> classes)
        {
            ProjectName = projectName;
            Classes = new List<SmartB1tCSClass>();
        }

        public void Export(string path)
        {
            foreach (SmartB1tCSClass cs in Classes)
            {
                string str = $"using SmartB1t.Database;\r\n\r\nnamespace {ProjectName}\r\n{{\r\n";
                str += $"{cs.ToString()}\r\n}}";
                StreamWriter sw = new StreamWriter($"{path}\\{cs.ClassName}.cs");
                sw.Write(str);
                sw.Close();
            }
        }
    }
}
