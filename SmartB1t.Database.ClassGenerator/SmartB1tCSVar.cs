namespace SmartB1t.Database.ClassGenerator
{
    internal class SmartB1tCSVar
    {
        public string DataType { get; set; }
        public string VarName { get; set; }

        public SmartB1tCSVar(string dataType, string varName)
        {
            DataType = dataType;
            VarName = varName;
        }

        public override string ToString()
        {
            return $"\t\tpublic {DataType} {VarName} {{ get; set; }}";
        }
    }
}
