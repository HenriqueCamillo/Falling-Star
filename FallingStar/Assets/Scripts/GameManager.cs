using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	int[] stars;
	int starTotal;
	int currentLevel;

	public AudioSource audioSource;
	public AudioClip[] audioClip;


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
	
	void Update() {
		if (audioSource.isPlaying) {
			Debug.Log("Playing");
		}
	}
}