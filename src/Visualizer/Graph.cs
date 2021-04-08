using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Visualizer
{

    public class Graph
    {
        private int nVertices; // simpen n vertices
        private string[] vertices; // simpen label vertices
        private bool[] visitedVertices; // simpen isVisited vertices 
        private int[,] adj; // adjancency matrix 
        private Stack<int> s; // stack buat DFS
        public Graph(int nV, string[] v)
        {
            nVertices = nV;
            s = new Stack<int>();

            vertices = new string[nV];
            visitedVertices = new bool[nV];
            for (int i = 0; i < nV; i++)
            {
                vertices[i] = v[i];
                visitedVertices[i] = false;
            }

            adj = new int[nV, nV];
            for (int i = 0; i < nV; i++)
            {
                for (int j = 0; i < nV; i++)
                {
                    adj[i, j] = 0;
                }
            }
        }

        public void addEdges(string c, string v) // nambahin sisi dari c ke v
        {
            int idxC = -1;
            int idxV = -1;

            for (int i = 0; i < nVertices; i++)
            {
                if (vertices[i] == c) idxC = i;
            }

            for (int j = 0; j < nVertices; j++)
            {
                if (vertices[j] == v) idxV = j;
            }
            if (idxC != -1 && idxV != -1)
            {
                adj[idxC, idxV] = 1;
                adj[idxV, idxC] = 1;
            }
        }

        public void showVertice(int v)
        {
            Console.Write(vertices[v] + " ");
            //Console.WriteLine("");
        }

        public int getAdjUnvisited(int v)
        {
            for (int i = 0; i < nVertices; i++)
            {
                if (adj[v, i] == 1 && visitedVertices[i] == false)
                {
                    return i;

                }
            }
            return -1;
        }

        public string[] DFS(string from, string to)
        {
            Console.WriteLine("Simpul yang ditelusuri: ");
            int idxF = -1;
            int idxT = -1;
            bool pathFound = false;
            for (int i = 0; i < nVertices; i++) // indeks dari titik awal
            {
                if (vertices[i] == from) idxF = i;
                if (vertices[i] == to) idxT = i;
            }
            visitedVertices[idxF] = true;
            showVertice(idxF);
            s.Push(idxF);
            while (s.Count() != 0 && pathFound == false)
            {
                int v = getAdjUnvisited(s.Peek());

                if (v == -1)
                {
                    s.Pop();
                }
                else
                {
                    visitedVertices[v] = true;
                    showVertice(v);
                    s.Push(v);
                    if (v == idxT)
                    {
                        Console.WriteLine("");
                        pathFound = true;
                    }
                }

            }

            int[] arr = s.ToArray();
            string[] res = new string[arr.Count()];
            int counter = arr.Count() - 1;
            for (int j = 0; j < arr.Count(); j++)
            {
                res[j] = vertices[arr[counter]];
                counter--;
            }
            Console.WriteLine("===============================");
            if (s.Count == 0)
            {
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
                resetGraph();
                return res;
            }
            Console.WriteLine("Nama akun: " + res[0] + " dan " + res[res.Count() - 1]);
            for (int j = 0; j < res.Count(); j++)
            {
                if (j < res.Count() - 1)
                    Console.Write(res[j] + " → ");
                else
                    Console.WriteLine(res[j]);
            }
            Console.WriteLine("Koneksi Derajat " + (res.Count() - 2));
            resetGraph();
            return res;
        }

        public string[] BFS(string from, string to)
        {
            int idxF = -1;
            int idxT = -1;
            bool pathFound = false;

            for (int i = 0; i < nVertices; i++) // get the index for the starting point
            {
                if (vertices[i] == from) idxF = i;
                if (vertices[i] == to) idxT = i;
            }
            visitedVertices[idxF] = true; // make starting point visited true
            Queue bangkit = new Queue();
            bangkit.Enqueue(idxF);
            int[] Pembangkit = new int[nVertices];
            while (bangkit.Count != 0 && pathFound == false)
            {
                int v = (int)bangkit.Dequeue();
                Console.Write("\nDiperiksa: ");
                showVertice(v);
                //Console.WriteLine("");
                int b;
                Console.Write("\nDibangkitkan: ");
                do
                {
                    b = getAdjUnvisited(v);
                    if (getAdjUnvisited(v) != -1)
                    {
                        bangkit.Enqueue(b);
                        visitedVertices[b] = true;
                        Pembangkit[b] = v;
                        showVertice(b);
                        if (b == idxT)
                        {
                            pathFound = true;
                        }
                    }

                } while (b != -1 && !pathFound);
            }
            int k = idxT;
            int temp;
            s.Push(k);
            while (k != idxF)
            {
                temp = Pembangkit[k];
                s.Push(temp);
                k = temp;
            }
            int[] arr = s.ToArray();
            string[] res = new string[arr.Count()];
            int counter = arr.Count() - 1;
            for (int j = arr.Count() - 1; j > -1; j--)
            {
                res[j] = vertices[arr[counter]];
                counter--;
            }
            Console.WriteLine("\n\n===============================");
            if (!pathFound)
            {
                Console.WriteLine("Tidak ada jalur koneksi yang tersedia\nAnda harus memulai koneksi baru itu sendiri.");
                resetGraph();
                return res;
            }
            for (int j = 0; j < res.Count(); j++)
            {
                if (j < res.Count() - 1)
                    Console.Write(res[j] + " → ");
                else
                    Console.WriteLine(res[j]);
            }
            Console.WriteLine("Koneksi Derajat " + (res.Count() - 2));
            resetGraph();
            return res;
        }

        public List<string>[] FriendRecommendation(string akun)
        {
            Console.WriteLine("Daftar Rekomendasi Teman untuk akun " + akun);
            int idxF = -1;

            for (int i = 0; i < nVertices; i++) // get the index for the starting point
            {
                if (vertices[i] == akun) idxF = i;
            }
            visitedVertices[idxF] = true; // make starting point visited true
            Queue bangkit = new Queue();
            bangkit.Enqueue(idxF);
            List<string>[] Result = new List<string>[nVertices];
            for (int i = 0; i < nVertices; i++)
            {
                Result[i] = new List<string>();
                Result[i].Add(vertices[i]);
            }
            int v = (int)bangkit.Dequeue();
            Console.Write("Diperiksa: ");
            showVertice(v);
            int b;
            Console.Write("\nDibangkitkan: ");
            do
            {
                b = getAdjUnvisited(v);
                if (getAdjUnvisited(v) != -1)
                {
                    bangkit.Enqueue(b);
                    visitedVertices[b] = true;
                    showVertice(b);
                }
            } while (b != -1);
            List<int> reset = new List<int>();
            while (bangkit.Count != 0)
            {
                v = (int)bangkit.Dequeue();
                Console.Write("\nDiperiksa: ");
                showVertice(v);
                Console.Write("\nDibangkitkan: ");
                do
                {
                    b = getAdjUnvisited(v);
                    if (getAdjUnvisited(v) != -1)
                    {
                        visitedVertices[b] = true;
                        Result[b].Add(vertices[v]);
                        reset.Add(b);
                        showVertice(b);
                    }
                } while (b != -1);
                for (int f = 0; f < reset.Count; f++)
                {
                    visitedVertices[reset[f]] = false;
                }
            }
            Console.WriteLine("\n\n===============================");
            Array.Sort<List<string>>(Result, new Comparison<List<string>>(
                  (i1, i2) => (i2.Count).CompareTo(i1.Count)));
            for (int l = 0; l < Result.Length; l++)
            {
                if (Result[l].Count > 1)
                {
                    Console.WriteLine("Nama akun: " + Result[l][0]);
                    Console.WriteLine((Result[l].Count - 1) + " mutual friend:");
                    for (int p = 1; p < Result[l].Count; p++)
                    {
                        Console.WriteLine(Result[l][p]);
                    }
                }
            }
            resetGraph();
            return Result;
        }

        void resetGraph()
        {
            s.Clear();
            for (int i = 0; i < nVertices; i++)
            {
                visitedVertices[i] = false;
            }
        }
    }

}
