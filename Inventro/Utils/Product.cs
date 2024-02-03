using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventro.Utils
{
    public class Product
    {
        private readonly int id;
        private readonly string productCode;
        private readonly string productName;
        private readonly decimal dailyRate;

        public Product(int id, string productCode, string productName, decimal dailyRate)
        {
            this.id = id;
            this.productCode = productCode;
            this.productName = productName;
            this.dailyRate = dailyRate;
        }

        public int getId() { return id; }
        public string getProductCode() { return productCode; }
        public string getProductName() { return productName; }
        public decimal getDailyRate() { return dailyRate; }
    }
}
