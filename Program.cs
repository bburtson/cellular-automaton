using System;

namespace AutomatonJustBecause_Burtson_2017
{
    internal class Program
    {
        private static readonly Automaton Auto = new Automaton();
        private static int _rule = -1;
        private static string spoilerMessage = "Psst! type 'spoiler' for a list of my favorite generations \n don't forget to scroll up you may need to adjust your console size! \n Cellular automaton by Brett Burtson Enjoy! :D \n";

        private static void Main(string[] args)
        {
            while (_rule < 0)
            {
                Console.WriteLine(spoilerMessage);
                Console.WriteLine($"Enter Rule (0 - 225):");

                var input = Console.ReadLine();

                spoilerMessage = "";

                if (input.Contains("spoiler"))
                {
                    Console.WriteLine("57, 129");
                    continue;
                }

                if (!int.TryParse(input, out _rule)) continue;

                if (Auto.SetRule(_rule))
                {
                    PrintResult();

                    _rule = -1;
                }

                else _rule = -1;
            }
        }

        private static void PrintResult()
        {
            Auto.ResetFirstGen();

            for (var i = 0; i < 85; i++)
            {
                Console.WriteLine(Auto.ToStringCurrentGen());
                Auto.PropagateNewGeneration();
            }
            Console.WriteLine($"\n> You entered: {_rule}");
        }
    }
}
