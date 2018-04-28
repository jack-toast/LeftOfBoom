using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollision : MonoBehaviour
{
	public float asteroidMass = 0.2f;

	private Rigidbody2D rb;



	void Start()
	{
		rb = transform.GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "MagneticAsteroid") {
			Physics2D.IgnoreCollision(col.collider.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			Destroy(col.collider.GetComponent<Rigidbody2D>());
			col.transform.parent = transform;

			float newMass = rb.mass + asteroidMass;

			rb.mass = newMass;

		}
	}
}
