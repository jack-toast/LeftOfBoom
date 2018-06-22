using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

	public float playerHealth = 100f;
	public float maxHealth = 100f;
	public AudioClip damageSound;

	private Rigidbody2D rbPlayer;

	private bool captureMomentum = false;
	private Vector2 playerMomentum = new Vector2(0f, 0f);
	private AudioSource source;


	// Use this for initialization
	void Start()
	{
		rbPlayer = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		if (captureMomentum) {
			captureMomentum = false;
			Vector2 momentum2 = rbPlayer.mass * rbPlayer.velocity;
			float deltaMomentumMagnitude = momentum2.magnitude - playerMomentum.magnitude;
			playerHealth -= Mathf.Abs(deltaMomentumMagnitude / 20f);
			float soundLevel = Mathf.Abs(deltaMomentumMagnitude / 50f);
			soundLevel = Mathf.Clamp(deltaMomentumMagnitude, 0.3f, 1f);
			if (!source.isPlaying) {
				source.PlayOneShot(damageSound, soundLevel);
			}

		}

		playerMomentum = rbPlayer.mass * rbPlayer.velocity;

		if (playerHealth < 0f) {
			transform.GetComponent<PlayerControl>().Reset();
			Reset();
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("Asteroid")) {
			
			captureMomentum = true;
		} else if (col.transform.CompareTag("Projectile")) {
			if (Vector3.Distance(col.transform.position, transform.position) < (transform.localScale.x + col.transform.localScale.x)) {
				captureMomentum = true;
			}
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

	public void SetHealth(float h)
	{
		playerHealth = h;
	}

}
