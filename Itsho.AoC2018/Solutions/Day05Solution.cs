using System;
using System.Collections.Generic;
using System.Linq;

namespace Itsho.AoC2018.Solutions
{
    public static class Day05Solution
    {
        private const int POSITIVE_GAP = 'a' - 'A';

        #region Tests

        public static void TestDay05Part1()
        {
            NUnit.Framework.Assert.AreEqual(0, GetPart1("aA"));
            NUnit.Framework.Assert.AreEqual(0, GetPart1("abBA"));
            NUnit.Framework.Assert.AreEqual(4, GetPart1("abAB"));
            NUnit.Framework.Assert.AreEqual(6, GetPart1("aabAAB"));
            NUnit.Framework.Assert.AreEqual(10, GetPart1("dabAcCaCBAcCcaDA")); // "dabCBAcaDA"

            // my tests
            NUnit.Framework.Assert.AreEqual(0, GetPart1("aAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaA"));
            NUnit.Framework.Assert.AreEqual(0, GetPart1("AaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAa"));
            NUnit.Framework.Assert.AreEqual(0, GetPart1("bAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaAaB"));
            NUnit.Framework.Assert.AreEqual(4, GetPart1("aaayYyYAEE"));
        }

        public static void TestDay05Part2()
        {
            NUnit.Framework.Assert.AreEqual(4, GetPart2("dabAcCaCBAcCcaDA"));
        }

        #endregion Tests

        public static int GetPart1(string input)
        {
            var polymers = new List<char>();
            polymers.Clear();
            polymers.AddRange(input.ToList());

            var indexToStartSearch = 0;
            var reactionIndex = FindNextReaction(ref polymers, indexToStartSearch);

            while (reactionIndex != -1)
            {
                indexToStartSearch = RemoveReaction(ref polymers, reactionIndex);
                reactionIndex = FindNextReaction(ref polymers, indexToStartSearch);
            }

            return polymers.Count;
        }

        public static int GetPart2(string input)
        {
            var smallestResult = input.Length;

            for (char charToFind = 'a'; charToFind <= 'z'; charToFind++)
            {
                var polymers = new List<char>();
                polymers.AddRange(input.ToList());

                polymers.RemoveAll(c => c == charToFind);
                polymers.RemoveAll(c => c == charToFind - POSITIVE_GAP);

                var activeReactsResult = GetPart1(string.Join("", polymers));

                if (activeReactsResult < smallestResult)
                {
                    smallestResult = activeReactsResult;
                    Console.WriteLine($"Found smaller unit ({charToFind})->{smallestResult}");
                }
            }

            return smallestResult;
        }

        private static int RemoveReaction(ref List<char> polymers, int reactionIndex)
        {
            polymers.RemoveAt(reactionIndex + 1);
            polymers.RemoveAt(reactionIndex);

            // if current index is the start, we cannot go back :-)
            if (reactionIndex == 0)
            {
                return 0;
            }
            // go back one char to check that the new combination does not cause any reaction
            return reactionIndex - 1;
        }

        private static int FindNextReaction(ref List<char> polymers, int indexToStartSearch)
        {
            if (polymers.Count == 0 || indexToStartSearch == -1)
            {
                return -1;
            }

            for (int i = indexToStartSearch; i < polymers.Count - 1; i++)
            {
                if (System.Math.Abs(polymers[i] - polymers[i + 1]) == POSITIVE_GAP)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}