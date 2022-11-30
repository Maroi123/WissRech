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
                b[i] = 1;
                txk[i] = 1;
            }
            CG cG = new CG(b, txk, 1, 10*N+1, "Hilbert");
            Console.ReadKey();
        }
    }
}
