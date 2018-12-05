using Itsho.AoC2018.Infra;
using Itsho.AoC2018.Solutions;
using System;
using System.IO;
using System.Linq;

namespace Itsho.AoC2018
{
    public class Program
    {
        private static void Main(string[] args)
        {
            RunDay01();
            RunDay02();
            RunDay03();
            RunDay04();
            RunDay05();
            RunDay06();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region Private methods

        private static void RunDay01()
        {
            Console.WriteLine("------ Day 1 ------");

            Console.WriteLine("Tests...");
            Day01Solution.TestDay01Part1();
            Day01Solution.TestDay01Part2();

            Console.WriteLine("Actual Run...");
            var strInputDay01 = File.ReadAllLines(@"inputs\DAY01.txt");
            Extensions.ConsoleWriteLineTimed("Day1 part1 - ", () => Day01Solution.GetPart1(strInputDay01).ToString());
            Extensions.ConsoleWriteLineTimed("Day1 part2 - ", () => Day01Solution.GetPart2(strInputDay01).ToString());
        }

        private static void RunDay02()
        {
            Console.WriteLine("------ Day 2 ------");

            Console.WriteLine("Tests...");
            Day02Solution.TestDay02Part1();
            Day02Solution.TestDay02Part2();

            Console.WriteLine("Actual Run...");
            var strInputDay02 = File.ReadAllLines(@"inputs\DAY02.txt");
            Extensions.ConsoleWriteLineTimed("Day2 part1 - ", () => Day02Solution.GetPart1(strInputDay02).ToString());
            Extensions.ConsoleWriteLineTimed("Day2 part2 - ", () => Day02Solution.GetPart2(strInputDay02).ToString());
        }

        private static void RunDay03()
        {
            Console.WriteLine("------ Day 3 ------");

            Console.WriteLine("Tests...");
            Day03Solution.TestDay03Part1();
            Day03Solution.TestDay03Part2();

            Console.WriteLine("Actual Run...");
            var strInputDay03 = File.ReadAllLines(@"inputs\DAY03.txt");
            Extensions.ConsoleWriteLineTimed("Day3 part1 - ", () => Day03Solution.GetPart1(strInputDay03, 1050).ToString());
            Extensions.ConsoleWriteLineTimed("Day3 part2 - ", () => Day03Solution.GetPart2(strInputDay03, 1050).ToString());
        }

        private static void RunDay04()
        {
            Console.WriteLine("------ Day 4 ------");

            Console.WriteLine("Tests...");
            Day04Solution.TestDay04Part1();
            Day04Solution.TestDay04Part2();

            Console.WriteLine("Actual Run...");
            var strInputDay04 = File.ReadAllLines(@"inputs\DAY04.txt");
            var sortedInput = strInputDay04.ToList();
            sortedInput.Sort();
            var preparedInput = Day04Solution.PrepareInput(sortedInput);

            Extensions.ConsoleWriteLineTimed("Day4 part1 - ", () => Day04Solution.GetPart1(preparedInput).ToString());
            Extensions.ConsoleWriteLineTimed("Day4 part2 - ", () => Day04Solution.GetPart2(preparedInput).ToString());
        }

        private static void RunDay05()
        {
            Console.WriteLine("------ Day 5 ------");

            Console.WriteLine("Tests...");
            Day05Solution.TestDay05Part1();
            Day05Solution.TestDay05Part2();

            Console.WriteLine("Actual Run...");
            var strInputDay05 = File.ReadAllText(@"Inputs\DAY05.txt");
            Extensions.ConsoleWriteLineTimed("Day5 part1 - ", () => Day05Solution.GetPart1(strInputDay05).ToString());
            Extensions.ConsoleWriteLineTimed("Day5 part2 - ", () => Day05Solution.GetPart2(strInputDay05).ToString());
        }

        private static void RunDay06()
        {
            Console.WriteLine("------ Day 6 ------");

            Console.WriteLine("Tests...");
            Day06Solution.TestPart1();
            //Day06Solution.TestPart2();

            Console.WriteLine("Actual Run...");
            var strInputDay06 = File.ReadAllLines(@"inputs\DAY06.txt");
            Extensions.ConsoleWriteLineTimed("Day6 part1 - ", () => Day06Solution.GetPart1(strInputDay06).ToString());
            //Extensions.ConsoleWriteLineTimed("Day6 part2 - ", () => Day06Solution.GetPart2(strInputDay06).ToString());
        }

        #endregion Private methods
    }
}