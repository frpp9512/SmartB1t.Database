namespace TestConsoleUI
{
    class File : SmartB1t.Database.DBObject
    {
        public string filename { get; set; }
        public System.DateTime creationTime { get; set; }
        public long filesize { get; set; }

        public File() : base("file", new string[] { "idFile", "filename", "creationTime", "filesize" }) { }

        protected override object[] Values
        {
            get { return new object[] { filename, creationTime, filesize }; }
        }

        protected override void SetValues(SmartB1t.Database.DataResult dr)
        {
            filename = dr.GetValue<string>("filename");
            creationTime = dr.GetValue<System.DateTime>("creationTime");
            filesize = dr.GetValue<long>("filesize");
        }
    }
}