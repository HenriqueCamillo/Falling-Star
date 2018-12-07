using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManager : MonoBehaviour {
	private	Save save;
	public static GameManager instance;
	// public int[] stars;
	public int starTotal;
	public int currentLevel;
	public int numberOfLevels;
	public AudioSource audioSource;
	public AudioClip[] audioClip;
	private Stream saveStream;
	private IFormatter formatter;

	public void NextStage () {
		currentLevel++;
		if (currentLevel > numberOfLevels) {
			currentLevel = 0;
			audioSource.Stop();
			audioSource.PlayOneShot(audioClip[0]);
		}
		SceneManager.LoadScene(currentLevel);		
	}

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		if (instance == this) {
			audioSource = GetComponent<AudioSource>();
			audioSource.PlayOneShot(audioClip[0]);

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
	}
}