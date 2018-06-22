using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointToCheckpoint : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu") {
			Destroy(gameObject);
			transform.GetChild(0).localPosition = new Vector3(100f, 0, 0);
			return;
		}

		GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

		if (checkpoints.Length == 0) {
			transform.GetChild(0).localPosition = new Vector3(500, 0, 0);
			return;
		}

		GameObject closestCheckpoint = checkpoints[0];
		float minDistance = Vector3.Distance(checkpoints[0].transform.position, transform.position);

		foreach (GameObject item in checkpoints) {
			float temp = Vector3.Distance(item.transform.position, transform.position);
			if (temp < minDistance) {
				closestCheckpoint = item;
				minDistance = temp;
			}
		}

		Debug.Log(closestCheckpoint.name);

		Vector3 checkpointPos = closestCheckpoint.transform.position;
		float angle = Mathf.Atan2((checkpointPos.y - transform.position.y), (checkpointPos.x - transform.position.x)) * Mathf.Rad2Deg - 180;
		transform.eulerAngles = new Vector3(0, 0, angle + 180);

		transform.GetChild(0).localPosition = new Vector3(Mathf.Min(minDistance - 2, Camera.main.orthographicSize) / 4, 0, 0);

	}
}
