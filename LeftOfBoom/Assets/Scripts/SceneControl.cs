﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
	public GameObject magneticAsteroidPrefab;
	public int currentPuzzle;
	private int numberPuzzles;

	private GameObject player;

	private List<Vector3> magAsteroidPositions = new List<Vector3>();

	// Use this for initialization
	void Start()
	{
		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		numberPuzzles = checkpoints.Length;
		Debug.Log("Number of checkpoints: " + numberPuzzles.ToString());
		currentPuzzle = 0;
		player = GameObject.FindGameObjectWithTag("Player");

		SaveMagAsteroidPositions();// Save the positions of all the magnetic asteroids

	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();	
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Reset();
		}
	}

	void Reset()
	{
		player.BroadcastMessage("Reset");
		ResetAsteroids();
	}

	void SaveMagAsteroidPositions()
	{
		GameObject[] magAsteroids = GameObject.FindGameObjectsWithTag("MagneticAsteroid");
		foreach (GameObject obj in magAsteroids) {
			Vector3 pos = obj.transform.position;
			magAsteroidPositions.Add(pos);
		}
	}

	void ResetAsteroids()
	{
		GameObject[] magneticAsteroids = GameObject.FindGameObjectsWithTag("MagneticAsteroid");
		Transform magneticAsteroidContainer = GameObject.Find("MagneticAsteroids").transform;
		int numAsteroids = magneticAsteroids.Length;

		for (int i = 0; i < numAsteroids; i++) {
			Destroy(magneticAsteroids[i]);
		}
			
		foreach (Vector3 pos in magAsteroidPositions) {
			Instantiate(magneticAsteroidPrefab, pos, new Quaternion(0, 0, 0, 0), magneticAsteroidContainer);
		}

	}

	public void IncrementPuzzleNumber()
	{
		currentPuzzle++;
		//Debug.Log("[SceneControl]: IncrementPuzzleNumber: currentPuzzle = " + currentPuzzle.ToString());
	}

	public int GetPuzzleNumber(){
		return currentPuzzle;
	}
}