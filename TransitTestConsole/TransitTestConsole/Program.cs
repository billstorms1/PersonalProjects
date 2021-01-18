using System.Threading.Tasks;

namespace TranistConsumerConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var program = new Run();
            await program.RunIt();
        }
    }
}
