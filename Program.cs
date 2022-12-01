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
            double[] b = new double[N];
            double[] txk = new double[N];
            for (int i = 0; i < N; i++)
            {
                b[i] = 1;
                txk[i] = 1;
            }
            // cG = new CG(b, txk, 0, 10 * N + 1, "Hilbert");
            //cG.run();
            Vektoren bv = new Vektoren(b);
            bv.multiply(2);
            Vektoren xv = new Vektoren(txk);
            Vektoren summe = new Vektoren(xv.add(bv).get_vektor());
            Console.WriteLine("Der Vektor b ist:");
            bv.Ausgabe();
            Console.WriteLine("Der Vektore xv ist:");
            xv.Ausgabe();
            Console.WriteLine("Der Vektor x+b ist:");
            summe.Ausgabe();
            Console.ReadKey();
        }
    
    }
}
