using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{

	public GameObject pauseMenu;

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
		float bgHeight = healthGauge.transform.GetComponent<RectTransform>().sizeDelta.y;
		float barWidth = bgWidth * (playerHealth.GetHealth() / playerHealth.maxHealth);
		healthGauge.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(barWidth, bgHeight);

		// update the health text
		healthText.GetComponent<Text>().text = "Health: " + playerHealth.GetHealth().ToString("F1");

		// update the score
		score.GetComponent<Text>().text = "Score: " + sceneControl.GetPuzzleNumber();

		// Pause menu control
		if (Input.GetKeyDown(KeyCode.Escape)) {

			GameObject[] pauseMenus = GameObject.FindGameObjectsWithTag("PauseMenu");

			if (pauseMenus.Length == 0) {
				// If pause menu disabled, enable the pause menu
				Instantiate(pauseMenu, transform);
			} else {
				// If there are any pause menus, destroy them all!
				foreach (var item in pauseMenus) {
					Destroy(item);
				}
			}
		}
	}

}
