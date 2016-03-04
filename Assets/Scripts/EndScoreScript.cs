using UnityEngine;
using System.Collections;

public class EndScoreScript : MonoBehaviour {
	
	int[] scores = new int[10];
	int yourScore;
	public string catchyPhrase;

	void Start () {
		for(int i = 1; i < 10; i++)
			scores[i-1] = PlayerPrefs.GetInt("Score"+i);
		yourScore = PlayerPrefs.GetInt ("YourScore");
	}
	
	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "High Scores!");
		for (int i = 1; i < 10; i++)
		{
			GUI.Label(new Rect(10, 10 +30*i, 100, 30), "Score " + i + ": " + scores[i-1]);
		}
		GUI.Label (new Rect (10, 340, 100, 300), "Your score: " + yourScore);

		GUI.Label (new Rect (Screen.width * 3 / 4, Screen.height / 2, 500, 40), catchyPhrase);
	}

	void Update()
	{
		if(Input.GetButtonDown("Jump"))
		   Application.LoadLevel(0);
	}
}
