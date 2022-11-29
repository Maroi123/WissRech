using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WissRech
{
    public class CG
    {
        double tk, bek;
        double[] tjk, tjk1, tpk, tpk1;  //Daniels methode neue Arrays, lösche die alten wenn es klappt
        double[] b, solution, xk1, xk, zk, tdk, tdk1, trk, trk1,Ak; //b = rechte seite, solution ist der Lösungsvektor xk1= xk+1, xk=xk,zk =z vektor ,tdk=dk,tdk1=tdk+1,trk=rk, trk1=trk+1;
        double eps, fehler, ak, bk; //eps ist die genauigkeit, fehler ist der aktuelle fehler, ak ist alpha k, bk ist beta k
        int N, dimension;    //N ist die anzahl der Iterationen, dimension ist die Dimension der Vektoren
        string Methode; //Welche Methode verwendet werden soll
        /// <summary>
        /// Hier wird ein Objekt von CG konstruiert
        /// </summary>
        /// <param name="b">Vektor der rechten seite Ax=b</param>
        /// <param name="xk">startwert</param>
        /// <param name="eps">Fehlergenauigkeit</param>
        /// <param name="N">Iterationen</param>
        /// <param name="Methode">Welche Methode verwendet werden soll</param>
        public CG(double[] b, double[] xk, double eps, int N, string Methode)
        {
            if (b.Length != xk.Length)
            {
                throw new InvalidOperationException("dimension not fitting");  //werfe einen fehler wenn die dimensionen nicht passen. 
            }
            else
            {
                this.dimension = b.Length;
                objektifizierung();
                
                this.b = b;
                this.xk = xk;
                this.eps = eps;
                this.Methode = Methode;
                

                
                for (int i = 0; i < dimension; i++) //initialisiere die array's mit 0 dieser schritt ist semi unnötig
                {
                    this.solution[i] = 0;
                    this.xk1[i] = 0;
                    this.zk[i] = 0;
                    this.tdk[i] = 0;
                    this.tdk1[i] = 0;
                    this.trk[i] = 0;
                    this.trk1[i] = 0;
                    this.Ak[i] = 0;
                    this.tjk[i] = 0;
                    this.tjk1[i] = 0;
                    this.tpk[i] = 0;
                    this.tpk1[i] = 0;


                }

                switch (Methode)  //wähle die Methode aus
                {
                    default:   //Erklärung: Switch methode funktioniert fast wie ein if nur mit dem unterschied das in den richtigen fall gesprungen wird bsp: du hast 100 fälle du könntest immer if(i==1){code} elseif(i==2){code} der computer prüft dann jedes mal ist i=1? nein dann ist i=2? nein usw.. switch ist einfach effektiver
                        break;
                    case "Hilbert":
                        CG_method(hilbert);
                        break;
                    case "Identity":
                        CG_method(identity);
                        break;
                    case "FE":
                        CG_method(FE);
                        break;

                        

                }
                for (int j = 0; j < dimension; j++) //gebe den lösungsvektor aus
                {
                    solution[j] = xk[j];
                    Console.WriteLine(solution[j]);
                }
            }

            

        }
        /// <summary>
        /// objekte von den Vektoren erstellen. 
        /// </summary>
        private void objektifizierung()
        {
            this.b =new double[dimension];
            this.solution = new double[dimension];
            this.xk1 = new double[dimension];
            this.xk = new double[dimension];
            this.zk = new double[dimension];
            this.tdk = new double[dimension];
            this.tdk1 = new double[dimension];
            this.trk = new double[dimension];
            this.trk1 = new double[dimension];
            this.Ak = new double[dimension];
            this.tjk = new double[dimension];
            this.tjk1 = new double[dimension];
            this.tpk = new double[dimension];
            this.tpk1 = new double[dimension];


        }
        /// <summary>
        /// implementiert die CG methode
        /// </summary>
        /// <param name="matrix_vec">Welche Matrix verwendet werden soll</param>
        private void CG_method(Func<double[], double[]> matrix_vec)
        {
            int i = 0;
            double normjkquad, dktdk, normjk1quad;
            double[] vektor=new double[dimension];
            vektor=matrix_vec(xk);
            for (int j = 0; j < dimension; j++)
            {
                tjk[j] = vektor[j] - b[j];
                tdk[j] = -tjk[j];
            }
            do
            {
                tpk=matrix_vec(tdk);
                normjkquad = produkt(tjk, tjk); 
                dktdk= produkt(tdk,tdk);
                tk = normjkquad / dktdk;
                for(int j = 0;j < dimension;j++)
                {
                    xk1[j] = xk[j] + tk * tdk[j];
                    tjk1[j] = tjk[j] + tk * tpk[j];
                }



                for (int j = 0; j < dimension; j++)
                {
                    xk[j] = xk1[j];
                    tjk[j] = tjk1[j];
                    tdk[j] = tdk1[j];
                    tpk[j] = tpk1[j];
                }
                i++;
            } while (i < N);
         /*   int i = 0; //zählvariabel 
            double rkrk = 0; //produkt von rk transponiert rk
            double rk1rk1 = 0; // produkt von rk+1 transponiert rk+1
            double tdktz = 0; //produkt von dk transponiert z
            do
            {

                if (i == 0)  //die 0 iteration
                {
                    Ak=matrix_vec(xk);
                    for(int j=0;j<dimension;j++)  
                    {
                        trk[j] = b[j] - Ak[j];
                        tdk[j] = trk[j];
                    }
                } //die 1. iteration
                zk=matrix_vec(tdk);
                rkrk = produkt(trk, trk);
                tdktz = produkt(tdk, Ak);
                ak = rkrk / tdktz;
                for(int j=0;j<dimension;j++)
                {
                    xk1[j] = xk[j] - ak * tdk[j];
                    trk1[j] = trk[j] + ak * zk[j];
                }
                rk1rk1 = produkt(trk1, trk1);
                bk= rk1rk1/rkrk; 
                for(int j=0;j<dimension;j++)
                {
                    tdk1[j] = -trk1[j] + bk * tdk[j];
                }
                for(int j=0;j<dimension; j++)
                {
                    xk[j] = xk1[j];
                    trk[j] = trk1[j];
                    tdk[j] = tdk1[j];
                }
                i++;
            } while (fehler < eps && i < N);
         */
            
        }
        /// <summary>
        /// berechnet transpose(x)*y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double produkt(double[] x, double[] y)
        {
            double outp = 0;
            if(x.Length != y.Length)
            {
                return 0;
            }
            else
            {
                for (int i = 0; i < x.Length; i++)
                {

                    outp = outp + x[i] * y[i];
                }
            return outp;
            }
        }
        /// <summary>
        /// Multiplikation mit id
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double[] identity(double[] x)
        {
            return x;
        }
        /// <summary>
        /// Multiplikation mit der Hilbertmatrix
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double[] hilbert(double[] x)
        {
            double sum = 0;
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                sum = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    sum += x[j] * 1 / (i + j + 1);
                }
                y[i] = sum;
            }
            return y;
        }


        /// <summary>
        /// Multiplikation mit der Finiten Elementen Matrix
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double[] FE(double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 1; i < x.Length - 1; i++)
            {
                y[i] = -x[i - 1] + 2 * x[i] - x[i + 1];
            }
            y[0] = 2 * x[0] - x[1];
            y[x.Length - 1] = -x[x.Length - 2] + 2 * x[x.Length - 1];
            return y;
        }
    }
}
