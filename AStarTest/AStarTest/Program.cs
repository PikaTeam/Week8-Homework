using System;

namespace AStarTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting.");

            new TestAStar().Run();

            Console.WriteLine("Ending.");
            Console.Read(); // to halt, if reached that point, then all test passed.
            
        }
    }
}
