
using System;
using System.Text;

namespace banked_alogorthm
{
    class program
    {
        public int m, n;
        public int[,] allo, max, need, request;
        public int[] avil;
        static int[,] Need(int n, int m, int[,] y, int[,] z)
        {
            int[,] x = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    x[i, j] = y[i, j] - z[i, j];

                }
            }
            return x;
        }
        static int[] available(int n, int m, int[,]x, int[]y)
        {
            for (int j = 0; j < m; j++)
            {
                int tong = 0;
                for (int i = 0; i < n; i++)
                {
                    tong += x[i, j];
                }
                y[j] -= tong;
            }
            return y;
        }

        static int[] Safa (int n, int m, int[,] x,int [,]y ,int[] z)
        {
            bool[] finish = new bool[n];
            int[] safa = new int[n];
            int id = 0;
            for (int i = 0; i < n; i++)
            {
                finish[i] = false;
            }
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (finish[i] == false)
                    {
                        bool ok = true; // báo hiệu cấp tài nguyên
                        for (int j = 0; j < m; j++)
                        {
                            if (x[i, j] > z[j])
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            finish[i] = true;
                            safa[id++] = i;
                            for (int j = 0; j < m; j++)
                            {
                                z[j] += y[i, j];
                            }
                        }
                    }
                }
            }
            return safa;
        }
                                                // avail  allo     need       request
        static int[] Request(int n, int x, int m, int[]b, int[,]y , int[,] z, int[,] a)
        {
            bool[] test = new bool[m];
            int check = 0;
            for (int j = 0; j < m; j++)
            {
                if (a[x, j] <= b[j])
                {
                    test[j] = true;
                    check++;
                }
            }
            if (check == m)
            {
                for (int j = 0; j < m; j++)
                {
                    y[x, j] += a[x, j];
                    z[x, j] -= a[x, j];
                    b[j] -= a[x, j];

                }
            }
            int [] nao = Safa(n, m, z, y, b);
            return nao;
            
        }
        static bool Check(int  n)
        {
            return n < 0;
        }
        static void input()
        {
            int m,n;
            while (true)
            {
                Console.Write("Số lượng tiến trình : ");
                 n = int.Parse(Console.ReadLine());
                if (!Check(n)) { break; }
            }
            while (true)
            {
                
                Console.Write("Số lượng tài nguyên : ");
                m = int.Parse(Console.ReadLine());
                if (!Check(m)) { break; }
            }
            int[] avil = new int[n];
            for (int i = 0;i < m; i++)
            {
                while (true)
                {
                    Console.Write($"Tài nguyên mỗi loại R{i} :");
                    avil[i] = int.Parse(Console.ReadLine());
                    if (!Check(avil[i])) { break; }
                }
            }
            Console.Clear();
            int[,] max = new int[n,m];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Tiến trình P{i}: ");
                for (int j = 0;j < m; j++)
                {
                    
                    while (true)
                    {
                        Console.Write($"Tài nguyên tối đa của tiến trình[{i}, {j}] :");
                        max[i, j] = int.Parse(Console.ReadLine());
                        if ((max[i,j] <= avil[j]) && !Check(max[i, j])) { break; }
                    }
                }
            }
            Console.Clear();
            int[,] allo = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Tiến trình P{i}: ");
                for (int j = 0; j < m; j++)
                {
                    
                    while (true)
                    {
                        Console.Write($"Tài nguyên hiện có của tiến trình[{i}, {j}] :");
                        allo[i, j] = int.Parse(Console.ReadLine());
                        if ((allo[i, j] <= max[i, j]) && !Check(allo[i, j])) { break; }
                    }
                }
            }
            int[,] request = new int[n, m];
            if(Safa(n, m, Need(n, m, max, allo), allo, available(n, m, allo, avil)).Length == n)
            {
                Console.WriteLine($"Hệ thông an toàn\nDãy an toàn là:{Hethong(Safa(n, m, Need(n, m, max, allo), allo, available(n, m, allo, avil)))}");
                Console.WriteLine("\nnhấn enter để request tài nguyên");
                Console.ReadKey();
                int x;
                Console.Clear();
                while (true)
                {
                    Console.Write("Nhập tiến trình  có yêu cầu tài nguyên: ");
                    x = int.Parse(Console.ReadLine());
                    if (!Check(x) && x < n) { break; }
                }
                for (int j = 0; j < m; j++)
                {

                    Console.WriteLine($"Tiến trình P{j}: ");
                    while (true)
                    {
                        Console.Write($"Tài nguyên tối đa của tiến trình[{x}, {j}] :");
                        request[0, j] = int.Parse(Console.ReadLine());
                        if ((request[x, j] <= Need(n, m, max, allo)[x, j]) && !Check(request[x, j])) { break; }
                    }

                }

                Console.WriteLine();
                Console.WriteLine($"Hệ thông được làm mới\nDãy an toàn là: {Hethong(Request(n, x, m, available(n, m, allo, avil), allo, Need(n, m, max, allo), request))}");
            }
            else { Console.WriteLine("Hệ thông không an toàn"); }
            

        }
        
        static string Hethong(int[] x)
        {
            Console.Clear();
            if (x.Length != 0)
            {
                string a = "";
                for (int i = 0; i < x.Length; i++)
                {
                    a += $"P{x[i]} -->";
                }
                if (a.EndsWith(" -->"))
                {
                    a = a.Substring(0, a.Length - 4);
                }
                return a;
            }
            else { return " "; }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            input();
        }
    }
}
