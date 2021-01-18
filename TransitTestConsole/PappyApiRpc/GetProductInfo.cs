using PappyApiRpc;
using System;
using System.Threading.Tasks;

namespace PappyServiceLib
{
    public interface IGetProductInfo
    {
        Task<GetProductsResponse> Get(int prodcutNo);
    }
    public class GetProductInfo : IGetProductInfo
    {
        private IPappyApiRpc _pappyApiRpc;

        public GetProductInfo(IPappyApiRpc pappyApiRpc)
        {
            _pappyApiRpc = pappyApiRpc;
        }

        public GetProductInfo() : this(new PappyApiRpcClient(new PappyApiRpcClient.EndpointConfiguration())) { }
        public async Task<GetProductsResponse> Get(int prodcutNo)
        {
            try
            {
                var request = new GetProductsRequest();
                request.ProductIDs = new[] { prodcutNo };
                var response = await _pappyApiRpc.GetProductsAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }
}