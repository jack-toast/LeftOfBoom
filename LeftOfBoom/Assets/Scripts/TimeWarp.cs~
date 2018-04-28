using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{

	public float start_timescale = 1;

	private bool time_slow_activated = false;

	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		if (Input.GetMouseButton(0)) {
			time_slow_activated = true;
		} else {
			time_slow_activated = false;
		}

		if (time_slow_activated) {
			Time.timeScale -= Time.timeScale / 10;
		} else {
			Time.timeScale *= 2.0f;
		}

		if (Time.timeScale < 0.0f) {
			Time.timeScale = 0.001f;
		}

		if (Time.timeScale > 1.0f) {
			Time.timeScale = 1.0f;
		}
	}
}
