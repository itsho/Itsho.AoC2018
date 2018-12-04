using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Itsho.AoC2018.Solutions
{
	public static class Day04Extentions
	{
		public static bool IsSleep(this DataRow dr, int minute)
		{
			var cell = dr[$@"{minute:D2}"];

			if (cell.GetType() != typeof(System.DBNull) &&
			    (char)cell == Day04Solution.SLEEP_TIME)
			{
				return true;
			}

			return false;
		}
	}

	public class GuardSleepCounter
	{
		public int GuardId { get; set; }
		public int MinuteId { get; set; }
		public int Counter { get; set; }
	}

	public static class Day04Solution
	{
		private const string COL_ROW_ID = "ROW_ID";
		private const string COL_DATE = "EVENT_DATE";
		private const string COL_GUARD_ID = "GUARD_ID";
		public const char SLEEP_TIME = '#';

		public static DataTable PrepareInput(IList<string> sortedSource)
		{
			var dt = new DataTable();

			dt.Columns.Add(COL_ROW_ID, typeof(int));
			dt.Columns.Add(COL_DATE, typeof(string));
			dt.Columns.Add(COL_GUARD_ID, typeof(int));

			for (int i = 0; i < 60; i++)
			{
				dt.Columns.Add($@"{i:D2}", typeof(char));
			}

			var lastGuard = -1;

			foreach (var line in sortedSource)
			{
				// parse line
				ParseLine(line, out string date, out string time, out string desc, out int? guardId);

				// each "sleep" is measured in minutes
				if (guardId != null)
				{
					lastGuard = guardId.Value;
				}
				else if (desc == "falls asleep")
				{
					var row = dt.Select($@"{COL_DATE}='{date}' AND {COL_GUARD_ID}={lastGuard}").FirstOrDefault();

					bool isNewRow = false;
					if (row == null)
					{
						row = dt.NewRow();
						row[COL_ROW_ID] = dt.Rows.Count;
						isNewRow = true;
					}
					row[COL_DATE] = date;
					row[COL_GUARD_ID] = lastGuard;
					row[time.ToString().Substring(3, 2)] = SLEEP_TIME;

					if (isNewRow)
					{
						dt.Rows.Add(row);
					}
				}
				else if (desc == "wakes up")
				{
					// find row
					var row = dt.Select($@"{COL_DATE}='{date}' AND {COL_GUARD_ID}={lastGuard}").FirstOrDefault();

					var timeStart = 0; //FindLastFallAsleep(row);
					{
						for (int i = 59; i >= 0; i--)
						{
							if (row.IsSleep(i))
							{
								timeStart = i;
								break;
							}
						}
					}

					var timeEnd = Convert.ToInt32(time.ToString().Substring(3, 2));

					for (int i = timeStart; i < timeEnd; i++)
					{
						row[$@"{i:D2}"] = SLEEP_TIME;
					}
				}
			}

			return dt;
		}

		public static int GetPart1(DataTable eventsTable)
		{
			// find guard with most minutes asleep
			var mostAsleepDict = GetMostAsleep(eventsTable);

			var guardMostAsleep = (from kvp in mostAsleepDict
				where kvp.Value == mostAsleepDict.Max(e => e.Value)
				select kvp.Key).FirstOrDefault();

			// most probable asleep in minute
			var firstMinuteAsleep = GetFirstMinuteAsleep(eventsTable, guardMostAsleep);

			return guardMostAsleep*firstMinuteAsleep;
		}

		public static int GetPart2(DataTable riddleSource)
		{
			// minute most asleep
			GetMinuteMostAsleep(riddleSource, out var mostSleptMinute, out var guardIdWithMostSleptMinute);

			return mostSleptMinute * guardIdWithMostSleptMinute;
		}

		private static int GetFirstMinuteAsleep(DataTable eventsTable, int guardMostAsleep)
		{
			var guardRows = eventsTable.Select($@"{COL_GUARD_ID}={guardMostAsleep}");

			// key = minute,
			// value = total sleep during all days
			var dictMinutes = new Dictionary<int, int>();
			for (int i = 0; i <= 59; i++)
			{
				foreach (DataRow guardRow in guardRows)
				{
					if (guardRow.IsSleep(i))
					{
						if (dictMinutes.ContainsKey(i))
						{
							dictMinutes[i]++;
						}
						else
						{
							dictMinutes.Add(i, 1);
						}
					}
				}
			}

			var maxSleep = dictMinutes.Max(kvp => kvp.Value);

			return dictMinutes.FirstOrDefault(kvp => kvp.Value == maxSleep).Key;
		}

		private static Dictionary<int,int> GetMostAsleep(DataTable eventsTable)
		{
			// key = guardId,
			// value = total minutes asleep
			var mostAsleepDict = new Dictionary<int,int>();

			foreach (DataRow singleEvent in eventsTable.Rows)
			{
				var guardId = (int)singleEvent[COL_GUARD_ID];
				var totalSleepMinutes = 0;
				for (int i = 0; i <= 59; i++)
				{
					if (singleEvent.IsSleep(i))
					{
						totalSleepMinutes++;
					}
				}

				if (mostAsleepDict.ContainsKey(guardId))
				{
					mostAsleepDict[guardId] += totalSleepMinutes;
				}
				else
				{
					mostAsleepDict.Add(guardId,totalSleepMinutes);
				}
			}

			return mostAsleepDict;
		}

		private static void ParseLine(string line, out string date, out string time, out string desc, out int? guardId)
		{
			var reg = new Regex(@"\[\d*?-(?'Date'\d*-\d*) (?'time'\d*:\d*)] (?'desc'.*#(?'GuardID'\d*).*|.*)");
			var match = reg.Matches(line)[0];
			date = match.Groups["Date"].Value;
			time = match.Groups["time"].Value;

			guardId = null;
			if (!string.IsNullOrEmpty(match.Groups["GuardID"].Value))
			{
				guardId = Convert.ToInt32(match.Groups["GuardID"].Value);
			}

			desc = match.Groups["desc"].Value;
		}

		private static void GetMinuteMostAsleep(DataTable eventRows, out int mostSleptMinute, out int guardIdWithMostSleptMinute)
		{
			// get list of guards
			var lstGuards = (from eventRow in eventRows.AsEnumerable()
							 select eventRow.Field<int>(COL_GUARD_ID)).Distinct();

			// key = guardid
			// value = minute which the guard is sleep (mostly)
			var dictGuardMinuteMostSleep = new List<GuardSleepCounter>();

			foreach (var guardId in lstGuards)
			{
				// get list of guard event
				var guardEvents = eventRows.Select($"{COL_GUARD_ID}='{guardId}'");

				// key = minute,
				// value = total sleep during all days
				var dictMinutes = new Dictionary<int, int>();
				for (int i = 0; i <= 59; i++)
				{
					foreach (DataRow eventRow in guardEvents)
					{
						if (!eventRow.IsSleep(i)) continue;
						if (dictMinutes.ContainsKey(i))
						{
							dictMinutes[i]++;
						}
						else
						{
							dictMinutes.Add(i, 1);
						}
					}
				}
				var maxSleepMinutes = dictMinutes.Max(kvp => kvp.Value);
				var minuteOfSleep = dictMinutes.FirstOrDefault(kvp => kvp.Value == maxSleepMinutes).Key;

				dictGuardMinuteMostSleep.Add(new GuardSleepCounter()
				{
					GuardId = guardId,
					MinuteId = minuteOfSleep,
					Counter = maxSleepMinutes
				});
			}

			var maxCounter = dictGuardMinuteMostSleep.Max(i => i.Counter);
			var foundGuard = dictGuardMinuteMostSleep.FirstOrDefault(i => i.Counter== maxCounter);

			mostSleptMinute = foundGuard.MinuteId;
			guardIdWithMostSleptMinute = foundGuard.GuardId;
		}
	}
}