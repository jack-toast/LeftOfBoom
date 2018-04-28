using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDelete : MonoBehaviour {

	public float deleteTime = 5f;
	public float timeElapsed;

	// Use this for initialization
	void Start () {
		timeElapsed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;

		if (timeElapsed > deleteTime) {
			Destroy (transform.gameObject);
		}
	}
}
