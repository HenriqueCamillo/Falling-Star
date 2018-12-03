using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	int[] stars;
	int starTotal;
	int currentLevel;
	[SerializeField] int levelQuantity;

	public AudioSource audioSource;
	public AudioClip[] audioClip;

	public void NextStage () {
		currentLevel++;
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
}