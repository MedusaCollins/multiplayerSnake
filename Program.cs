using System;

namespace multiplayerSnake

{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();
            helper.Greet("Enes");
            Console.WriteLine("Eno babaa");
            Console.WriteLine("Kerooo babaaa");
            Helper sayi = new Helper();
            helper.sayi(3,5);
        }
    }
}