using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
namespace CSharpBlackjack
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            new Game().Run();
        }
    }
}
