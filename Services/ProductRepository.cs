using DataBase;
using Domain;
using System.Text.Json;
using System.Text.RegularExpressions;
using Utility.ViewModel;

namespace Services
{
    public class ProductRepository : IProductRepository
    {
        public string AddProduct(ProductDTO product)
        {
            if (product != null)
            {
                try
                {
                    bool isValid = CheckProductName(product.Name);
                    if (isValid)
                    {
                        var Pro = new List<Product>()
                {
                    new Product()
                    {
                        ProductId = product.ProductId,
                        Barcode = product.Barcode,
                        Name = product.Name,
                    }
                }.ToList();
                        var getAll = GetProductList();
                        foreach (var item in Pro)
                        {
                            getAll.Add(item);
                        }
                        var fileName = PathFile.PathFileDataBase();
                        JsonFile.SimpleWrite(getAll, fileName);
                        return "added successfully.";
                    }
                    else
                    {
                        return "The selected product name is" +
                                " incorrect or duplicate.";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Enter the name according to the given format");
                }
            }

            return "There is no product";
        }

        public string GetProductById(int id)
        {
            var GetAll = GetProductList();
            var FindId = GetAll.Find(p => p.ProductId == id);
            return "ProductId : " + FindId.ProductId + " \n" + "Name : " + FindId.Name + " \n" + "Barcod : " + FindId.Barcode;
        }

        public List<Product> GetProductList()
        {
            var fileName = PathFile.PathFileDataBase();
            var Read = JsonFile.SimpleRead(fileName);
            var list = JsonSerializer.Deserialize<List<Product>>(Read);

            return list.ToList();
        }
        private bool CheckProductName(string productName)
        {

            if (Regex.IsMatch(productName, @"^[A-Z]+([a-z]{3})+.+_+([0-9]{3})$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
