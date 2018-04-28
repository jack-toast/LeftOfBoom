using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

	public Vector3 resetPosition;

	private GameObject sceneController;
	private Rigidbody2D rb;
	private PlayerHealth playerHealth;

	// Use this for initialization
	void Start()
	{
		resetPosition = transform.position;
		sceneController = GameObject.FindGameObjectWithTag("SceneController");
		rb = GetComponent<Rigidbody2D>();
		playerHealth = GetComponent<PlayerHealth>();
	}

	// Update is called once per frame
	void Update()
	{
		// A rotates counter-clockwise
		if (Input.GetKey(KeyCode.A)) {
			rb.AddTorque(0.5f);
		}
		// D rotates clockwise
		if (Input.GetKey(KeyCode.D)) {
			rb.AddTorque(-0.5f);
		}
	}

	public void Reset()
	{
		transform.position = resetPosition;
		rb.velocity = new Vector3(0, 0, 0);
		rb.angularVelocity = 0f;
		rb.mass = 1f;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("[CheckpointTrigger] OnTriggerEnter");
		if (col.gameObject.CompareTag("Checkpoint")) {
			sceneController.GetComponent<SceneControl>().IncrementPuzzleNumber();
			resetPosition = col.transform.position;
			Destroy(col.gameObject);

			DestroyAttachedAsteroids();
			rb.mass = 1f;

			playerHealth.Reset();


		}
	}

	void DestroyAttachedAsteroids()
	{
		GameObject[] magAsteroids = GameObject.FindGameObjectsWithTag("MagneticAsteroid");
		int numAsteroids = magAsteroids.Length;
		Debug.Log(numAsteroids.ToString());
		for (int i = 0; i < numAsteroids; i++) {
			if (magAsteroids[i].transform.parent == transform) {
				Destroy(magAsteroids[i]);
			}
		}
	}

}
