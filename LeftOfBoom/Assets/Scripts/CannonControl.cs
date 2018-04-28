﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{

	public float outerRadius = 4f;
	public float innerRadius = 1f;
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
				chargeDirection = transform.position - mousePosition;
			} else {
				aimOuter.SetColorOutOfBounds();
				aimArrow.Deactivate();
			}

			if (mouseDistance > outerRadius + 0.5f) {
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
		}

		if (!Input.GetMouseButton(0) && charging) {
			mouseDistance = Mathf.Clamp(mouseDistance, outerRadius, innerRadius);
			rb.AddForce(chargeDirection.normalized * mouseDistance * 100);
			charging = false;
			GameObject particles = Instantiate(boostParticles, transform);
			particles.transform.parent = null;
			return;
		}


	}


}
