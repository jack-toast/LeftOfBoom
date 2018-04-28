using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct AsteroidStruct
{
	public Vector3 position;
	public float radius;

	public AsteroidStruct(float x, float y, float rad)
	{
		position = new Vector3(x, y, 0f);
		radius = rad;
	}
};

public class LoadAsteroidsFromImage : MonoBehaviour
{

	public Texture2D image;
	public GameObject asteroidPrefab;


	public float xShift = 30f;
	public float yShift = 256f;

	private Transform asteroidContainer;

	// Use this for initialization
	void Start()
	{
		asteroidContainer = GameObject.Find("Asteroids").transform;

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

		for (int i = 0; i < height; i++) {
			for (int j = 0; j < width; j++) {
				if ((image.GetPixel(j, i) != Color.black) && (image.GetPixel(j, i) != Color.white)) {
					Debug.Log("(" + j + "," + i + ")" + " not white or black, " + image.GetPixel(j, i));
					return;
				}

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
			}
		}

		/*
		Debug.Log("leftBound = " + leftBound);
		Debug.Log("rightBound = " + rightBound);
		Debug.Log("bottomBound = " + bottomBound);
		Debug.Log("topBound = " + topBound);
		*/

		float density = ((float)activeCount) / (width * height);
		int attemptsPerRadius = ((int)(density * 25000));

		attemptsPerRadius = Mathf.Clamp(attemptsPerRadius, 500, 5000);


		Debug.Log("Density: " + density.ToString() + ", Attempts Calculated: " + attemptsPerRadius.ToString());

		float maxRadius = 5f;
		float minRadius = 0.5f;
		float radius = maxRadius;

		List<AsteroidStruct> roids = new List<AsteroidStruct>();

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
						if (image.GetPixel(j, i) == Color.white) {
							collided = true;
							break;
						}
					} 
				}

				if (collided) {
					attemptCount++;
					continue;
				}

				roids.Add(new AsteroidStruct(xRand, yRand, radius));
				attemptCount++;
			}

			radius *= 0.8f;
		}

		foreach (AsteroidStruct item in roids) {
			//Debug.Log("pos: " + item.position + ", rad: " + item.radius);
			Vector3 spawnPosition = new Vector3(item.position.x - xShift, item.position.y - yShift, 0);
			GameObject blep = Instantiate(asteroidPrefab, spawnPosition, new Quaternion(0, 0, 0, 0), asteroidContainer).gameObject;
			blep.transform.localScale = new Vector3(item.radius * 2, item.radius * 2, 1);
		}


	}
	
	// Update is called once per frame
	void Update()
	{
		
	}
}
