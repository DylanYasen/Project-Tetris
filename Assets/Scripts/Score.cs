using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {


	public static Text scoreText;
	public static int score;

	// Use this for initialization
	void Start () {
		score = 0;
		//scoreText.text = "Score: " + score.ToString();
	}


	


	public static void updateScore(int rowsCleared)
	{
		switch (rowsCleared) {
		case 1:
			score += 40;
			break;
		case 2:
			score += 100;
			break;
		case 3:
			score += 300;
			break;
		case 4:
			score += 1200;
			break;
		}
		//scoreText.text = "Score: " + score.ToString();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
