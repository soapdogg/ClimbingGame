using UnityEngine;
using System.IO;

public class CoinManager : MonoBehaviour, IManager
{
	private const int NUM_COINS = 5;
	private int numCoinsCollected;

	public GameObject c1, c2, c3, c4, c5;
	private GameObject[] coins;

	public static CoinManager singleton { get; private set;}

	void Start()
	{
		singleton = this;
		Initialize ();
	}

	public void IncrementNumPressed()
	{
		if (++numCoinsCollected >= NUM_COINS) 
		{
			CoinGameManager.singleton.SetStateToStopped ();
            EndGameMenuManager.singleton.EnableEndGameVisuals(true);
			OverlayManager.singleton.pauseText.enabled = false;
		}
	}

	public void GenerateCoins()
	{
		string path = GetPresetCoinPath ();
		StreamReader sr = new StreamReader (path);
		int counter = 0;
		while (!sr.EndOfStream) {
			string line = sr.ReadLine ();
			string[] lineSplit = line.Split (',');
			float x = float.Parse (lineSplit [0]);
			float y = float.Parse (lineSplit [1]);
			coins [counter].SetActive (true);
			coins [counter++].transform.position = new Vector3 (x, y, 0);
		}
		sr.Close ();
	}

	private string GetPresetCoinPath ()
	{
		DifficultyMenuManager dms = DifficultyMenuManager.singleton;

		string diff = dms.currentDifficulty.ToString ();
		string presetNum = UnityEngine.Random.Range (1, 5).ToString ();
		string path = Path.Combine ("Assets", "Coin Game");
		path = Path.Combine (path, "Presets");
		return Path.Combine (path, diff + presetNum + ".txt");
	}

	public void Initialize()
	{
		numCoinsCollected = 0;
		coins = new []{c1, c2, c3, c4, c5};
		foreach (GameObject go in coins)
			go.SetActive (false);
	}
}