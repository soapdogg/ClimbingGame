using UnityEngine;
using UnityEngine.UI;
using Kinect;
using UnityEngine.AI;

public class CoinGameManager : MonoBehaviour, IManager
{
	public static CoinGameManager singleton { get; private set; }

	public GameObject skeleton;
	private GameState currentState;

	public enum GameState
	{
		NewGame,
		GameRunning,
		GameStopped,
		Countdown

	}

	private CoinGameManager ()
	{
	}

	void Start ()
	{
		singleton = this;
		Initialize ();
		TranslateCollisionBoxes ();
		Debug.Log ("Coin Game Scene Entered");

	}

	void TranslateCollisionBoxes ()
	{
		Renderer r = this.gameObject.AddComponent<MeshRenderer> ();
		for (int i = 0; i < this.transform.childCount; i++) {
			
			Transform child = this.transform.GetChild (i);
			if (child.name != "Start") {
				continue;
			}
			Debug.Log ("fuck shit");
			var rect = child.GetComponent<RectTransform> ();
			Vector2 anchorPos = rect.anchoredPosition;

			float camHeight = 2f * Camera.main.orthographicSize;
			float camWidth = camHeight * Camera.main.aspect;

			Debug.Log (r.bounds.size);

			float translatedX = (anchorPos.x / r.bounds.size.x) * camWidth;
			float translatedY = (anchorPos.y / r.bounds.size.y) * camHeight;
			float translatedWidth = (rect.rect.width / r.bounds.size.x) * camWidth;
			float translatedHeight = (rect.rect.height / r.bounds.size.y) * camHeight;
			Debug.Log (translatedX);
			Debug.Log (translatedY);
			Debug.Log (translatedWidth);
			Debug.Log (translatedHeight);

			var collider = child.GetComponent<BoxCollider> ();
			collider.bounds.SetMinMax (
				new Vector3 (translatedX, translatedY, -10000),
				new Vector3 (translatedX + translatedWidth, translatedY + translatedHeight, 100000));

			collider.bounds.SetMinMax (
				new Vector3 (0, 0, 0),
				new Vector3 (5, 5, 5));
			//collider.bounds.center = new Vector3 (translatedX + translatedWidth / 2, translatedY + translatedHeight / 2, 0);
			//collider.bounds.size = new Vector3 (translatedWidth, translatedHeight, 10000);

		}
	}

	void Update ()
	{
		if (currentState == GameState.GameRunning) {
			OverlayManager.singleton.UpdateTimer ();
		} else if (currentState == GameState.Countdown) {
			OverlayManager.singleton.UpdateCountdown ();
		}
	}

	public void SetStateToStopped ()
	{
		currentState = GameState.GameStopped;
	}

	public void SetStateToRunning ()
	{
		currentState = GameState.GameRunning;
	}

	public void SetStateToNew ()
	{
		currentState = GameState.NewGame;
	}

	public void SetStateToCountdown ()
	{
		currentState = GameState.Countdown;
	}


	public GameState GetGameState ()
	{
		return currentState;
	}

	public void Initialize ()
	{
		SetStateToNew ();
	}
}
