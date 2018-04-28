using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
	private float health;
	private PlayerHealth player;

	private Text textComponent;

	// Use this for initialization
	void Start()
	{
		health = 0;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

		health = player.GetHealth();
		textComponent = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		health = player.GetHealth();
		textComponent.text = "Health:  " + health.ToString("0.0");
	}
}
