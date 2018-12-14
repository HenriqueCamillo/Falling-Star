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

	/// <summary>
	/// Displays the number of stars gotten in the level.
	/// Check if the level is already unlocked or not, allowing or not the player to play it
	/// </summary>
	void Start () {
		// Displays the number of stars gotten in the level, activating or deactivating the star images.
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

		/* Check if number of stars gotten is equal or higher than the number of stars required to play the level,
		and then, enables or disables the button, and changes its opacity to indicate wheter it's active or not. */
		requiredStars = GameManager.instance.starRequirement[thisLevel-1];
		Debug.Log("Level " + thisLevel + " requires " + requiredStars + " stars");
		
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

	/// <summary>
	/// Loads level related to this button, and changes the music.
	/// </summary>
	public void LoadLevel () {
		DontDestroyOnLoad(GameManager.instance);
		
		// Changes music
		GameManager.instance.audioSource.Stop();
		GameManager.instance.audioSource.clip = GameManager.instance.audioClip[1];
		GameManager.instance.audioSource.Play();

		GameManager.instance.inGame = true;
		GameManager.instance.currentLevel = thisLevel;

		SceneManager.LoadScene(thisLevel);		
	}
}