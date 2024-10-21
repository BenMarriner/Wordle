using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Runtime.CompilerServices;

static class Dictionary
{
	public static List<String>? WordDictionary;

	public static void InitDictionary()
	{
		String filepath = Directory.GetCurrentDirectory() + "/Dictionary.csv";
		WordDictionary = new List<String>();

		try
		{
			using (TextFieldParser reader = new TextFieldParser(filepath))
			{
				while (!reader.EndOfData)
				{
					string? fieldData = reader.ReadLine();
					if (fieldData != null)
					{
						// Only allow 5 letter words
						if (fieldData.Length == 5)
						{
							WordDictionary.Add(fieldData);
						}
					}
				}
			}
		}
		catch (FileNotFoundException)
		{
			Console.WriteLine("FATAL ERROR: Could not find the Dictionary.csv file");
		}
	}
}
