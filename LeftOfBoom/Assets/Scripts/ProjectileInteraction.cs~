using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInteraction : MonoBehaviour
{

	public GameObject collisionParticleEffect;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.transform.CompareTag("Player")) {
			// Deal damage or do something to the player
		}

		if (col.transform.CompareTag("EnemyShip")) {
			return;
		}

		// Spawn a particle system
		GameObject hitEffect = Instantiate(collisionParticleEffect, transform.position, transform.rotation).gameObject;

		Destroy(hitEffect, 4);
		Destroy(gameObject, 0.01f);
	}

}
