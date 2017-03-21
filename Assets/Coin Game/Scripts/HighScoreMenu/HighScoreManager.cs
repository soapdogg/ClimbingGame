using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System;
using System.CodeDom.Compiler;

/*
 * This class handles:
 * 		checking if the user's score is a high score
 * 		updating the high score file if it is a high score
 * 		and getting the list of high scores
 * 
 */
public class HighScoreManager : MonoBehaviour, IManager
{
	
	private const int MAX_NUM_OF_HIGH_SCORES = 5;

	public static HighScoreManager singleton { get; private set; }

	public Dictionary<DifficultyMenuManager.Difficulty, string> highScoreFileDict;

	void Start ()
	{
		singleton = this;
		Initialize ();
	}

	public void Initialize ()
	{
		highScoreFileDict = new Dictionary<DifficultyMenuManager.Difficulty, string> ();
		BuildHighScoreFileDict ();
	}

	public string GetHighScoreFilePath (DifficultyMenuManager.Difficulty difficulty)
	{
		return highScoreFileDict [difficulty];
	}

	/*
	 * Adds the name and score to the list of high scores for the current difficulty
	 * UpdateHighScoreListFile() is then called with this modified list
	 */
	public void WriteHighScore (string name, float score)
	{
		List<HighScore> list = new List<HighScore> ();
		list = GetOrderedScores ();
		list.Add (new HighScore (name, score));
		UpdateHighScoreListFile (list);
	}

	/*
	 * Checks whether the given parameter, score, is a high score for the current difficulty
	 */
	public bool IsHighScore (float score)
	{
		List<HighScore> list = new List<HighScore> ();
		list = GetOrderedScores ();

		if (list.Count < MAX_NUM_OF_HIGH_SCORES || (list [MAX_NUM_OF_HIGH_SCORES - 1] as HighScore).score > score) {
            Debug.Log("High score!");
            return true;
		}
        Debug.Log("No high score :( ");
        return false;
	}

	/*
	 * Updates the high score file for the current difficulty
	 * 
	 * Takes in a list of HighScore objects
	 * This list is sorted and reversed (so the fastest time is first)
	 * 
	 * The file is completely overwritten if there was something there previously
	 * Every HighScore object is written line by line to the file
	 */
	private void UpdateHighScoreListFile (List<HighScore> list)
	{
		list.Sort ();
		list.Reverse ();

		//removes the last item in the list (to limit the number of high scores)
		if (list.Count > MAX_NUM_OF_HIGH_SCORES) {
			list.RemoveAt (list.Count - 1);
		}

		using (StreamWriter sw = new StreamWriter (GetHighScoreFilePath (DifficultyMenuManager.singleton.GetDifficulty ()))) {
			foreach (HighScore hs in list)
				sw.WriteLine (hs.ToString ());
		}
	}

	/*
	 * Gets the ordered high scores from the file
	 * 
	 * Reads each line from the file and creates a new HighScore object
	 * The HighScore objects are put into a list
	 * The list is returned after being sorted by time and reversed (so the fastest time is first)
	 * 
	 */
	public List<HighScore> GetOrderedScores ()
	{
		List<HighScore> list = new List<HighScore> ();
		string[] lines = File.ReadAllLines (GetHighScoreFilePath (DifficultyMenuManager.singleton.GetDifficulty ()));
		foreach (var line in lines) {
			list.Add (new HighScore (line));
		}

		list.Sort ();
		list.Reverse ();
		return list;
	}

	/**
	 * Adds the high score text files to the dictionary.
	 * 
	 * Dictionary key -> DifficultyMenuManager.Difficulty
	 * Dictionary value -> Path to text file with the difficulty high scores (string)
	 */ 
	private void BuildHighScoreFileDict ()
	{
		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "HighScores");

		string easyPath = Path.Combine (path, "easy_highScores.txt");
		highScoreFileDict.Add (DifficultyMenuManager.Difficulty.Easy, easyPath);
		if (!File.Exists (easyPath)) {
			File.Create (easyPath);
		}
		 
		string mediumPath = Path.Combine (path, "medium_highScores.txt");
		highScoreFileDict.Add (DifficultyMenuManager.Difficulty.Medium, mediumPath);
		if (!File.Exists (mediumPath)) {
			File.Create (mediumPath);
		}
			
		string hardPath = Path.Combine (path, "hard_highScores.txt");
		highScoreFileDict.Add (DifficultyMenuManager.Difficulty.Hard, hardPath);
		if (!File.Exists (hardPath)) {
			File.Create (hardPath);
		}
	}
}