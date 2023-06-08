// See https://aka.ms/new-console-template for more information

using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using System.Net;
using System.Xml.Linq;
using Utility.ViewModel;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IStockRepository, StockRepository>();
}).Build();

var Product = host.Services.GetService<IProductRepository>();
var Stock = host.Services.GetService<IStockRepository>();


Console.WriteLine("Hi, welcome ");


bool flag = true;
while (flag)
{

    Console.WriteLine("1 : Add Product");
    Console.WriteLine("2 : ProductList");
    Console.WriteLine("3 : BuyProduct");
    Console.WriteLine("4 : GetSalesProductList");
    Console.WriteLine("5 : SaleProduct");
    Console.WriteLine("6 : GetStockList");

    Console.WriteLine("Enter the desired menu number");
    int number = Convert.ToInt32(Console.ReadLine());

    if (number == 1)
    {
        Console.WriteLine("Enter the product name :");
        Console.WriteLine("The name format should be like this :");
        Console.WriteLine("Alopa_123 :");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the product barcode :");

        var barcode = Console.ReadLine();
        Product.AddProduct(new ProductDTO() { ProductId = 0, Name = name, Barcode = barcode });

        Console.WriteLine("Product added");
        Console.ReadKey();

    }
    else if (number == 2)
    {
        var pro = Product.GetProductList();
        foreach (var item in pro)
        {
            Console.WriteLine("ProductId :" + item.ProductId + " " + "ProductName :" + item.Name + " " + "ProductBarcode :" + item.Barcode);
        }
        Console.ReadKey();

    }
    else if (number == 3)
    {
        var Stocks = Stock.GetSalesProductList();
        foreach (var item in Stocks)
        {
            Console.WriteLine("ProductId :" + item.ProductId + " " + "ProductName :" + item.ProductName + " " + "ProductBarcode :" + item.ProductQuantity);
        }
        Console.WriteLine("Enter the desired product ID :");
        var Id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the desired product Name :");
        var name = Console.ReadLine();

        Console.WriteLine("Enter the desired product Price :");
        var Price = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(Stock.BuyProduct(new StockDTO() { ProductId = Id, ProductQuantity = 1 , Name = name , ProductPrice = Price }));
        Console.ReadKey();

    }
    else if (number == 4)
    {
       var stocks = Stock.GetSalesProductList();
        foreach (var item in stocks)
        {
            Console.WriteLine("ProductId :" + item.ProductId + " " + "ProductName :" + item.ProductName + " " + "ProductBarcode :" + item.Barcode);

        }
        Console.ReadKey();
    }
    else if (number == 5)
    {
        var stocks = Stock.GetSalesProductList();
        foreach (var item in stocks)
        {
            Console.WriteLine("ProductId :" + item.ProductId + " " + "ProductName :" + item.ProductName + " " + "ProductBarcode :" + item.Barcode);

        }
        Console.WriteLine("Type the ID of the desired product :");
        var Id =Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Number of products :");
        var count = Convert.ToInt32(Console.ReadLine());

        var test = Stock.SaleProduct(Id, count);
        Console.WriteLine(test);
        Console.ReadKey();
    }
    else if (number == 6)
    {
        var stocks = Stock.GetSalesProductList();
        foreach (var item in stocks)
        {
            Console.WriteLine("ProductId :" + item.ProductId + " " + "ProductName :" + item.ProductName + " " + "ProductBarcode :" + item.Barcode);

        }
        Console.ReadKey();
    }
    else
    {
        Console.WriteLine("ERROR : Enter one of the menu items");
    }

}






















//Product.AddProduct(new ProductDTO() { ProductId = 1, Name = "test5", Barcode = "12585865856" });



//var Prod = Product.GetProductById(1);
//Console.WriteLine(Prod);

//Stock.BuyProduct(new Stock() { ProductId = 1 , Name = "bab" , ProductPrice = 3200, StockId = 2 , ProductQuantity = 0 });



//var test = Stock.BuyProduct(new StockDTO() { ProductId = 3, Name = "Micro_123", ProductPrice = 10220, StockId = 3 , ProductQuantity = 1});
host.Run();
Console.ReadKey();