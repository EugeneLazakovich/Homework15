using System;
using System.Threading;

namespace ConsoleApp27
{
    class Shared
    {
        public Boolean flag = true;
        public void OneCust(string name)
        {
            Console.WriteLine(name + "'s buying hamburger");
            Thread.Sleep(3000);
        }
        public void isClose()
        {
            int rnd = new Random().Next(0, 9);
            if (rnd < 3)
            {
                Console.WriteLine("Mac is closed");
                flag = false;
            }
        }
    }
    class Customer
    {
        public string name;
        Semaphore sem;
        Shared shared;
        public Customer(string name, Shared shared, Semaphore sem)
        {
            this.name = name;
            this.shared = shared;
            this.sem = sem;
            new Thread(MyMethod).Start();
        }
        public void MyMethod()
        {
            sem.WaitOne();
            if (!shared.flag)
            {
                sem.Release();
            }
            else
            {
                shared.OneCust(name);
                shared.isClose();
                sem.Release();
            }            
        }
    }
    class Program
    {       
        static void Main(string[] args)
        {
            Shared shared = new Shared();
            Semaphore sem = new Semaphore(1, 1);
            new Customer("John", shared, sem);
            new Customer("Elvis", shared, sem);
            new Customer("Vasya", shared, sem);
            new Customer("Caren", shared, sem);
            new Customer("Lusien", shared, sem);                 
        }
    }
}
