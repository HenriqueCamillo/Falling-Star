using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad: MonoBehaviour {
	public int thisLevel; 
	[SerializeField] GameObject star1, star2, star3;
	[SerializeField] Button buttonScript;
	[SerializeField] Image buttonImage;

	[SerializeField] int requiredStars;

	void Start () {
		// Displays the number of stars gotten in the level
		transform.Find("Button").Find("Text").gameObject.GetComponent<Text>().text = this.gameObject.name;
		if (GameManager.instance.save.stars.ContainsKey(thisLevel)){
			if (GameManager.instance.save.stars[thisLevel] == 3) {
				star1.SetActive(true);
				star2.SetActive(true);
				star3.SetActive(true);
			} else if (GameManager.instance.save.stars[thisLevel] == 2) {
				star1.SetActive(true);
				star2.SetActive(true);
				star3.SetActive(false);
			} else if (GameManager.instance.save.stars[thisLevel] == 1) {
				star1.SetActive(true);
				star2.SetActive(false);
				star3.SetActive(false);
			} else {
				star1.SetActive(false);
				star2.SetActive(false);
				star3.SetActive(false);
			}
		} else {
			star1.SetActive(false);
			star2.SetActive(false);
			star3.SetActive(false);
		}

		// Check if number of stars gotten is equal or higher than the number of stars required to play the level
		GameManager.instance.CalculateNumberOfStars();
		if (GameManager.instance.numberOfStars >= requiredStars) {
			buttonScript.enabled = true;

			// Changes the opacity of the button
			var tempColor = buttonImage.color;
			tempColor.a = 1f;
			buttonImage.color = tempColor;
		} else {
			buttonScript.enabled = false;

			// Changes the opacity of the button
			var tempColor = buttonImage.color;
			tempColor.a = 0.5f;
			buttonImage.color = tempColor;
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