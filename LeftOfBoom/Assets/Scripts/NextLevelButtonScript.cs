using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButtonScript : MonoBehaviour
{

	public void LoadNextLevel()
	{
		Debug.Log("[LoadNextLevel]");
		GameObject.FindWithTag("SceneController").GetComponent<SceneControl>().LoadNextLevel();
	}
}
