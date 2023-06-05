using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ViewModel;

namespace Services
{
    public interface IProductRepository
    {
        string AddProduct(ProductDTO product);
        List<Product> GetProductList();
        string GetProductById(int id);
    }
}
