using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
	public float chargeStrength = 1f;
	public float outerRadius = 16f;
	public float innerRadius = 4f;
	public GameObject boostParticles;

	private bool charging = false;

	private float mouseDistance = 0f;
	private Vector3 chargeDirection;

	private AimOuter aimOuter;
	private AimArrow aimArrow;

	private Rigidbody2D rb;


	// Use this for initialization
	void Start()
	{
		aimOuter = transform.parent.GetComponentInChildren<AimOuter>();
		aimArrow = transform.parent.GetComponentInChildren<AimArrow>();
		rb = transform.GetComponentInParent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
		mouseDistance = Vector3.Distance(mousePosition, transform.position);
		float angle = Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 180;
		transform.eulerAngles = new Vector3(0, 0, angle); // Rotate to face the mouse

		if (Input.GetMouseButton(0)) {

			if (mouseDistance < outerRadius) {
				aimOuter.SetColorInBounds();
				aimArrow.Activate();
				//chargeDirection = transform.position - mousePosition;
			} else {
				aimOuter.SetColorOutOfBounds();
				aimArrow.Deactivate();
			}

			if (mouseDistance > outerRadius + 0.1f) {
				aimOuter.SetColorOutOfBounds();
				aimArrow.Deactivate();
				charging = false;
				return;
			}
		} else {
			aimOuter.SetColorDeactivated();
			aimArrow.Deactivate();
		}
	}

	void FixedUpdate()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
		mouseDistance = Vector3.Distance(mousePosition, transform.position);

		if (Input.GetMouseButton(0)) {
			charging = true;
			chargeDirection = transform.position - mousePosition;
		}

		if (!Input.GetMouseButton(0) && charging) {
			mouseDistance = Mathf.Clamp(mouseDistance, innerRadius, outerRadius);

			Vector3 shotForce = chargeDirection.normalized * (200f + 12.5f * Mathf.Pow(mouseDistance - 4f, 2));
			shotForce = Vector3.ClampMagnitude(shotForce, 1800f);

			//Debug.Log("Force Strength: " + shotForce.magnitude);
			//Debug.Log ("Distance to Mouse: " + mouseDistance);

			rb.AddForce(shotForce);
			charging = false;
			GameObject particles = Instantiate(boostParticles, transform);
			particles.transform.parent = null;
			Destroy(particles, 2f);

			Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, 14f);



			foreach (Collider2D item in nearbyObjects) {
				if (item.CompareTag("Asteroid") || item.CompareTag("MagneticAsteroid")) {
					// Check to see if item is within the blast cone.
					Vector3 itemToCannon = item.transform.position - transform.position;

					float angleBetween = Vector3.Angle(itemToCannon, -1f * transform.right);

					if (item.transform.parent.name != "Player" && angleBetween < 45f) {
						Vector3 diff = item.transform.position - transform.position;
						item.attachedRigidbody.AddForce(diff.normalized * chargeStrength);
					}
				}
			}

			return;
		}


	}


}
