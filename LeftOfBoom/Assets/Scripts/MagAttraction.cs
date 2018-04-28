﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagAttraction : MonoBehaviour
{

	public float activationDistance = 5f;
	public float maxSpeed = 1f;
	public float attractionForce = 5f;
	private Rigidbody2D rb;
	private Transform player;
	private float distToPlayer;

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
	}
	
	// Update is called once per frame
	void Update()
	{
		if (transform.GetComponent<Rigidbody2D>() == null) {
			//Debug.Log("RB is null (attached to player)");
			Destroy(this);
			return;
		}

		distToPlayer = Vector3.Distance(transform.position, player.position);

		if (distToPlayer < activationDistance) {
			Vector3 playerDirection = (player.position - transform.position).normalized;

			rb.AddForce(playerDirection * (activationDistance / distToPlayer) * (attractionForce * 0.1f));
		}
	}
}
