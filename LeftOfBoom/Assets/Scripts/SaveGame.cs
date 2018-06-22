using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveGame : ISerializable
{

	/* Things to save:
	 * Player health
	 * Player position
	 * Checkpoint count
	 * 
	 * 
	 */

	public float Health { get; set; }

	public string LevelName { get; set; }

	public Vector3 playerPosition;

	// we have to copy information over from the game model into this class
	// because it is the one that is written to disk and read back from
	public void StoreData(GameSaveModel model)
	{	
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {
			Debug.Log("player object not found");
			return;
		}
		Health = player.GetComponent<PlayerHealth>().GetHealth();

		LevelName = SceneManager.GetActiveScene().name;

		//playerScale = model.player.gameObject.transform.localScale;
		playerPosition = player.transform.position;
		//playerOrientation = model.player.gameObject.transform.rotation;
	}

	// we pass in the game model here so that we can copy information back to
	// the model from this save game object
	public void LoadData(GameSaveModel model)
	{
		if (SceneManager.GetActiveScene().name != LevelName) {
			SceneManager.LoadScene(LevelName);
			return;
		}

		// It only make sense to have the player save and load the level they're on. 

		/*
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {
			Debug.Log("player object not found");
			return;
		}
		player.GetComponent<PlayerHealth>().SetHealth(Health);

		//model.player.gameObject.transform.localScale = playerScale;
		player.gameObject.transform.position = playerPosition;
		//model.player.gameObject.transform.rotation = playerOrientation;
		player.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
		player.GetComponent<Rigidbody2D>().angularVelocity = 0f;

		Debug.Log("LevelToLoad: " + LevelName);
		*/
	}

	// this method is called when your object is serialized--this helps deal with some of the
	// issues around unity objects that aren't flagged as serializable
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("levelName", LevelName);
		info.AddValue("health", Health);
		// Vector3, just like other Unity objects, is not serializable, so
		// we have to break it apart into three values, definitely a major pain
		info.AddValue("posx", playerPosition.x);
		info.AddValue("posy", playerPosition.y);
		info.AddValue("posz", playerPosition.z);
	}

	// we use the empty constructor when creating a save game before writing it to the disk
	public SaveGame()
	{
	}

	// this is a special constructor needed by ISerializable so that we can
	// construct the object from a stream--here we must fill out all the values
	// we saved to the file
	public SaveGame(SerializationInfo info, StreamingContext context)
	{
		LevelName = info.GetString("levelName");
		Health = info.GetInt32("health");
		playerPosition = new Vector3(
			info.GetSingle("posx"),
			info.GetSingle("posy"),
			info.GetSingle("posz"));

	}

}