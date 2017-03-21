using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Diagnostics;
using System.Xml.Linq;
using UnityEngine.SocialPlatforms.Impl;

/*
 * This class handles:
 *      creating the dictionary to access the high scores
 *      checking to see if the user's score is a high score
 *      updating the high score file with the new score
 *      returning the high scores from the dictionary for the given difficulty
 */
public class HighScoreScript
{
	public const int MAX_NUM_OF_HIGH_SCORES = 5;

	public static void writeHighScore (string name, float score)
	{
		List<HighScore> list = new List<HighScore> ();
		list = getOrderedScores ();
		list.Add (new HighScore (name, score));
		updateHighScoreListFile (list);
	}

	public static bool isHighScore (float score)
	{
		List<HighScore> list = new List<HighScore> ();
		list = getOrderedScores ();

		if (list.Count < MAX_NUM_OF_HIGH_SCORES || (list [MAX_NUM_OF_HIGH_SCORES - 1] as HighScore).score > score) {
			return true;
		}
		return false;
	}

	public static void updateHighScoreListFile (List<HighScore> list)
	{
		list.Sort ();
		list.Reverse ();
		if (list.Count > MAX_NUM_OF_HIGH_SCORES) {
			list.RemoveAt (list.Count - 1);
		}

		using (StreamWriter sw = new StreamWriter (getHighScoreFilePath ())) {
			foreach (HighScore hs in list)
				sw.WriteLine (hs.ToString ());
		}
	}

	public static string getHighScoreFilePath ()
	{
		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "HighScores");
		path = Path.Combine (path, "hard_highScores.txt");
		if (!File.Exists (path)) {
			File.Create (path);
		}
		return path;
	}

	public static List<HighScore> getOrderedScores ()
	{
		List<HighScore> list = new List<HighScore> ();
		string[] lines = File.ReadAllLines (getHighScoreFilePath ());
		foreach (var line in lines) {
			list.Add (new HighScore (line));
		}
		list.Sort ();
		list.Reverse ();
		return list;
	}
}
