using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public float playerHealth = 100f;
	public float maxHealth = 100f;
	public float minHealth = 0.001f;

	private Rigidbody2D rbPlayer;

	private bool captureMomentum = false;
	private Vector2 playerMomentum = new Vector2(0f, 0f);

	// Use this for initialization
	void Start()
	{
		rbPlayer = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update()
	{
		
		if (captureMomentum) {
			captureMomentum = false;
			Vector2 momentum2 = rbPlayer.mass * rbPlayer.velocity;
			float deltaMomentumMagnitude = momentum2.magnitude - playerMomentum.magnitude;
			//Debug.Log("Change in momentum: " + deltaMomentumMagnitude);
			playerHealth -= Mathf.Abs(deltaMomentumMagnitude * 3);
		}

		playerMomentum = rbPlayer.mass * rbPlayer.velocity;

		if (playerHealth < minHealth) {
			//Debug.Log("rip");
			playerHealth = 0f;
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("Asteroid")) {
			captureMomentum = true;
		}
	}

	public void Reset()
	{
		playerHealth = maxHealth;
	}

	public float GetHealth()
	{
		return playerHealth;
	}

}
