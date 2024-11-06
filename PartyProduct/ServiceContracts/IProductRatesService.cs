using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IProductRatesService
    {
        public Task<List<ProductRate>> GetAllProductRates();
        public Task AddProductRate(Product product);
    }
}
