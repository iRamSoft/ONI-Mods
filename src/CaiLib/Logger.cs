﻿using System;

namespace CaiLib
{
	public static class Logger
	{
		public static void LogInit(string mod, int version)
		{
			Console.WriteLine($"{Timestamp()} <<-- CaiLib -->> Loaded << {mod} >> with version << {version} >>");
		}

		public static void Log(string mod, string message)
		{
			Console.WriteLine($"{Timestamp()} <<-- {mod} -->> " + message);
		}

		private static string Timestamp() => DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
	}
}
