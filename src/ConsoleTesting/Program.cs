using Events.IO.Domain.Events;
using System;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var occasion = new Occasion(
                "Venda de carros",
                DateTime.Now,
                DateTime.Now,
                true,
                0,
                true,
                "Chevrolett"
                );

            var occasion2 = new Occasion(
                "Venda de carros",
                DateTime.Now,
                DateTime.Now,
                true,
                0,
                true,
                "Chevrolett"
                );

            var occasion3 = occasion;

            // test validation
            var occasion4 = new Occasion(
                "",
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(-2),
                false,
                0,
                true,
                ""
                );

            Console.WriteLine(occasion.ToString());
            Console.WriteLine(occasion2.ToString());
            Console.WriteLine(occasion3.ToString());
            Console.WriteLine(occasion.Equals(occasion2));
            Console.WriteLine(occasion.Equals(occasion3));

            if (!occasion4.IsValid())
            {
                foreach (var item in occasion4.ValidationResult.Errors)
                {
                    Console.WriteLine($"Error: {item.ErrorMessage}"); 
                }
            }
        }
    }
}
