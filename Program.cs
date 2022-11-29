using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WissRech
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //       "Hilbert"

            //"Identity"

            // "FE"


            int N = 3;
            double[] b=new double[N];
            double[] txk = new double[N];
            for(int i=0; i<N; i++)
            {
                b[i] = 0;
                txk[i] = 1;
            }
            CG cG = new CG(b, txk, 0, 100, "Hilbert");
            Console.ReadKey();
        }
    }
}
