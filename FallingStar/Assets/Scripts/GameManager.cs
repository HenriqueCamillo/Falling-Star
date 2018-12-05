using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	int[] stars;
	int starTotal;
	public int currentLevel;
	[SerializeField] int levelQuantity;

	public AudioSource audioSource;
	public AudioClip[] audioClip;

	public void NextStage () {
		currentLevel++;
		Debug.Log(currentLevel);
		if (currentLevel > levelQuantity)
			currentLevel = 0;
		SceneManager.LoadScene(currentLevel);		
	}
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this);
		}

		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClip[0];
		audioSource.Play();
	}

	public void SaveGame () {
		
	}
}