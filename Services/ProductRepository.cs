using DataBase;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility.ViewModel;

namespace Services
{
    public class ProductRepository : IProductRepository
    {
        public string AddProduct(ProductDTO product)
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
            catch
            {
                throw;
            }
            return null;
        }

        public string GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }
        private bool CheckProductName(string productName)
        {
            var sameName = GetProductList().FirstOrDefault(p => p.Name == productName);
            if (sameName == null)
            {
                return Regex.IsMatch(productName, @"^[A-Z]+([a-z]{3})+.+_+([/d]{3})$");
            }
            else
            {
                return false;
            }
        }
    }
}
