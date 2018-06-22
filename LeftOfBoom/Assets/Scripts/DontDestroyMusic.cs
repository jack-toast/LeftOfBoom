/* Code adapted from the following unity forums answer.
 * https://answers.unity.com/questions/1253516/playing-audio-through-multiple-scenes.html
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour {

	void Awake()
	{
		GameObject[] musicObjects = GameObject.FindGameObjectsWithTag ("MusicPlayer");
		if (musicObjects.Length > 1) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}
}
