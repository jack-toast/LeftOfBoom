using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{

	public float timescaleReadout = 1;
	public float rampSpeed = 10f;

	private bool time_slow_activated = false;

	// Use this for initialization
	void Start()
	{
		Time.timeScale = 1;
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
			Time.timeScale = Mathf.SmoothStep (Time.timeScale, 1.0f, Time.fixedDeltaTime * rampSpeed);
		}

		if (Time.timeScale < 0.0f) {
			Time.timeScale = 0.001f;
		}

		if (Time.timeScale > 1.0f) {
			Time.timeScale = 1.0f;
		}

		timescaleReadout = Time.timeScale;

	}

	public void Pause(){
		Time.timeScale = 0.0f;

	}

	public void UnPause(){
		Time.timeScale = 1.0f;

	}

}
