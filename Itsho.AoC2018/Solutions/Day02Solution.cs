using System.Collections.Generic;
using System.Linq;

namespace Itsho.AoC2018.Solutions
{
	public static class Day02Solution
	{
		public static int GetPart1(string[] riddleSource)
		{
			var twicers = 0;
			var thricers = 0;

			foreach (var riddleLine in riddleSource)
			{
				var countLetters = CountLetters(riddleLine);

				var innerTwicers = countLetters.Count(kvp => kvp.Value == 2);
				var innerThricers = countLetters.Count(kvp => kvp.Value == 3);

				if (innerTwicers >= 1)
				{
					twicers++;
				}
				if (innerThricers >= 1)
				{
					thricers++;
				}
			}

			return twicers*thricers;
		}

		public static Dictionary<char, int> CountLetters(string riddleLine)
		{
			var dictCounter = new Dictionary<char, int>();
			foreach (var chr in riddleLine)
			{
				if (dictCounter.ContainsKey(chr))
				{
					dictCounter[chr]++;
				}
				else
				{
					dictCounter.Add(chr,1);
				}
			}

			return dictCounter;
		}

		public static string GetPart2(string[] riddleSource)
		{
			var lstInput = riddleSource.ToList();
			lstInput.Sort();

			for (int i = 1; i < lstInput.Count; i++)
			{
				var line = lstInput[i];

				var diffCharIndex = GetDiffChar(line, lstInput[i - 1]);
				if (diffCharIndex != null)
				{
					// remove this char and return the line
					var result = line.ToList();
					result.RemoveAt(diffCharIndex.Value);
					return new string(result.ToArray());
				}
				
			}

			return null;

		}

		private static int? GetDiffChar(string line1, string line2)
		{
			int? foundChar = null;
			for (int chrIndex = 0; chrIndex < line1.Length; chrIndex++)
			{
				// if chars are the same
				if (line1[chrIndex] == line2[chrIndex])
				{
					continue;
				}

				// if this is the SECOND mismatch
				if (foundChar != null)
				{
					// we found more than one mis-match
					return null;
				}

				foundChar = chrIndex;

			}


			return foundChar;
		}
	}
}