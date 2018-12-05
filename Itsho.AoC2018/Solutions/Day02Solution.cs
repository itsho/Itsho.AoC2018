using System;
using System.Collections.Generic;
using System.Linq;

namespace Itsho.AoC2018.Solutions
{
    public static class Day02Solution
    {
        #region Tests

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

            NUnit.Framework.Assert.AreEqual(2, CountLetters("bababc")['a']);
            NUnit.Framework.Assert.AreEqual(3, CountLetters("bababc")['b']);
            NUnit.Framework.Assert.AreEqual(3, CountLetters("ababab")['a']);
            NUnit.Framework.Assert.AreEqual(3, CountLetters("ababab")['b']);

            NUnit.Framework.Assert.AreEqual(12, GetPart1(testInput.Split(' ')));
        }

        public static void TestDay02Part2()
        {
            {
                var boxTest = @"abcde fghij klmno pqrst fguij axcye wvxyz";
                NUnit.Framework.Assert.AreEqual("fgij", GetPart2(boxTest.Split(' ')));
            }

            {
                var boxTest = @"aaaaaaaaaaaaaaaa
aaaaaaa1aeaaaaab
aaaaaaa2aeaaaaac
aaaaaaa3aeaaaaad
aaaaaaa4aeaaaaae
aaaaaaa4aeaaaaaf".Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                NUnit.Framework.Assert.AreEqual("aaaaaaa4aeaaaaa", GetPart2(boxTest));
            }
        }

        #endregion Tests

        public static int GetPart1(string[] input)
        {
            var twicers = 0;
            var thricers = 0;

            foreach (var riddleLine in input)
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

            return twicers * thricers;
        }

        public static string GetPart2(string[] input)
        {
            var lstInput = input.ToList();
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
                    dictCounter.Add(chr, 1);
                }
            }

            return dictCounter;
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