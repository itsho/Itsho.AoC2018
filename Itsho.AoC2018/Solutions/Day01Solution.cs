using System.Collections.Generic;
using System.IO;

namespace Itsho.AoC2018.Solutions
{
    public static class Day01Solution
    {
        #region Tests

        public static void TestDay01Part1()
        {
            var intPart1 = GetPart1("+1 -2 +3 +1".Split(' '));
            NUnit.Framework.Assert.AreEqual(3, intPart1);
        }

        public static void TestDay01Part2()
        {
            NUnit.Framework.Assert.AreEqual(2, GetPart2("+1,-2,+3,+1".Split(',')));
            NUnit.Framework.Assert.AreEqual(0, GetPart2("+1,-1".Split(',')));
            NUnit.Framework.Assert.AreEqual(10, GetPart2("+3,+3,+4,-2,-4".Split(',')));
            NUnit.Framework.Assert.AreEqual(5, GetPart2("-6,+3,+8,+5,-6".Split(',')));
            NUnit.Framework.Assert.AreEqual(14, GetPart2("+7,+7,-2,-7,-4".Split(',')));
        }

        #endregion Tests

        public static int GetPart1(string[] input)
        {
            var location = 0;
            for (int i = 0; i < input.Length; i++)
            {
                location += ParseItem(input[i]);
            }

            return location;
        }

        private static int ParseItem(string s)
        {
            int result = 0;
            if (s.StartsWith("+"))
            {
                if (!int.TryParse(s.Replace("+", ""), out result))
                {
                    throw new InvalidDataException();
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

        public static int GetPart2(string[] input)
        {
            int frequency = 0;
            var numbersEncounter = new HashSet<int>();
            numbersEncounter.Add(0);
            var foundSameFreq = false;
            while (!foundSameFreq)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var changeValue = GetPart1(new[] { input[i] });
                    frequency += changeValue;

                    if (numbersEncounter.Contains(frequency))
                    {
                        foundSameFreq = true;
                        return frequency;
                    }

                    numbersEncounter.Add(frequency);
                }
            }

            return frequency;
        }
    }
}