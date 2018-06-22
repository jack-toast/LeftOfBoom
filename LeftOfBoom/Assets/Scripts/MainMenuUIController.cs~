using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{

	public GameObject loadingOverlay;

	public void StartGame()
	{
		Instantiate(loadingOverlay, GameObject.Find("Canvas").transform);
		SceneManager.LoadScene("level1");
	}

	public void LoadGame()
	{
		GameObject SaveGO = GameObject.FindWithTag("GameSaveModel");
		if (SaveGO == null) {
			Debug.Log("no GameSaveModel object found");
			return;
		}
		Instantiate(loadingOverlay, GameObject.Find("Canvas").transform);
		SaveGO.GetComponent<GameSaveModel>().OnLoadClick();
	}

	public void LoadCredits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void QuitApplication()
	{
		Application.Quit();
	}


}
