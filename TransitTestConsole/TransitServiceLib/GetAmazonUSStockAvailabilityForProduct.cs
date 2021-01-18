using ShippingApiRpc;
using System.Threading.Tasks;

namespace TransitServiceLib
{
    public interface IGetAmazonUSStockAvailabilityForProduct
    {
        Task<GetStockAvailabilityResponse> Get(int productNo, int productTypeNo);
    }
    public class GetAmazonUSStockAvailabilityForProduct : IGetAmazonUSStockAvailabilityForProduct
    {
        private IShippingApiRPC _shippingApiRPC;
        public GetAmazonUSStockAvailabilityForProduct(IShippingApiRPC shippingApiRPC)
        {
            _shippingApiRPC = shippingApiRPC;
        }

        public GetAmazonUSStockAvailabilityForProduct() : this(new ShippingApiRPCClient()) { }

        public async Task<GetStockAvailabilityResponse> Get(int productNo, int productTypeNo)
        {
            var request = new GetStockAvailabilityRequest();
            request.Products = new StockAvailabilityProductRequest[]
            {
                new StockAvailabilityProductRequest
                {
                    ProductNo = productNo,
                    ProductTypeNo = productTypeNo,
                    SalesChannelNo = 11
                },

            };
            request.DomainCode = "US";
            var stockAvailabilityResponse = await _shippingApiRPC.GetStockAvailabilityAsync(request);
            return stockAvailabilityResponse;
        }
    }
}