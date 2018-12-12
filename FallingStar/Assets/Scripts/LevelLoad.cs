using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad: MonoBehaviour {
	public int thisLevel; 
	[SerializeField] GameObject star1, star2, star3;

	void Start () {
		transform.Find("Button").Find("Text").gameObject.GetComponent<Text>().text = this.gameObject.name;

		if (GameManager.instance.save.stars[thisLevel] == 3) {
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
		} else if (GameManager.instance.save.stars[thisLevel] == 2) {
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(false);
		} else if (GameManager.instance.save.stars[thisLevel] == 2) {
			star1.SetActive(true);
			star2.SetActive(false);
			star3.SetActive(false);
		} else {
			star1.SetActive(false);
			star2.SetActive(false);
			star3.SetActive(false);
		}
	}

	public void LoadLevel () {
		DontDestroyOnLoad(GameManager.instance);
		GameManager.instance.inGame = true;
		GameManager.instance.audioSource.Stop();
		GameManager.instance.audioSource.clip = GameManager.instance.audioClip[1];
		GameManager.instance.audioSource.Play();
		GameManager.instance.currentLevel = thisLevel;
		SceneManager.LoadScene(thisLevel);		
	}
}