using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimArrow : MonoBehaviour
{

	private bool isActive;

	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start()
	{
		isActive = false;
		lineRenderer = GetComponent<LineRenderer>();

	}
	
	// Update is called once per frame
	void Update()
	{
		if (isActive) {
			//draw some stuff

			lineRenderer.SetPosition(0, transform.position);
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
			Vector3 diff = transform.position - mousePosition;
			Vector3 endPos = transform.position + diff;

			lineRenderer.SetPosition(1, endPos);
		}
	}

	public void Activate()
	{
		isActive = true;
	}

	public void Deactivate()
	{
		isActive = false;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, transform.position);
	}
}
