using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimOuter : MonoBehaviour
{

	public Color colorInBounds = Color.green;
	public Color colorOutOfBound = Color.red;
	public Color colorDeactivated = Color.grey;
	public float line_width = 1.0f;

	public int vertexCount = 36;

	private LineRenderer lineRenderer;

	private float radius = 2f;

	// Use this for initialization
	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		radius = transform.parent.GetComponentInChildren<CannonControl>().outerRadius;
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startColor = colorDeactivated;
		lineRenderer.endColor = colorDeactivated;
	}
	
	// Update is called once per frame
	void Update()
	{
		radius = transform.parent.GetComponentInChildren<CannonControl>().outerRadius;
		lineRenderer.positionCount = vertexCount + 1;
		lineRenderer.widthMultiplier = line_width;

		float x;
		float y;
		float z = 0.0f;

		float angle = 0.0f;

		for (int i = 0; i < vertexCount + 1; i++) {
			x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
			y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
			lineRenderer.SetPosition(i, new Vector3(x, y, z) + transform.position);
			angle += (360f / (vertexCount - 0.1f));
		}
	}

	public void SetColorInBounds()
	{
		lineRenderer.startColor = colorInBounds;
		lineRenderer.endColor = colorInBounds;
	}

	public void SetColorOutOfBounds()
	{
		lineRenderer.startColor = colorOutOfBound;
		lineRenderer.endColor = colorOutOfBound;
	}

	public void SetColorDeactivated()
	{
		lineRenderer.startColor = colorDeactivated;
		lineRenderer.endColor = colorDeactivated;
	}
}
