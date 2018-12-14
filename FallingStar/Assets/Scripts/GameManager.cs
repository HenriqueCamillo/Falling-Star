using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public Save save;
	public int numberOfStars;
	public int currentLevel;
	public int numberOfLevels;
	public AudioSource audioSource;
	public AudioClip[] audioClip;
	public bool inGame = false;
	private Stream saveStream;
	private IFormatter formatter = new BinaryFormatter();
	public List<int> starRequirement = new List<int>();

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		if (instance == this) {
			audioSource = GetComponent<AudioSource>();
			audioSource.clip = audioClip[0];
			audioSource.Play();


			if (File.Exists(Application.persistentDataPath + "\\Save.bin")) {
				saveStream = new FileStream(Application.persistentDataPath + "\\Save.bin", FileMode.Open, FileAccess.Read);
				if (saveStream.Length != 0) {
					save = (Save)formatter.Deserialize(saveStream);
					saveStream.Close();
				}
				else {
					save = new Save();
				}
			}
			else {
				saveStream = new FileStream(Application.persistentDataPath + "\\Save.bin", FileMode.Create, FileAccess.Write);
				save = new Save();
				saveStream.Close();
			}

			foreach (KeyValuePair<int, int> entry in save.stars) {
				Debug.Log("Level " + entry.Key + ": " + entry.Value + " stars");
			}
		}
	}

	/// <summary>
	/// Loads next stage
	/// </summary>
	public void NextStage () {
		currentLevel++;
		// if (currentLevel > numberOfLevels) {
		// 	currentLevel = 0;
		// 	audioSource.Stop();
		// 	audioSource.clip = audioClip[0];
		// 	audioSource.Play();
		// 	SceneManager.LoadScene(currentLevel);
		// } else 
		if (numberOfStars < starRequirement[currentLevel-1]) {
			StageManager.instance.insufficientStars.SetActive(true);
		} else {
			GameManager.instance.inGame = true;
			SceneManager.LoadScene(currentLevel);
		}
	}

	/// <summary>
	/// Updates the number of stars gotten until now.
	/// Replaces the number of stars of the current level if it's higher than the previous value.
	/// Writes the save dictionary data to the save file (in binary format)
	/// </summary>
	/// <param name="levelStars">The number of stars gotten in the current level</param>
	public void SaveGame (int levelStars) {
		CalculateNumberOfStars();

		// Replaces value if higher than previous
		if (save.stars.ContainsKey(currentLevel)) {
			if(levelStars > save.stars[currentLevel]) {
				save.stars[currentLevel] = levelStars;
			}
		}
		else {
			save.stars[currentLevel] = levelStars;
		}

		// Writes data to save file
		saveStream = new FileStream(Application.persistentDataPath + "\\Save.bin", FileMode.Open, FileAccess.Write);
		formatter.Serialize(saveStream, save);
		saveStream.Close();
	}

	/// <summary>
	/// Resets the save dictionary and writes changes to the save file (in binary format)
	/// </summary>
	public void ResetSaveData () {
		saveStream = new FileStream(Application.persistentDataPath +"\\Save.bin", FileMode.Open, FileAccess.Write);

		save.stars.Clear();

		formatter.Serialize(saveStream, save);
		saveStream.Close();
	}
	
	/// <summary>
	/// Iterates in the save dictionary and count all stars
	/// </summary>
	public void CalculateNumberOfStars () {
		numberOfStars = 0;
		foreach (KeyValuePair<int, int> level in save.stars) {
			numberOfStars += save.stars[level.Key];
		}
	}
}