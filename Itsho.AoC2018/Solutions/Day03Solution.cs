using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Itsho.AoC2018.Solutions
{
	public static class Day03Solution
	{
		private const char EMPTY_CELL = '.';
		private const char USED_CELL = '#';
		private const char INVALID_CELL = 'X';

		public static int GetPart1(string[] riddleSource, int matrixSize)
		{
			var matrix = InitMatrix(matrixSize);
			foreach (var claim in riddleSource)
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

		public static int? GetPart2(string[] riddleSource, int matrixSize)
		{
			var matrix = InitMatrix(matrixSize);
			foreach (var claim in riddleSource)
			{
				bool hasOverLap;
				ApplyClaim(ref matrix, claim, out hasOverLap);
			}


			foreach (var claim in riddleSource)
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

			for (int widthIndex = 0; widthIndex <  width; widthIndex++)
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