using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace WissRech
{
    public class FEM
    {
        private double[] T;
        private int N;
        private double [] N_boundary;
        private double[] D_boundary;
        private double[] points;
        private int[] global_index;
        private int[] N_i_index;
        private int[] D_i_index;
        private int K;


        public FEM(double[] T, int N, double[] N_boundary, double[] D_boundary) //N={1,2}, T speichert die Punkte der Triangulation, 1D: 2N, KONSTRUKTOR
        {
            this.T = T;
            this.N = N;
            this.N_boundary=N_boundary;
            this.D_boundary=D_boundary;
            K = T.Length / 6;
            points = point(T);
            global_index = global_indices(T);
            N_i_index = set_i_N(T);
            D_i_index = set_D(global_index,N_i_index);
        }

        public double[] point(double[] T)
        {
            double[] points_1 = new double[2];
            points_1[0] = T[0];
            points_1[1] = T[1];
            for (int i = 2; i < T.Length; i = i + 2)
            {
                int k = 0;
                for (int j = 0; j < points_1.Length; j = j + 2)
                {
                    if (T[i] == points_1[j] & T[i + 1] == points_1[j + 1])
                    {
                        k++;
                    }
                }
                if (k == 0)
                {
                    Array.Resize(ref points_1, points_1.Length + 2);
                    points_1[points_1.Length - 2] = T[i];
                    points_1[points_1.Length - 1] = T[i + 1];
                }
            }
            return points_1;
        }

        public int[] global_indices(double[] T)
        {
            int[] global_indices = new int[T.Length/2];
            for( int i=0; i < T.Length; i += 2)
            {
                for( int j=0; j < points.Length; j+=2)
                {
                    if (T[i] == points[j] & T[i + 1] == points[j + 1])
                        {
                        global_indices[i/2] = j/2;
                        }
                }
            }
            return(global_indices);
        }

        public int[] set_i_N(double[] T)
        {
            int[] set_i_N = new int[0];
            for( int i=0; i<T.Length; i += 2)
            {
                int k = 0;
                for (int j = 0; j < D_boundary.Length; j += 2)
                {
                    if (T[i] == D_boundary[j] & T[i + 1] == D_boundary[j + 1])
                    {
                        k++;
                    }
                }
                 
                if(k==0) 
                {
                    int l = 0;
                    for (int m = 0; m < set_i_N.Length; m++)
                    {
                        if (global_index[i / 2] == set_i_N[m])
                        {
                            l++;
                        }
                    }
                    if (l == 0)
                    {
                        Array.Resize(ref set_i_N, set_i_N.Length + 1);
                        set_i_N[set_i_N.Length - 1] = global_index[i / 2];
                    }
                }

            }
            return set_i_N;
        }

        public int [] set_D(int[] global_index, int[] N_i_index)
        {
            int[] set_D = new int[0];
            for(int i=0; i<global_index.Length; i++)
            {
                int m = 0;
                for (int j = 0; j < N_i_index.Length; j++)
                {
                    if (global_index[i] == N_i_index[j])
                    {
                        m++;
                    }
                    for (int k = 0; k < set_D.Length; k++)
                    {
                        if (set_D[k] == global_index[i])
                        {
                            m++;
                        }
                    }
                }
                if (m == 0)
                {
                    Array.Resize(ref set_D, set_D.Length + 1);
                    set_D[set_D.Length - 1] = global_index[i];
                }
            }
            return set_D;
        }
 

        public void print_points()
        {
            for(int i=0;i<points.Length; i+=2)
            {
                Console.WriteLine("("+points[i]+"," +points[i+1]+")");

            }
        }
        public void print_global_indeces()
        {
            for (int i = 0; i < global_index.Length; i++)
            {
                Console.WriteLine("Punkt " + "("+T[2 * i]+","+T[2*i+1]+") hat Index " +global_index[i]);

            }

        }

        public void print_set_i_N()
        {
            Console.WriteLine("Neumann + Interior");
            for (int i = 0; i < N_i_index.Length; i++)
            {
                Console.WriteLine("Index "+N_i_index[i]);
            }

        }
        public void print_set_D()
        {
            Console.WriteLine("Dirichlet");
            for (int i = 0; i < D_i_index.Length; i++)
            {
                Console.WriteLine("Index " + D_i_index[i]);
            }

        }
        public void number_triangles()
        {
            Console.WriteLine(K);
        }

    }

}
