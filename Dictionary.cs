using Microsoft.VisualBasic.FileIO;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

/// <summary>
/// This class handles the loading of the Dictionary.csv file into memory.
/// </summary>
public static class Dictionary
{
	private static List<String>? _wordDictionary;
	public static List<String> WordDictionary
	{ 
		get 
		{
            _wordDictionary = _wordDictionary ?? []; // Initialise the dictionary list if it has not yet been initialised
			return _wordDictionary;
		}
	}

	/// <summary>
	/// This function must be called on startup of the program so that the word dictionary can be used
	/// </summary>
	public static void InitDictionary()
	{
		try
		{
			String filepath = Directory.GetCurrentDirectory() + "/dictionary.txt";
			if (!File.Exists(filepath)) throw new FileNotFoundException();

			using (TextFieldParser reader = new TextFieldParser(filepath))
			{
				while (!reader.EndOfData)
				{
					string? currLine = reader.ReadLine();
					if (currLine != null)
					{
						// Only allow 5 letter words
						if (currLine.Length == 5)
						{
							WordDictionary.Add(currLine.ToUpper());
						}
					}
				}
			}
		}
		catch (FileNotFoundException e)
		{
			// Program will be forced to exit since it cannot function without the dictionary
			Console.WriteLine("FATAL ERROR: Could not find the dictionary.txt file");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
			Environment.Exit(e.HResult);
		}
	}
	

	/// <summary>
	/// Useful for helping to ensure the player inputs valid words
	/// </summary>
	/// <param name="word"></param>
	/// <returns></returns>
	public static bool WordExists(String word)
	{
		if (WordDictionary != null) 
			return WordDictionary.Contains(word.ToUpper());
		else 
			return false;
	}
}
