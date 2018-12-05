using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Itsho.AoC2018.Solutions
{
    public static class Day03Solution
    {
        #region Consts

        private const char EMPTY_CELL = '.';
        private const char USED_CELL = '#';
        private const char INVALID_CELL = 'X';

        #endregion Consts

        #region Tests

        public static void TestDay03Part1()
        {
            {
                var claim = @"#123 @ 3,2: 5x4";

                var matrixActual = InitMatrix(11);

                bool hasOverlap;
                ApplyClaim(ref matrixActual, claim, out hasOverlap);
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

                //var expectedVisual = VisualizeMatrix(ref matrixExpected);
                //var actualVisual = VisualizeMatrix(ref matrixActual);

                NUnit.Framework.Assert.AreEqual(matrixExpected, matrixActual);
            }

            {
                var claims =
                    @"#1 @ 1,3: 4x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

                var actual = GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
                NUnit.Framework.Assert.AreEqual(4, actual);
            }

            {
                var claims =
                    @"#1 @ 1,3: 3x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

                var actual = GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
                NUnit.Framework.Assert.AreEqual(2, actual);
            }

            {
                var claims =
                    @"#1 @ 1,3: 2x4
#2 @ 3,1: 4x4
#3 @ 5,5: 2x2";

                var actual = GetPart1(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
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

                var actual = GetPart2(claims.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries), 8);
                NUnit.Framework.Assert.AreEqual(3, actual);
            }
        }

        #endregion Tests

        public static int GetPart1(string[] input, int matrixSize)
        {
            var matrix = InitMatrix(matrixSize);
            foreach (var claim in input)
            {
                bool hasOverLap;
                ApplyClaim(ref matrix, claim, out hasOverLap);
            }

            //var temp = VisualizeMatrix(ref matrix);
            var countInvalid = (from c in matrix.Cast<char>()
                                where c == INVALID_CELL
                                select c).Count();

            return countInvalid;
        }

        public static int? GetPart2(string[] input, int matrixSize)
        {
            var matrix = InitMatrix(matrixSize);
            foreach (var claim in input)
            {
                bool hasOverLap;
                ApplyClaim(ref matrix, claim, out hasOverLap);
            }

            foreach (var claim in input)
            {
                bool hasOverLap;
                var id = ValidateClaim(ref matrix, claim, out hasOverLap);

                if (!hasOverLap)
                {
                    return id;
                }
            }

            return null;
        }

        private static int ValidateClaim(ref char[,] matrix, string claim, out bool isHasOverlap)
        {
            isHasOverlap = false;
            var regexMatch = new Regex(@"#(?'ID'\d*) @ (?'left'\d*),(?'top'\d*): (?'wide'\d*)x(?'tall'\d*)");
            var result = regexMatch.Matches(claim)[0];
            var id = Convert.ToInt32(result.Groups["ID"].Value);
            var locationX = Convert.ToInt32(result.Groups["left"].Value);
            var locationY = Convert.ToInt32(result.Groups["top"].Value);

            var width = Convert.ToInt32(result.Groups["wide"].Value);
            var height = Convert.ToInt32(result.Groups["tall"].Value);

            for (int widthIndex = 0; widthIndex < width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < height; heightIndex++)
                {
                    var locY = locationY + heightIndex;
                    var locX = locationX + widthIndex;

                    if (matrix[locY, locX] == INVALID_CELL)
                    {
                        isHasOverlap = true;
                    }
                }
            }

            return id;
        }

        public static string VisualizeClaim(string claim, int size)
        {
            var matrix = InitMatrix(size);
            bool hasOverLap;
            ApplyClaim(ref matrix, claim, out hasOverLap);

            return VisualizeMatrix(ref matrix);
        }

        public static string VisualizeMatrix(ref char[,] matrix)
        {
            var stringVisual = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    stringVisual.Append(matrix[i, j]);
                }

                stringVisual.Append(Environment.NewLine);
            }
            return stringVisual.ToString().TrimEnd(Environment.NewLine.ToCharArray());
        }

        public static int ApplyClaim(ref char[,] matrix, string claim, out bool isHasOverlap)
        {
            isHasOverlap = false;
            var regexMatch = new Regex(@"#(?'ID'\d*) @ (?'left'\d*),(?'top'\d*): (?'wide'\d*)x(?'tall'\d*)");
            var result = regexMatch.Matches(claim)[0];
            var id = Convert.ToInt32(result.Groups["ID"].Value);
            var locationX = Convert.ToInt32(result.Groups["left"].Value);
            var locationY = Convert.ToInt32(result.Groups["top"].Value);

            var width = Convert.ToInt32(result.Groups["wide"].Value);
            var height = Convert.ToInt32(result.Groups["tall"].Value);

            for (int widthIndex = 0; widthIndex < width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < height; heightIndex++)
                {
                    var locY = locationY + heightIndex;
                    var locX = locationX + widthIndex;

                    if (matrix[locY, locX] == EMPTY_CELL)
                    {
                        matrix[locY, locX] = USED_CELL;// id.ToString()[0];
                    }
                    else
                    {
                        matrix[locY, locX] = INVALID_CELL;
                        isHasOverlap = true;
                    }
                }
            }

            return id;
        }

        public static char[,] InitMatrix(int size)
        {
            var matrix = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = EMPTY_CELL;
                }
            }
            return matrix;
        }
    }
}