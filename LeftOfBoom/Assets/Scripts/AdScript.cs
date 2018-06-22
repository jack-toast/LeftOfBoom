using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdScript : MonoBehaviour
{

	public bool adsEnabled;

	// Use this for initialization
	void Start()
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(1)) {
			adsEnabled = !adsEnabled;
			if (adsEnabled) {
				GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
			}
		}

		if (!adsEnabled) {
			GetComponent<RectTransform>().position = new Vector3(-500, -500, 0);
		}
	}

	public void ChangePosition()
	{
		//GetComponent<RectTransform>()
		RectTransform rt = GetComponent<RectTransform>();

		float newPosX = Random.Range(0, transform.parent.GetComponent<RectTransform>().sizeDelta.x - rt.sizeDelta.x / 2);
		float newPosY = Random.Range(0, transform.parent.GetComponent<RectTransform>().sizeDelta.y - rt.sizeDelta.y / 2);

		rt.position = new Vector3(newPosX, newPosY, 0);

		Debug.Log("ChangePosition");
	}

}
