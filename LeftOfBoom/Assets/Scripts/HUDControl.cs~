using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{

	private PlayerHealth playerHealth;
	private SceneControl sceneControl;

	private GameObject healthGauge;
	private GameObject healthText;
	private GameObject score;

	// Use this for initialization
	void Start()
	{
		playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		sceneControl = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneControl>();

		healthGauge = transform.Find("HealthGauge").gameObject;
		healthText = transform.Find("HealthText").gameObject;
		score = transform.Find("Score").gameObject;
	}
	
	// Update is called once per frame
	void Update()
	{
		// update the health bar
		float bgWidth = healthGauge.transform.GetComponent<RectTransform>().sizeDelta.x;
		float barWidth = bgWidth * (playerHealth.GetHealth() / playerHealth.maxHealth);
		healthGauge.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(barWidth, 20);

		// update the health text
		healthText.GetComponent<Text>().text = "Health: " + playerHealth.GetHealth().ToString("F1");

		// update the score
		score.GetComponent<Text>().text = "Score: " + sceneControl.GetPuzzleNumber();

	}
}
