using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ViewModel;

namespace Services
{
    public interface IStockRepository
    {
        string SaleProduct(int productId, int cnt);
        string BuyProduct(StockDTO productInStock);
        List<SockProductViewModel> GetSalesProductList();
    }
}
