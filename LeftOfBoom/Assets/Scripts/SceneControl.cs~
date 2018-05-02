using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControl : MonoBehaviour
{
	public GameObject magneticAsteroidPrefab;
	public int currentPuzzle;
	public int levelNumber = 1;

	private int numberPuzzles;

	private GameObject player;

	private List<AsteroidStruct> magAsteroids = new List<AsteroidStruct>();

	private bool levelComplete;


	// Use this for initialization
	void Start()
	{
		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		numberPuzzles = checkpoints.Length;
		Debug.Log("Number of checkpoints: " + numberPuzzles.ToString());
		currentPuzzle = 0;
		player = GameObject.FindGameObjectWithTag("Player");

		levelComplete = false;

		SaveMagAsteroidPositions();// Save the positions of all the magnetic asteroids
		ResetAsteroids();
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

		if (currentPuzzle >= numberPuzzles) {
			levelComplete = true;

			if (GameObject.Find("NextLevelButton") != null) {
				GameObject.Find("NextLevelButton").GetComponent<Button>().enabled = true;

			}

		}

		if (levelComplete) {
			Debug.Log("Level Complete! WOO!");
		}

	}

	void Reset()
	{
		player.BroadcastMessage("Reset");
		ResetAsteroids();
	}

	void SaveMagAsteroidPositions()
	{
		magAsteroids.Clear();
		GameObject[] magAsts = GameObject.FindGameObjectsWithTag("MagneticAsteroid");
		foreach (GameObject obj in magAsts) {
			Vector3 pos = obj.transform.position;
			magAsteroids.Add(new AsteroidStruct(obj.transform.position.x, obj.transform.position.y, Mathf.Max(obj.transform.localScale.x, obj.transform.localScale.y) / 2, true));
		}
	}

	void ResetAsteroids()
	{
		DestroyMagneticAsteroids();
		Transform magneticAsteroidContainer = GameObject.Find("MagneticAsteroids").transform;

		foreach (AsteroidStruct item in magAsteroids) {
			Vector3 spawnPosition = new Vector3(item.position.x, item.position.y, 0);
			GameObject blep = Instantiate(magneticAsteroidPrefab, spawnPosition, new Quaternion(0, 0, 0, 0), magneticAsteroidContainer).gameObject;			
			blep.transform.localScale = new Vector3(item.radius * 2.5f, item.radius * 2.5f, 1);

		}

	}

	void DestroyMagneticAsteroids()
	{
		GameObject[] magneticAsteroids = GameObject.FindGameObjectsWithTag("MagneticAsteroid");
		int numAsteroids = magneticAsteroids.Length;

		for (int i = 0; i < numAsteroids; i++) {
			Destroy(magneticAsteroids[i]);
		}
	}

	void DestroyAsteroids()
	{
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		int numAsteroids = asteroids.Length;

		for (int i = 0; i < numAsteroids; i++) {
			Destroy(asteroids[i]);
		}
	}

	void DestroyCheckpoints()
	{
		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		int numAsteroids = checkpoints.Length;

		for (int i = 0; i < numAsteroids; i++) {
			Destroy(checkpoints[i]);
		}
	}

	public void IncrementPuzzleNumber()
	{
		currentPuzzle++;
		//Debug.Log("[SceneControl]: IncrementPuzzleNumber: currentPuzzle = " + currentPuzzle.ToString());
	}

	public int GetPuzzleNumber()
	{
		return currentPuzzle;
	}

	public void LoadNextLevel()
	{
		DestroyAsteroids();
		DestroyMagneticAsteroids();

		currentPuzzle = 0;
		levelComplete = false;
		GetComponent<LoadAsteroidsFromImage>().LoadLevel(levelNumber + 1);
		GameObject.Find("NextLevelButton").GetComponent<Button>().enabled = false;

		SaveMagAsteroidPositions();

		player.transform.position = new Vector3(0f, 0f, 0f);
		player.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

		player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
		player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
	}
}
