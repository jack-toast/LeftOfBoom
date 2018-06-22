using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	public void LoadMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void QuitApplication()
	{
		Application.Quit ();
	}

	public void SaveGame(){
		GameObject SaveGO = GameObject.FindWithTag ("GameSaveModel");

		if (SaveGO == null) {
			Debug.Log ("no GameSaveModel object found");
			return;
		}

		SaveGO.GetComponent<GameSaveModel> ().OnSaveClick ();
	}

}
