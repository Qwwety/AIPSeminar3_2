using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AIPSeminar3_2
{
    struct Product
    {
        public string Name;
        public long ArticleNumber;
        public int Count;
        public double Price;
        public char Discount;

        public Product(string Name, long ArticleNumber, int Count, double Price, char Discount)
        {
            this.Name = Name;
            this.ArticleNumber = ArticleNumber;
            this.Count = Count;
            this.Price = Price;
            this.Discount = Discount;
        }
    }



    class Program
    {
        //static Dictionary<string, List<Product>> DataBase = new Dictionary<string, List<Product>>();
        static string Path = @"D:\C#\AIP\AIPSeminar3_2\DataBases\";
        static string DataBaseName = "";
        static List<Product> Products = new List<Product>(20);

        static void Main(string[] args)
        {
            //List<Product> Products = new List<Product>(20);

            while (true)
            {


                if (!String.IsNullOrEmpty(DataBaseName))
                {
                    Console.WriteLine("Vebirite trebuemi Punckt");

                    Console.WriteLine($"1::Create|Open data base" + "\n" +
                                      $"2::Upload data" + "\n" +
                                      $"3::Save data base" + "\n" +
                                      $"4::Add product" + "\n" +
                                      $"5::Show all products" + "\n" +
                                      $"6::Search product by article number" + "\n" +
                                      $"7::Show all products with discount" + "\n" +
                                      $"8::Show all products that are out of stock" + "\n" +
                                      $"9::Sort all products by selected attribute" + "\n");

                    try
                    {
                        int SelectedNumber = Convert.ToInt32(Console.ReadLine());

                        switch (SelectedNumber)
                        {
                            case 1:
                                CreateOpenDataBase();
                                break;
                            case 2:
                                GetDataBaseData();
                                break;
                            case 3:
                                SaveData();
                                break;
                            case 4:
                                CreateProduct();
                                break;
                            case 5:
                                ShowAllProducts(Products);
                                break;
                            case 6:
                                WorkWithProduct();
                                break;
                            case 7:
                                ShowAllProductsWithDiscount(Products);
                                break;
                            case 8:
                                ShowAllProductsThatAreOutOfStock(Products);
                                break;
                            case 9:
                                SortAllProductsBySelectedAttribute();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine($"Enter Data Base Name to Create|Open it");
                    CreateOpenDataBase();
                }


            }
        }

        /// <summary>
        /// Task1
        /// </summary>
        static void CreateOpenDataBase()
        {
            Path = @"D:\C#\AIP\AIPSeminar3_2\DataBases\";
            DataBaseName = "";

            Console.WriteLine("Enter DataBase Name");
            string Name;
            DataBaseName = Console.ReadLine();

            Path += DataBaseName;

            try
            {
                FileWrite(Path, null, FileMode.OpenOrCreate);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Task 3
        /// </summary>
        static void SaveData()
        {
            try
            {
                FileWrite(Path, Products, FileMode.Create);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
        static void FileWrite(string Path, List<Product> Products, FileMode fileMode)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(new FileStream(Path, fileMode, FileAccess.Write)))
                {
                    if (Products != null)
                    {
                        foreach (var s in Products)
                        {
                            writer.Write(s.Name);
                            writer.Write(s.ArticleNumber);
                            writer.Write(s.Count);
                            writer.Write(s.Price);
                            writer.Write(s.Discount);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Task2
        /// </summary>
        static void GetDataBaseData()
        {
            FileRead(Path);
        }
        static void FileRead(string Path)
        {
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(Path, FileMode.Open)))
                {
                    Products.Clear();

                    while (reader.PeekChar() > -1)
                    {
                        string Name = reader.ReadString();
                        long ArticleNumber = reader.ReadInt64();
                        int Count = reader.ReadInt32();
                        double Price = reader.ReadDouble();
                        char Discount = reader.ReadChar();


                        Product product = new Product(Name, ArticleNumber, Count, Price, Discount);

                        Products.Add(product);

                        Console.WriteLine("Name::{0},ArticleNumber::{1},Count::{2},Price::{3},Discount::{4}",
                                            Name, ArticleNumber, Count, Price, Discount);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Task 4
        /// </summary>
        static void CreateProduct()
        {
            //Console.WriteLine("Vedite Sledushee S")
            int ContinueAdding = 0;

            string Name;
            long ArticleNumber;
            int Count;
            double Price;
            char Discount;

            while (ContinueAdding != 3)
            {
                Console.WriteLine("Enter:: Name");
                Name = Console.ReadLine().ToString();

                Console.WriteLine("Enter:: ArticleNumber");
                ArticleNumber = Convert.ToInt64(Console.ReadLine());

                Console.WriteLine("Enter:: Count");
                Count = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter:: Price");
                Price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter:: Discount");
                Discount = Console.ReadKey().KeyChar;

                Console.WriteLine("\n" + "Add new Product ? (1-Add/2-Start Again/ 3-Stop Adding)");

                Console.WriteLine($"Name::{Name}" + "\n" +
                                  $"ArticleNumber::{ArticleNumber}" + "\n" +
                                  $"Count::{Count}" + "\n" +
                                  $"Price::{Price}" + "\n" +
                                  $"Discount::{Discount}" + "\n"
                                  );

                ContinueAdding = Convert.ToInt32(Console.ReadLine());

                if (ContinueAdding == 1)
                {
                    Product product = new Product(Name, ArticleNumber, Count, Price, Discount);
                    Products.Add(product);
                }
                else if (ContinueAdding == 2)
                {
                    continue;
                }
                else if (ContinueAdding == 3)
                {
                    break;
                }

            }
        }
        /// <summary>
        /// Task 5
        /// </summary>
        /// <param name="Products"></param>
        static void ShowAllProducts(List<Product> Products)
        {
            foreach (var item in Products)
            {
                Console.WriteLine($"Name::{item.Name}" + "\n" +
                                 $"ArticleNumber::{item.ArticleNumber}" + "\n" +
                                 $"Count::{item.Count}" + "\n" +
                                 $"Price::{item.Price}" + "\n" +
                                 $"Discount::{item.Discount}" + "\n"
                                 );
            }
        }

        /// <summary>
        /// Task 6
        /// </summary>
        static void WorkWithProduct()
        {
            Console.WriteLine("Product Name");

            try
            {
                string Name = Console.ReadLine();

                Product Product = Products.GroupBy(x => x).Where(y => y.Key.Name == Name).Select(y => y.Key).First();
                int Ind = Products
              .Select((i, index) => new { index, i }).Where(y => y.i.Name == Name).Select(y=>y.index).First();

                Console.WriteLine($"Name::{Product.Name}" + "\n" +
                                  $"ArticleNumber::{Product.ArticleNumber}" + "\n" +
                                  $"Count::{Product.Count}" + "\n" +
                                  $"Price::{Product.Price}" + "\n" +
                                  $"Discount::{Product.Discount}" + "\n"
                                  );

                Console.WriteLine("\n"+"Select Action");
                Console.WriteLine($"1::Edit Product" +"\n"+
                                  $"2::Delete Product"+"\n");

              int ActionNumber = Convert.ToInt32(Console.ReadLine());

                switch (ActionNumber)
                {
                    case 1:
                        Product=EditProduct(Product);
                        Products[Ind] = Product;
                        break;
                    case 2:
                        Products.Remove(Product);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static Product EditProduct(Product Product)
        {
            long ArticleNumber;
            int Count;
            double Price;
            char Discount;


            Console.WriteLine("Enter:: ArticleNumber");
            ArticleNumber = Convert.ToInt64(Console.ReadLine());

            Console.WriteLine("Enter:: Count");
            Count = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter:: Price");
            Price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter:: Discount");
            Discount = Console.ReadKey().KeyChar;

            Console.WriteLine(
                                 $"\n"+"ArticleNumber::{ArticleNumber}" + "\n" +
                                 $"Count::{Count}" + "\n" +
                                 $"Price::{Price}" + "\n" +
                                 $"Discount::{Discount}" + "\n"
                                 );

            Product.Name = Product.Name;
            Product.ArticleNumber = ArticleNumber;
            Product.Count = Count;
            Product.Price = Price;
            Product.Discount = Discount;

            return Product;
        }
            

        /// <summary>
        /// Task 7
        /// </summary>
        /// <param name="Products"></param>
        static void ShowAllProductsWithDiscount(List<Product> Products)
        {
            foreach (var item in Products)
            {
                if (item.Discount != 0)
                {
                    Console.WriteLine($"Name::{item.Name}" + "\n" +
                                     $"ArticleNumber::{item.ArticleNumber}" + "\n" +
                                     $"Count::{item.Count}" + "\n" +
                                     $"Price::{item.Price}" + "\n" +
                                     $"Discount::{item.Discount}" + "\n"
                                     );
                }
            }
        }
        /// <summary>
        /// Task 8
        /// </summary>
        /// <param name="Products"></param>
        static void ShowAllProductsThatAreOutOfStock(List<Product> Products)
        {
            foreach (var item in Products)
            {
                if (item.Count == 0)
                {
                    Console.WriteLine($"Name::{item.Name}" + "\n" +
                                     $"ArticleNumber::{item.ArticleNumber}" + "\n" +
                                     $"Count::{item.Count}" + "\n" +
                                     $"Price::{item.Price}" + "\n" +
                                     $"Discount::{item.Discount}" + "\n"
                                     );
                }
            }
        }

        /// <summary>
        /// Task 9
        /// </summary>
        static void SortAllProductsBySelectedAttribute()
        {
            Console.WriteLine("Select Attribute");

            Console.WriteLine($"1::Name" + "\n" +
                              $"2::ArticleNumber" + "\n" +
                              $"3::Count" + "\n" +
                              $"4::Price" + "\n" +
                              $"5::Discount" + "\n");

            try
            {
                int NumSelectedAttribute = Convert.ToInt32(Console.ReadLine());

                switch (NumSelectedAttribute) 
                { 
                    case 1:
                        Products = Products.OrderBy(x => x.Name).ToList();
                        break;
                    case 2:
                        Products = Products.OrderBy(x => x.ArticleNumber).ToList();
                        break;
                    case 3:
                        Products = Products.OrderBy(x => x.Count).ToList();
                        break;
                    case 4:
                        Products = Products.OrderBy(x => x.Price).ToList();
                        break;
                    case 5:
                        Products = Products.OrderBy(x => x.Discount).ToList();
                        break;

                }

                foreach (var item in Products)
                {
                    Console.WriteLine($"Name::{item.Name}" + "\n" +
                                     $"ArticleNumber::{item.ArticleNumber}" + "\n" +
                                     $"Count::{item.Count}" + "\n" +
                                     $"Price::{item.Price}" + "\n" +
                                     $"Discount::{item.Discount}" + "\n"
                                     );
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
