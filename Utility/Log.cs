using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ViewModel;

namespace Utility
{
    public static class Log
    {
        public static void Logger(StockDTO product)
        {
            var Path = PathFile.PathFileDataBaseLogBuy();
            using (TextWriter textWriter = File.AppendText(Path))
            {
                textWriter.WriteLine("Time:" + DateTime.Now);
                textWriter.WriteLine("ProductName : " + product.Name);            
                textWriter.WriteLine("Quantity:" + product.ProductQuantity);            
                textWriter.WriteLine("Price:" + product.ProductPrice);            
            }
        }
        public static void Logger(StockDTO productId, int cnt)
        {
            var Path = PathFile.PathFileDataBaseLogSale();
            using (TextWriter tw = File.AppendText(Path))
            {
                tw.WriteLine("DateTime:", DateTime.Now);
                tw.WriteLine($"{cnt} " +
                             $"ProductName{productId.Name}");
                tw.WriteLine("Current stock: {0}",
                             productId.ProductQuantity);
            }
        }
    }
}
