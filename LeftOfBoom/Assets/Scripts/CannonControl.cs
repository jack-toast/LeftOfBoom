using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
	public float outerRadius = 28f;
	public float innerRadius = 4f;
	public float minForce = 5000f;
	public float maxForce = 50000f;
	public GameObject boostParticles1;
	public GameObject boostParticles2;

	public bool debugShotforce;
	public float debugForce;

	public AudioClip cannonSound;

	private AudioSource source;

	private bool charging = false;

	private float mouseDistance = 0f;
	private Vector3 chargeDirection;

	private AimOuter aimOuter;
	private AimArrow aimArrow;

	private Rigidbody2D rb;

	private bool shootyToots;


	// Use this for initialization
	void Start()
	{
		aimOuter = transform.parent.GetComponentInChildren<AimOuter>();
		aimArrow = transform.parent.GetComponentInChildren<AimArrow>();
		rb = transform.GetComponentInParent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
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


		if (shootyToots) {
			SpawnParticles(boostParticles1, 0.1f);
			SpawnParticles(boostParticles2, -0.1f);
			shootyToots = false;
		}

	}

	void SpawnParticles(GameObject particlePrefab, float yOffset)
	{
		GameObject particles = Instantiate(particlePrefab, transform.position + -3f * transform.right + (transform.up * yOffset), transform.rotation).gameObject;
		ParticleSystem.MainModule mainModule = particles.GetComponent<ParticleSystem>().main;
		mainModule.startSpeedMultiplier = mouseDistance;
		particles.transform.parent = null;
		Destroy(particles, 5f);
	}

	void FixedUpdate()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
		mouseDistance = Vector3.Distance(mousePosition, transform.position);
		float angle = Mathf.Atan2((mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 180;
		transform.eulerAngles = new Vector3(0, 0, angle); // Rotate to face the mouse


		if (Input.GetMouseButton(0)) {
			charging = true;
			chargeDirection = transform.position - mousePosition;
		}

		if (!Input.GetMouseButton(0) && charging) {
			mouseDistance = Mathf.Clamp(mouseDistance, innerRadius, outerRadius);


			Vector3 shotForce = chargeDirection.normalized * (minForce + 312 * Mathf.Pow(mouseDistance - innerRadius, 2));
			shotForce = Vector3.ClampMagnitude(shotForce, maxForce);

			rb.AddForce(shotForce);
			charging = false;

			shootyToots = true;

			Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, outerRadius);

			foreach (Collider2D item in nearbyObjects) {
				if (item.CompareTag("Asteroid") || item.CompareTag("MagneticAsteroid")) {
					// Check to see if item is within the blast cone.
					Vector3 itemToCannon = item.transform.position - transform.position;

					float angleBetween = Vector3.Angle(itemToCannon, -1f * transform.right);

					if (item.transform.parent.name != "Player" && angleBetween < 45f) {
						Vector3 diff = item.transform.position - transform.position;
						item.attachedRigidbody.AddForce(shotForce.magnitude * (diff.normalized / outerRadius) / 100f);
					}
				}
			}

			float cameraShakeAmount = (shotForce.magnitude / maxForce) / 3f;
			float cameraShakeDuration = (shotForce.magnitude / maxForce) / 3f;
			Camera.main.GetComponent<CameraShake>().ShakeCamera(cameraShakeAmount, cameraShakeDuration, -chargeDirection.normalized);

			// Play the sound
			source.PlayOneShot(cannonSound, 0.3f);

			return;
		}


	}


}
