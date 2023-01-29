using System;
using System.IO;

namespace Feleves_HAZI_NEW
{
    class Program
    {
        static void Main(string[] args)
        {
            Napirend[] nr = new Napirend[5];
            if (nr[0] == null)
            {
                Console.WriteLine("null");
            }
            BeosztasKeszito bk = new BeosztasKeszito("rendez.be");
            Console.WriteLine("\n\n");
        }
    }
}
