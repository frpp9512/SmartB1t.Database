namespace SmartB1t.Database.ClassGeneratorUtil.SmartB1tGenerationClasses
{
    public class SmartB1tCSVar
    {
        public string DataType { get; set; }
        public string VarName { get; set; }
        public bool IsCustomType { get; set; }

        public SmartB1tCSVar(string varName, string dataType, bool isCustomType)
        {
            DataType = dataType;
            VarName = varName;
            IsCustomType = isCustomType;
        }

        public override string ToString()
        {
            return $"\t\tpublic {DataType} {VarName} {{ get; set; }}";
        }
    }
}
