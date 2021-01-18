using PappyServiceLib;
using System;
using System.Threading.Tasks;
using TransitServiceLib;

namespace TranistConsumerConsole
{
    public class Run
    {
        private IGetProductInfo _getProductInfo;
        private IGetAmazonUSStockAvailabilityForProduct _getStockAvailabilityForProduct;

        public Run(IGetProductInfo getProductInfo, IGetAmazonUSStockAvailabilityForProduct getStockAvailabilityForProduct)
        {
            _getProductInfo = getProductInfo;
            _getStockAvailabilityForProduct = getStockAvailabilityForProduct;
        }

        public Run() : this(new GetProductInfo(), new GetAmazonUSStockAvailabilityForProduct())
        {
        }

        public async Task RunIt()
        {
            bool isValid = false;
            int productNo = 0;
            int productTypeNo = 0;

            while (!isValid)
            {
                Console.WriteLine("Enter a product number.");
                productNo = int.Parse(Console.ReadLine());
                var productInfo = await _getProductInfo.Get(productNo); //Bad product no 234232. Good product no 21596                
                if (productInfo.Products.Length > 0)
                {
                    productTypeNo = productInfo.Products[0].ProductTypeNo;
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"No products found with product number {productNo}. Please try again.");
                }
            }

            var stockAvailability = await _getStockAvailabilityForProduct.Get(productNo, productTypeNo);

            foreach (var product in stockAvailability.Products)
            {
                Console.WriteLine("US Availability For Amazon Sales Channel(11)");
                foreach (var color in product.Colors)
                {
                    foreach (var size in color.Sizes)
                    {
                        foreach (var att in size.Attributes)
                        {
                            Console.WriteLine($"ProductNo : {product.ProductNo} | Color : {color.ColorNo} | Size : {size.SizeNo} | StockStatus : {att.StockStatusCode}");
                        }
                    }
                }

            }
        }
    }
}
