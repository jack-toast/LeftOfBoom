using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
	public GameObject magneticAsteroidPrefab;
	public int currentPuzzle;
	public string nextLevel;
	public GameObject loadingOverlay;
	public GameObject levelCompleteText;
	public GameObject nextLevelButton;
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
		if (Input.GetKeyDown(KeyCode.R)) {
			Reset();
		}

		if (currentPuzzle >= numberPuzzles) {

			if (!levelComplete) {
				GameObject canvas = GameObject.FindWithTag("Canvas");

				if (canvas == null) {
					return;
				}

				Instantiate(levelCompleteText, canvas.transform);

				if (GameObject.FindWithTag("NextLevelButton") == null) {
					Instantiate(nextLevelButton, canvas.transform);
				} 

			}

			levelComplete = true;

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
			blep.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
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
		// Display some sort of loading screen
		Instantiate(loadingOverlay, GameObject.Find("Canvas").transform);

		SceneManager.LoadScene(nextLevel);
	}
}
