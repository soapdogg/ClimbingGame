using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;

public class HighScoreScript
{
	public static void checkHighScore (string name, float score)
	{
		HighScore userHighScore = writeHighScore (name, score);
		List<HighScore> list = getOrderedScores ();

		Debug.Log (String.Format ("The best score is: {0}", list [0]));
		Debug.Log (String.Format ("{0}'s score places them in spot {1} out of {2}", 
			name, list.IndexOf (userHighScore), list.Count));
	}

	public static HighScore writeHighScore (string name, float score)
	{
		HighScore userScore;
		using (StreamWriter sw = File.AppendText (getHighScoreFilePath ())) {
			userScore = new HighScore (name, score);
			sw.WriteLine (userScore.ToString ());
		}
		return userScore;
	}

	public static string getHighScoreFilePath ()
	{
		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "highscores.txt");
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
