using Itsho.AoC2018.Solutions;
using Itsho.AoC2018.Tests;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Itsho.AoC2018
{
	public class Program
	{
		private static void Main(string[] args)
		{
			#region Day 01

			/*
			Console.WriteLine("------ Day 1 ------");

			Console.WriteLine("Tests...");
			Tester.TestDay01Part1();
			Tester.TestDay01Part2();

			Console.WriteLine("Actual Run...");
			var strInputDay01 = File.ReadAllLines(@"RiddleSources\DAY01.txt");
			ConsoleWriteLineTimed("Day1 part1 - ", () => Day01Solution.GetPart1(strInputDay01).ToString());
			ConsoleWriteLineTimed("Day1 part2 - ", () => Day01Solution.GetPart2(strInputDay01).ToString());
			*/

			#endregion Day 01

			#region Day 02

			/*
			Console.WriteLine("------ Day 2 ------");

			Console.WriteLine("Tests...");
			Tester.TestDay02Part1();
			Tester.TestDay02Part2();

			Console.WriteLine("Actual Run...");
			var strInputDay02 = File.ReadAllLines(@"RiddleSources\DAY02.txt");
			ConsoleWriteLineTimed("Day2 part1 - ", () => Day02Solution.GetPart1(strInputDay02).ToString());
			ConsoleWriteLineTimed("Day2 part2 - ", () => Day02Solution.GetPart2(strInputDay02).ToString());
			*/

			#endregion Day 02

			#region Day 03

			/*
			Console.WriteLine("------ Day 3 ------");

			Console.WriteLine("Tests...");
			Tester.TestDay03Part1();
			Tester.TestDay03Part2();

			Console.WriteLine("Actual Run...");
			var strInputDay03 = File.ReadAllLines(@"RiddleSources\DAY03.txt");
			ConsoleWriteLineTimed("Day3 part1 - ", () => Day03Solution.GetPart1(strInputDay03,1050).ToString());
			ConsoleWriteLineTimed("Day3 part2 - ", () => Day03Solution.GetPart2(strInputDay03,1050).ToString());
			*/

			#endregion Day 03

			#region Day 04

			Console.WriteLine("------ Day 4 ------");

			Console.WriteLine("Tests...");
			Tester.TestDay04Part1();
			Tester.TestDay04Part2();

			Console.WriteLine("Actual Run...");
			var strInputDay04 = File.ReadAllLines(@"RiddleSources\DAY04.txt");
			var sortedInput = strInputDay04.ToList();
			sortedInput.Sort();
			var preparedInput = Day04Solution.PrepareInput(sortedInput);

			ConsoleWriteLineTimed("Day4 part1 - ", () => Day04Solution.GetPart1(preparedInput).ToString());
			ConsoleWriteLineTimed("Day4 part2 - ", () => Day04Solution.GetPart2(preparedInput).ToString());

			#endregion Day 04



			#region Day 05

			/*
			Console.WriteLine("------ Day 5 ------");

			Console.WriteLine("Tests...");
			Tester.TestDay05Part1();
			//Tester.TestDay05Part2();

			Console.WriteLine("Actual Run...");
			var strInputDay05 = File.ReadAllLines(@"RiddleSources\DAY05.txt");
			ConsoleWriteLineTimed("Day5 part1 - ", () => Day03Solution.GetPart1(strInputDay05).ToString());
			//ConsoleWriteLineTimed("Day5 part2 - ", () => Day03Solution.GetPart2(strInputDay05).ToString());
			*/

			#endregion Day 05

			Console.ReadKey();
		}

		public static void ConsoleWriteLineTimed(string title, Func<string> actionToRun)
		{
			var sw = new Stopwatch();
			sw.Start();
			var strResult = actionToRun();
			sw.Stop();
			Console.WriteLine(title + "\t" + strResult + "\t (" + sw.Elapsed.ToString() + ")");
		}
	}
}