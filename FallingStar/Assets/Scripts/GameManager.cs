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
	public int starTotal;
	public int currentLevel;
	public int numberOfLevels;
	public AudioSource audioSource;
	public AudioClip[] audioClip;
	public bool inGame;
	private Stream saveStream;
	private IFormatter formatter;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		if (instance == this) {
			inGame = false;

			audioSource = GetComponent<AudioSource>();
			audioSource.clip = audioClip[0];
			audioSource.Play();

			formatter = new BinaryFormatter(); 

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

	public void NextStage () {
		currentLevel++;
		if (currentLevel > numberOfLevels) {
			currentLevel = 0;
			audioSource.Stop();
			audioSource.clip = audioClip[0];
			audioSource.Play();
		} else {
			GameManager.instance.inGame = true;
		}
		SceneManager.LoadScene(currentLevel);		
	}

	public void SaveGame (int levelStars) {
		saveStream = new FileStream(Application.persistentDataPath + "\\Save.bin", FileMode.Open, FileAccess.Write);

		if (save.stars.ContainsKey(currentLevel)) {
			if(levelStars > save.stars[currentLevel]) {
				save.stars[currentLevel] = levelStars;
			}
		}
		else {
			save.stars[currentLevel] = levelStars;
		}

		formatter.Serialize(saveStream, save);
		saveStream.Close();

		CalculateStarTotal();
	}

	public void ResetSaveData () {
		saveStream = new FileStream(Application.persistentDataPath +"\\Save.bin", FileMode.Open, FileAccess.Write);

		save.stars.Clear();

		formatter.Serialize(saveStream, save);
		saveStream.Close();
	}
	
	public void CalculateStarTotal () {
		starTotal = 0;
		foreach (KeyValuePair<int, int> level in save.stars) {
			starTotal += save.stars[level.Key];
		}
	}
}