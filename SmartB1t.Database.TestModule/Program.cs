using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartB1t.Database;

namespace SmartB1t.Database.TestModule
{
    class Program
    {
        static void Main(string[] args)
        {
            MySQLConnector.CurrentConnection.SetConnection("localhost", "root", "", "myshop");
            List<Product> prods = MySQLConnector.CurrentConnection.ExecuteStoredProcedure("LoadActiveProducts").GetList<Product>();
            foreach (var p in prods)
            {
                Console.WriteLine($"{p.Code}\t\t{p.Denomination}\t\t{p.MU}\t\t{p.Amount}\t\t{p.Price}\t\t{p.SellingWay.ToString()}");
            }
            Console.ReadKey();
        }

        public class Product : DBObject
        {
            public string Code { get; set; }
            public string Denomination { get; set; }
            public string MU { get; set; }
            public decimal Amount { get; set; }
            public decimal Price { get; set; }
            public decimal TotalValue { get => Amount * Price; }
            public List<Category> Categories { get; set; } = new List<Category>();
            public SellingWay SellingWay { get; set; } = SellingWay.Free;
            public Status Status { get; set; } = Status.Active;

            public Product()
                : base("product", new string[] { "idProduct", "code", "denomination", "mu", "amount", "price", "sellingWay", "status" })
            {

            }

            protected override object[] Values
            {
                get => new object[]
                    {
                    Code,
                    Denomination,
                    MU,
                    Amount,
                    Price,
                    SellingWay.ToString(),
                    Status.ToString()
                    };
            }

            protected override void SetValues(DataResult values)
            {
                Code = values.GetValue<string>("code");
                Denomination = values.GetValue<string>("denomination");
                MU = values.GetValue<string>("mu");
                Amount = values.GetValue<decimal>("amount");
                Price = values.GetValue<decimal>("price");
                SellingWay = values.GetValue<SellingWay>("sellingWay");
                Status = values.GetValue<Status>("status");
            }
        }

        public enum SellingWay
        {
            Free,
            Assign,
        }

        public class Category : DBObject
        {
            public string Code { get; set; }
            public string Denomination { get; set; }
            public string Description { get; set; }
            public Status Status { get; set; } = Status.Active;

            public Category()
                : base("category", new string[] { "idCategory", "code", "denomination", "description", "status" })
            {

            }

            protected override object[] Values
            {
                get => new object[]
                    {
                    Code,
                    Denomination,
                    Description,
                    Status.ToString(),
                    };
            }

            protected override void SetValues(DataResult values)
            {
                Code = values.GetValue<string>("code");
                Denomination = values.GetValue<string>("denomination");
                Description = values.GetValue<string>("description");
                Status = values.GetValue<Status>("status");
            }
        }
    }
}

namespace SmartB1t.Database.TestModule
{
    enum Status
    {
        Active,
        Inactive
    }
}