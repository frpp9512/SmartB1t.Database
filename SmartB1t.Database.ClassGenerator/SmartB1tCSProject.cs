using System.Collections.Generic;

namespace SmartB1t.Database.ClassGenerator
{
    internal class SmartB1tCSProject
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
    }
}
