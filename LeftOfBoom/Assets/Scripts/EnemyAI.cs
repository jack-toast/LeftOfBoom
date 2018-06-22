using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public GameObject projectilePrefab;
	public float maxSpeed = 2f;
	public float attractionForce = 10f;
	public float followSpeed = 2f;
	public float shotDelay = 2f;
	public float engagementDistance = 40f;
	public float firingDistance = 20f;
	public float predictionOffset = 2f;
	public float shotSpeed = 5f;
	public float projectileKillDelay = 10f;

	private Rigidbody2D rb;
	private Transform player;
	private Rigidbody2D playerRB;
	private float distToPlayer;
	private Vector3 predictionDirection;


	// Use this for initialization
	void Start()
	{	
		if (GameObject.FindWithTag("Player") == null) {
			Destroy(this);
			return;
		}

		player = GameObject.FindGameObjectWithTag("Player").transform;

		distToPlayer = Mathf.Infinity;
		rb = GetComponent<Rigidbody2D>();
		playerRB = player.GetComponent<Rigidbody2D>();

		StartCoroutine(ShootProjectileCR());

	}

	void FixedUpdate()
	{
		distToPlayer = Vector3.Distance(transform.position, player.position);

		if (distToPlayer < engagementDistance) {
			Vector3 playerVelocity = new Vector3(playerRB.velocity.x, playerRB.velocity.y, 0);
			Vector3 predictedPosition = player.position + (playerVelocity * predictionOffset);

			// Vector from enemy transform to the 
			predictionDirection = (predictedPosition - transform.position).normalized;

			// Set velocity to intercept the player
			Vector2 newVelocity = (new Vector2(predictionDirection.x, predictionDirection.y)) * followSpeed;
			rb.velocity = newVelocity;
		}
	}

	IEnumerator ShootProjectileCR()
	{
		while (true) {
			if (distToPlayer < firingDistance) {
				// shoot the projectile
				GameObject projectile = Instantiate(projectilePrefab, transform.position + (predictionDirection * 3), new Quaternion(0, 0, 0, 0));
				projectile.GetComponent<Rigidbody2D>().velocity = predictionDirection * shotSpeed;
				Destroy(projectile, projectileKillDelay);
			}

			yield return new WaitForSeconds(shotDelay);
		}
	}

}
