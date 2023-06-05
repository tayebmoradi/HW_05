// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using Utility.ViewModel;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddScoped<IProductRepository, ProductRepository>();
    
}).Build();

var Product = host.Services.GetService<IProductRepository>();

//Product.AddProduct(new ProductDTO() { ProductId = 1, Name = "LapdT_123", Barcode = "12585865856" });
//var pro = Product.GetProductList();
//foreach (var item in pro)
//{
//    Console.WriteLine(item.Name);
//}
var Prod = Product.GetProductById(1);
Console.WriteLine(Prod);
host.Run();
Console.ReadKey();
