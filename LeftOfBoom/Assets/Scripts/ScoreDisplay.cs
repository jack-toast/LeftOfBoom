using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	private int score;
	private SceneControl sc;

	private Text textComponent;

	// Use this for initialization
	void Start () {
		score = 0;
		sc = GameObject.FindGameObjectWithTag ("SceneController").GetComponent<SceneControl>();
		score = sc.GetPuzzleNumber ();
		textComponent = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		score = sc.GetPuzzleNumber ();
		textComponent.text = "Score: " + score.ToString ();

	}
}
