using System;
using System.Collections.Generic;
using System.Threading;

namespace _15._2
{
    class WorkerProducer
    {
        public int MethodProducer(int count)
        {
            return count;
        }
    }
    class Worker
    {
        string name;
        int count;
        public int countWork = 0;
        WorkerProducer workProd;
        public Worker(WorkerProducer workProd, string name, int count)
        {
            this.workProd = workProd;
            this.name = name;
            this.count = count;
            new Thread(addWorker).Start();
        }
        public override string ToString()
        {
            return name + count + ": " + countWork + " golds";
        }
        public void addWorker()
        {
            workProd.MethodProducer(count);
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(1000);
                countWork += 3;
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Worker> workList = new List<Worker>();
            int count = 1;
            Boolean flag = true;
            int countGold = 1000;
            WorkerProducer workProd = new WorkerProducer();
            workList.Add(new Worker(workProd, "Worker ", count++));
            workList.Add(new Worker(workProd, "Worker ", count++));
            workList.Add(new Worker(workProd, "Worker ", count++));
            while (countGold > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    foreach (Worker list in workList)
                    {
                        Console.WriteLine(list);
                        countGold = countGold - 3;
                        if (countGold < 0)
                        {
                            flag = false;
                        }
                    }                    
                    Thread.Sleep(1000);
                    Console.Clear();
                    if (!flag) { break; }
                }
                if (!flag) { break; }
                workList.Add(new Worker(workProd, "Worker ", count++));
            }
            foreach (Worker list in workList)
            {
                Console.WriteLine(list);
            }
        }
    }
}
