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

			col.transform.parent = transform;

			float newMass = rb.mass + (col.collider.GetComponent<Rigidbody2D>().mass) / 3f;
			rb.mass = newMass;
			Destroy(col.collider.GetComponent<Rigidbody2D>());
			Destroy(col.transform.GetComponent<MagAttraction>());
		}
	}
}
