using System;
//using System.Collections.Generic;
//using SmartB1t.Database;
using SmartB1t.Database.ClassGenerator;

namespace TestConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SmartB1tCSProject proj = new SmartB1tCSProject("PersonTest");
            SmartB1tCSClass cs = new SmartB1tCSClass("File");
            cs.Fields.AddRange(
                new SmartB1tCSVar[] 
                {
                    new SmartB1tCSVar("string", "filename"),
                    new SmartB1tCSVar("System.DateTime", "creationTime"),
                    new SmartB1tCSVar("long", "filesize"),
                }
                );
            proj.Classes.Add(cs);
            proj.Export(AppDomain.CurrentDomain.BaseDirectory);
            /*MySQLConnector.CurrentConnection.SetConnection("localhost", "root", "", "test");
            List<Person> people = MySQLConnector.CurrentConnection.ExecuteStoredProcedure("SelectPeople").GetList<Person>();
            foreach (var p in people)
                Console.WriteLine($"Fullname: {p.FirstName} {p.LastName}, Phonenumber: {p.PhoneNumber}, Added: {p.AddedDate.ToShortDateString()}, Salary: {p.Salary}");
            Console.ReadKey();*/
        }

        public class Person : SmartB1t.Database.DBObject
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public System.DateTime AddedDate { get; set; }
            public decimal Salary { get; set; }

            public Person() : base("person", new string[] { "idPerson", "firstname", "lastname", "phonenumber", "added", "salary" }, 0)
            {

            }

            protected override object[] Values
            {
                get
                {
                    return new object[] { FirstName, LastName, PhoneNumber, AddedDate, Salary };
                }
            }

            protected override void SetValues(SmartB1t.Database.DataResult dr)
            {
                FirstName = dr.GetValue<string>("firstname");
                LastName = dr.GetValue<string>("lastname");
                PhoneNumber = dr.GetValue<string>("phonenumber");
                AddedDate = dr.GetValue<System.DateTime>("added");
                Salary = dr.GetValue<decimal>("salary");
            }
        }
    }
}
