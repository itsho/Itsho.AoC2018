using Itsho.AoC2018.Solutions;
using System;
using System.Linq;

namespace Itsho.AoC2018.Tests
{
	public static class Tester
	{
		public static void TestDay01Part1()
		{
			var intPart1 = Day01Solution.GetPart1("+1 -2 +3 +1".Split(' '));
			NUnit.Framework.Assert.AreEqual(3, intPart1);
		}

		public static void TestDay01Part2()
		{
			NUnit.Framework.Assert.AreEqual(2,Day01Solution.GetPart2("+1,-2,+3,+1".Split(',')));
			NUnit.Framework.Assert.AreEqual(0,Day01Solution.GetPart2("+1,-1".Split(',')));
			NUnit.Framework.Assert.AreEqual(10,Day01Solution.GetPart2("+3,+3,+4,-2,-4".Split(',')));
			NUnit.Framework.Assert.AreEqual(5,Day01Solution.GetPart2("-6,+3,+8,+5,-6".Split(',')));
			NUnit.Framework.Assert.AreEqual(14,Day01Solution.GetPart2("+7,+7,-2,-7,-4".Split(',')));
		}

		public static void TestDay02Part1()
		{
			//	"abcdef"
			//	"bababc"	2a, 3b (both)
			//	"abbcde"	2b
			//	"abcccd"	     3c
			//	"aabcdd"	2a2d     (count ONE)
			//	"abcdee"	2e
			//	"ababab"	    3a3b (count one)
			var testInput = @"abcdef bababc abbcde abcccd aabcdd abcdee ababab";

			NUnit.Framework.Assert.AreEqual(2, Day02Solution.CountLetters("bababc")['a']);
			NUnit.Framework.Assert.AreEqual(3, Day02Solution.CountLetters("bababc")['b']);
			NUnit.Framework.Assert.AreEqual(3, Day02Solution.CountLetters("ababab")['a']);
			NUnit.Framework.Assert.AreEqual(3, Day02Solution.CountLetters("ababab")['b']);

			NUnit.Framework.Assert.AreEqual(12, Day02Solution.GetPart1(testInput.Split(' ')));
		}

		public static void TestDay02Part2()
		{
			{
				var boxTest = @"abcde fghij klmno pqrst fguij axcye wvxyz";
				NUnit.Framework.Assert.AreEqual("fgij", Day02Solution.GetPart2(boxTest.Split(' ')));
			}

			{
				var boxTest = @"aaaaaaaaaaaaaaaa
aaaaaaa1aeaaaaab
aaaaaaa2aeaaaaac
aaaaaaa3aeaaaaad
aaaaaaa4aeaaaaae
aaaaaaa4aeaaaaaf".Split(Environment.NewLine.ToCharArray());
				NUnit.Framework.Assert.AreEqual("aaaaaaa4aeaaaaa", Day02Solution.GetPart2(boxTest));
			}
		}

		public static void TestDay03Part1()
		{
			{
				var claim = @"#123 @ 3,2: 5x4";

				var matrixActual = Day03Solution.InitMatrix(11);

				bool hasOverlap;
				Day03Solution.ApplyClaim(ref matrixActual, claim, out hasOverlap);
				var matrixExpected = new char[11, 11]
				{
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','#','#','#','#','#','.','.','.'},
					{'.','.','.','#','#','#','#','#','.','.','.'},
					{'.','.','.','#','#','#','#','#','.','.','.'},
					{'.','.','.','#','#','#','#','#','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'},
					{'.','.','.','.','.','.','.','.','.','.','.'}
				};

				//var expectedVisual = Day03Solution.VisualizeMatrix(ref matrixExpected);
				//var actualVisual = Day03Solution.VisualizeMatrix(ref matrixActual);

				NUnit.Framework.Assert.AreEqual(matrixExpected, matrixActual);
			}

			{
				var claims =
@"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

				var actual = Day03Solution.GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
				NUnit.Framework.Assert.AreEqual(4, actual);
			}

			{
				var claims =
@"#1 @ 1,3: 3x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

				var actual = Day03Solution.GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
				NUnit.Framework.Assert.AreEqual(2, actual);
			}

			{
				var claims =
					@"#1 @ 1,3: 2x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

				var actual = Day03Solution.GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
				NUnit.Framework.Assert.AreEqual(0, actual);
			}
		}

		public static void TestDay03Part2()
		{
			{
				var claims =
					@"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

				var actual = Day03Solution.GetPart2(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
				NUnit.Framework.Assert.AreEqual(3, actual);
			}
		}

		public static void TestDay04Part1()
		{
			var input = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up".Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

				input.Sort();

			var dt = Day04Solution.PrepareInput(input);

			var actual = Day04Solution.GetPart1(dt);


			NUnit.Framework.Assert.AreEqual(240, actual);
		}

		public static void TestDay04Part2()
		{
			{
				var input = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up".Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

				input.Sort();

				var dt = Day04Solution.PrepareInput(input);
				var actual = Day04Solution.GetPart2(dt);
				NUnit.Framework.Assert.AreEqual(4455, actual);
			}

			{
				var input = @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up
[1518-11-05 23:58] Guard #50 begins shift
[1518-11-06 00:15] falls asleep
[1518-11-06 00:16] wakes up
[1518-11-06 23:58] Guard #50 begins shift
[1518-11-07 00:15] falls asleep
[1518-11-07 00:16] wakes up
[1518-11-07 23:58] Guard #50 begins shift
[1518-11-08 00:15] falls asleep
[1518-11-08 00:16] wakes up
[1518-11-08 23:58] Guard #50 begins shift
[1518-11-09 00:15] falls asleep
[1518-11-09 00:16] wakes up".Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

				input.Sort();

				var dt = Day04Solution.PrepareInput(input);
				var actual = Day04Solution.GetPart2(dt);
				NUnit.Framework.Assert.AreEqual(50*15, actual);
			}

		}

		public static void TestDay05Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay05Part2()
		{
			throw new NotImplementedException();
		}

		public static void TestDay06Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay06Part2()
		{
			throw new NotImplementedException();
		}

		public static void TestDay07Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay07Part2()
		{
			throw new NotImplementedException();
		}

		public static void TestDay08Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay08Part2()
		{
			throw new NotImplementedException();
		}

		public static void TestDay09Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay09Part2()
		{
			throw new NotImplementedException();
		}

		public static void TestDay10Part1()
		{
			throw new NotImplementedException();
		}

		public static void TestDay10Part2()
		{
			throw new NotImplementedException();
		}
	}
}