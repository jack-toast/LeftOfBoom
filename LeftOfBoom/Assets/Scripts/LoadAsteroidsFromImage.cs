using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AsteroidStruct
{
	public Vector3 position;
	public float radius;
	public bool isMagnetic;

	public AsteroidStruct(float x, float y, float rad, bool isMag)
	{
		position = new Vector3(x, y, 0f);
		radius = rad;
		isMagnetic = isMag;
	}
};

public class LoadAsteroidsFromImage : MonoBehaviour
{
	public Texture2D image;
	public GameObject asteroidPrefab;
	public GameObject magAsteroidPrefab;

	public float xShift = 30f;
	public float yShift = 256f;

	public float maxRadius = 5f;
	public float minRadius = 0.5f;

	public float magRadius = 1f;

	private Transform asteroidContainer;
	private Transform magAsteroidContainer;

	// Use this for initialization
	void Awake()
	{
		LoadLevel();
	}

	public void LoadLevel()
	{
		asteroidContainer = GameObject.Find("Asteroids").transform;
		magAsteroidContainer = GameObject.Find("MagneticAsteroids").transform;

		int width = image.width;
		int height = image.height;
		Debug.Log("Width: " + image.width + ", Height: " + image.height);

		Debug.Log("(0,0): " + image.GetPixel(0, 0));
		Debug.Log("(256,256): " + image.GetPixel(256, 256));

		int leftBound = width - 1;
		int rightBound = 0;
		int bottomBound = height - 1;
		int topBound = 0;
		int activeCount = 0;
		int magnetCount = 0;

		List<Vector2Int> magnetPositions = new List<Vector2Int>();

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {

				if (image.GetPixel(j, i) == Color.black) {
					activeCount++;

					if (j < leftBound) {
						leftBound = j;
					}
					if (j > rightBound) {
						rightBound = j;
					}
					if (i < bottomBound) {
						bottomBound = i;
					}
					if (i > topBound) {
						topBound = i;
					}
				}

				if (image.GetPixel(j, i) == Color.red) {
					magnetPositions.Add(new Vector2Int(j, i));
					magnetCount++;
				}
			}
		}

		List<AsteroidStruct> roids = new List<AsteroidStruct>();

		for (int k = 0; k < magnetCount; k++) {
			Vector2Int temp = magnetPositions[k];

			bool collided = false;

			float randoRadius = Random.Range(0.3f, magRadius);

			foreach (AsteroidStruct item in roids) {
				if (Vector2.Distance(temp, item.position) < (item.radius + randoRadius)) {
					collided = true;
					break;
				}
			}

			if (collided) {
				continue;
			}

			// Check collisions with white pixels
			for (int i = Mathf.FloorToInt(temp.y - randoRadius); i < Mathf.CeilToInt(temp.y + randoRadius); i++) {
				for (int j = Mathf.FloorToInt(temp.x - randoRadius); j < Mathf.CeilToInt(temp.x + randoRadius); j++) {
					if (image.GetPixel(j, i) != Color.red) {
						collided = true;
						break;
					}
				}
			}

			if (collided) {
				continue;
			}



			roids.Add(new AsteroidStruct(temp.x, temp.y, randoRadius, true));

		}

		float density = ((float)activeCount) / (width * height);
		int attemptsPerRadius = ((int)(density * 25000));

		attemptsPerRadius = Mathf.Clamp(attemptsPerRadius, 500, 5000);

		Debug.Log("Density: " + density.ToString() + ", Attempts Calculated: " + attemptsPerRadius.ToString());

		float radius = maxRadius;

		while (radius > minRadius) {
			int attemptCount = 0;
			while (attemptCount < attemptsPerRadius) {
				float xRand = Random.Range(leftBound + radius, rightBound - radius);
				float yRand = Random.Range(bottomBound + radius, topBound - radius);

				Vector2 randomPosition = new Vector2(xRand, yRand);

				bool collided = false;
				// Check collisions with asteroids
				foreach (AsteroidStruct asteroid in roids) {
					if (Vector2.Distance(randomPosition, asteroid.position) < (radius + asteroid.radius)) {
						collided = true;
						break;
					}
				}

				if (collided) {
					attemptCount++;
					continue;
				}

				// Check collisions with white pixels
				for (int i = Mathf.FloorToInt(yRand - radius); i < Mathf.CeilToInt(yRand + radius); i++) {
					for (int j = Mathf.FloorToInt(xRand - radius); j < Mathf.CeilToInt(xRand + radius); j++) {
						if (image.GetPixel(j, i) != Color.black) {
							collided = true;
							break;
						}
					}
				}

				if (collided) {
					attemptCount++;
					continue;
				}

				roids.Add(new AsteroidStruct(xRand, yRand, radius, false));
				attemptCount++;
			}

			radius *= 0.8f;
		}

		foreach (AsteroidStruct item in roids) {
			//Debug.Log("pos: " + item.position + ", rad: " + item.radius);
			Vector3 spawnPosition = new Vector3(item.position.x - xShift, item.position.y - yShift, 0);

			if (item.isMagnetic) {
				GameObject blep = Instantiate(magAsteroidPrefab, spawnPosition, new Quaternion(0, 0, 0, 0), magAsteroidContainer).gameObject;			
				blep.transform.localScale = new Vector3(item.radius * 2, item.radius * 2, 1);
				blep.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
			} else {
				GameObject blep = Instantiate(asteroidPrefab, spawnPosition, new Quaternion(0, 0, 0, 0), asteroidContainer).gameObject;			
				blep.transform.localScale = new Vector3(item.radius * 2, item.radius * 2, 1);
				blep.transform.Rotate(0f, 0f, Random.Range(0f, 360f));
			}
		}


	}

}
