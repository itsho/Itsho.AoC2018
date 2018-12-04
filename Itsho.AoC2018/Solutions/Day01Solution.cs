using System.Collections.Generic;
using System.IO;

namespace Itsho.AoC2018.Solutions
{
	public static class Day01Solution
	{
		public static int GetPart1(string[] riddleSource)
		{
			var location = 0;
			for (int i = 0; i < riddleSource.Length; i++)
			{
				location += ParseItem(riddleSource[i]);
			}

			return location;
		}

		private static int ParseItem(string s)
		{
			int result = 0;
			if (s.StartsWith("+"))
			{
				if (!int.TryParse(s.Replace("+",""), out result))
				{
					throw  new InvalidDataException();
				}
			}
			else if (s.StartsWith("-"))
			{
				if (!int.TryParse(s.Replace("-", ""), out result))
				{
					throw new InvalidDataException();
				}

				result *= -1;
			}

			return result;
		}

		public static int GetPart2(string[] riddleSource)
		{
			int frequency = 0;
			var numbersEncounter = new HashSet<int>();

			var foundSameFreq = false;
			while (!foundSameFreq)
			for (int i = 0; i < riddleSource.Length; i++)
			{
				var changeValue = GetPart1(new []{riddleSource[i]});
				frequency += changeValue;

				if (numbersEncounter.Contains(frequency))
				{
					foundSameFreq = true;
					return frequency;
				}

				numbersEncounter.Add(frequency);
			}

			return frequency;
		}
	}
}