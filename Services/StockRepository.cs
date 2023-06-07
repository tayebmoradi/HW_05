using DataBase;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.ViewModel;

namespace Services
{
    public class StockRepository : IStockRepository
    {
        private readonly IProductRepository _productRepository;


        public StockRepository(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public string BuyProduct(StockDTO productInStock)
        {

            var u = GetStockList().FirstOrDefault(p => p.ProductId == productInStock.ProductId);
            if (u != null)
            {

                decimal productPrice = ((productInStock.ProductPrice * productInStock.ProductQuantity) +
                    (u.ProductPrice * u.ProductQuantity))
                    / (productInStock.ProductQuantity + u.ProductQuantity);
                u.ProductPrice = Math.Round(productPrice, 1);
                List<Stock> list = GetStockList();
                foreach (var item in list)
                {
                    if (productInStock.ProductId == item.ProductId)
                    {
                        item.ProductQuantity += productInStock.ProductQuantity;
                    }
                }

                var fileName = PathFile.PathFileDataBase1();
                JsonFile.SimpleWrite(list, fileName);

                return $"The {u.Name} was updated.";
            }

            else
            {
                var countStock = GetStockList().Count();
                var Stockid = Math.Max(0, countStock);
                productInStock.StockId = Stockid;
                ProductRepository product = new ProductRepository();

                var productSerch = _productRepository.GetProductList().Find(p => p.ProductId == productInStock.ProductId);


                Random rnd = new Random();
                int num = rnd.Next(10);


                if (productSerch == null)
                {

                    var Pro = new ProductDTO() { ProductId = productInStock.ProductId, Barcode = num.ToString(), Name = productInStock.Name };

                    _productRepository.AddProduct(Pro);
                    AddStock(productInStock);

                    //return $"The {productSerch.Name} was added to stock.";
                }
                else
                {
                    return $"The {productInStock.Name} was not in the products list.";
                }
                return null;
            }
        }

        public List<SockProductViewModel> GetSalesProductList()
        {
            ProductRepository repository = new ProductRepository();
            var Products = repository.GetProductList();
            var Stocks = GetStockList();

            var list = (from s in Stocks
                        join p in Products on s.ProductId equals p.ProductId
                        select new SockProductViewModel()
                        {
                            ProductId = s.ProductId,
                            Barcode = p.Barcode,
                            ProductName = p.Name,
                            ProductPrice = s.ProductPrice,
                            ProductQuantity = s.ProductQuantity,
                        }).ToList();
            var fileName = PathFile.PathFileDataBaseText();
            using (TextWriter tw = File.CreateText(fileName))
            {
                foreach (var s in list)
                {
                    tw.WriteLine("ProductId :" + s.ProductId + " " + "Barcode :" + s.Barcode + " " + "ProductName :" +
                                    s.ProductName + " " + "ProductPrice :" + s.ProductPrice + 
                                    " " + "ProductQuantity :" + s.ProductQuantity);
                }
            }
            return list;
        }

        public string SaleProduct(int productId, int cnt)
        {
            var product = GetStockList().FirstOrDefault(p => p.ProductId == productId);
            int quantity = GetProductQuantity(productId);
            if (quantity > cnt)
            {
                List<Stock> list = GetStockList();
                foreach (var item in list)
                {
                    if (product.ProductId == item.ProductId)
                    {
                        item.ProductQuantity -= cnt;
                    }
                }

                var fileName = PathFile.PathFileDataBase1();
                JsonFile.SimpleWrite(list, fileName);
                return $"{cnt} items of {product.Name} were sold successfully";
            }
            else
            {
                return "Insufficient stock";
            }
        }
        public List<Stock> GetStockList()
        {
            var fileName = PathFile.PathFileDataBase1();
            var Read = JsonFile.SimpleRead(fileName);
            var jsonString = JsonSerializer.Deserialize<List<Stock>>(Read);

            return jsonString.ToList();
        }
        private int GetProductQuantity(int productId)
        {
            return (from s in GetStockList()
                    where s.ProductId == productId
                    select s.ProductQuantity).FirstOrDefault();
        }

        public string AddStock(StockDTO stock)
        {
            try
            {
                var countProduct = GetStockList().Count();
                var Stockid = Math.Max(0, countProduct++);
                var stocks = new List<Stock>()
                {
                    new Stock()
                    {
                        ProductId = stock.ProductId,
                        Name = stock.Name,
                        ProductPrice= stock.ProductPrice,
                        ProductQuantity = stock.ProductQuantity,
                        StockId = Stockid
                    }
                }.ToList();
                var getAll = GetStockList();
                foreach (var item in stocks)
                {
                    getAll.Add(item);
                }
                var fileName = PathFile.PathFileDataBase1();
                JsonFile.SimpleWrite(getAll, fileName);
                return "added successfully.";

            }
            catch (Exception ex)
            {

            }
            return null;

        }

    }
}
