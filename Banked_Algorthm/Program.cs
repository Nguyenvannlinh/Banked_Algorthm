
using System;
using System.Text;

namespace banked_alogorthm
{
    class program
    {

        static int[,] need(int n, int m, int[,] y, int[,] z)
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
        static bool check(int  n)
        {
            return n < 0;
        }
        static int[] input()
        {
            int m,n;
            while (true)
            {
                Console.Write("Số lượng tiến trình : ");
                 n = int.Parse(Console.ReadLine());
                if (!check(n)) { break; }
            }
            while (true)
            {
                
                Console.Write("Số lượng tài nguyên : ");
                m = int.Parse(Console.ReadLine());
                if (!check(m)) { break; }
            }
            int[] avil = new int[n];
            for (int i = 0;i < m; i++)
            {
                while (true)
                {
                    Console.Write($"Tài nguyên mỗi loại R{i} :");
                    avil[i] = int.Parse(Console.ReadLine());
                    if (!check(avil[i])) { break; }
                }
            }
            Console.Clear();
            int[,] max = new int[n,m];
            for (int i = 0; i < n; i++)
            {
                for(int j = 0;j < m; j++)
                {
                    Console.WriteLine($"Tiến trình P{i}: ");
                    while (true)
                    {
                        Console.Write($"Tài nguyên tối đa của tiến trình[{i}, {j}] :");
                        max[i, j] = int.Parse(Console.ReadLine());
                        if ((max[i,j] <= avil[j]) && !check(max[i, j])) { break; }
                    }
                }
            }
            Console.Clear();
            int[,] allo = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.WriteLine($"Tiến trình P{i}: ");
                    while (true)
                    {
                        Console.Write($"Tài nguyên hiện có của tiến trình[{i}, {j}] :");
                        allo[i, j] = int.Parse(Console.ReadLine());
                        if ((allo[i, j] <= max[i, j]) && !check(allo[i, j])) { break; }
                    }
                }
            }
            if(Safa(n, m, need(n, m, max, allo), allo, available(n, m, allo, avil)).Length == n)
            {
                return Safa(n, m, need(n, m, max, allo), allo, available(n, m, allo, avil));
            }
            return new int[] {};
           
        }
        static void Hethong(int[] x)
        {
            Console.Clear();
            if (x.Length != 0)
            {
                Console.WriteLine("hệ thống an toàn");
                Console.Write("dãy an toàn là :");
                string a = "";
                for (int i = 0; i < x.Length; i++)
                {
                    a += $"P{x[i]} -->";
                }
                if (a.EndsWith(" -->"))
                {
                    a = a.Substring(0, a.Length - 4);
                }
                Console.WriteLine($"{a}");
            }
            else { Console.WriteLine("hệ thông đang ở trạng tháy không an toàn "); }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            int[] a = input();
            Hethong(a);
        }
    }
}