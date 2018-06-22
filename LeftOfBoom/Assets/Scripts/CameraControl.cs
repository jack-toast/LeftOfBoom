using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public float parallaxSpeed;

	public float zoomSpeed = 10f;

	public float xOffset = 2f;
	public float cameraChaseFactor = 4f;

	private GameObject player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		// Move camera to player position
		Vector3 newPosition = player.transform.position;
		newPosition.z = -10f;
		newPosition.x += xOffset;

		Vector3 beforePosition = transform.position;
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * cameraChaseFactor);

		// Creating the scrolling background effect
		Vector3 difference = transform.position - beforePosition;
		Vector2 diff2 = new Vector2(difference.x, difference.y);

		diff2 *= parallaxSpeed;

		MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

		int i = 2;
		foreach (var item in meshRenderers) {
			Vector2 newOffset = item.material.mainTextureOffset + (diff2 / (i + 1));
			item.material.mainTextureOffset = newOffset;
			i++;
		}

		// Zoom in and out
		float orthographicSize = GetComponent<Camera>().orthographicSize + (Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed);
		orthographicSize = Mathf.Clamp(orthographicSize, 5, 30);
		GetComponent<Camera>().orthographicSize = orthographicSize;
	}

}
