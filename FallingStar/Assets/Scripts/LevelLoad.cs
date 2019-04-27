using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoad: MonoBehaviour {
	public int thisLevel; 
	[SerializeField] Button buttonScript;
	[SerializeField] Image buttonImage;

	[SerializeField] int requiredStars;
	[SerializeField] GameObject requirement, starsGotten;

	/// <summary>
	/// Displays the number of stars gotten in the level.
	/// Checks if the level is already unlocked or not, allowing or not the player to play it
	/// </summary>
	void Start () {
		// Displays the number of stars gotten in the level, activating or deactivating the star images.
		GameManager.instance.CalculateNumberOfStars();
		transform.Find("Button").Find("Text").gameObject.GetComponent<Text>().text = this.gameObject.name;

		/* Check if number of stars gotten is equal or higher than the number of stars required to play the level,
		and then, enables or disables the button, and changes its opacity to indicate wheter it's active or not. */
		requiredStars = GameManager.instance.starRequirement[thisLevel-1];
		// Debug.Log("Level " + thisLevel + " requires " + requiredStars + " stars");
		
		if (GameManager.instance.numberOfStars >= requiredStars) {
			buttonScript.enabled = true;

			starsGotten.SetActive(true);
			requirement.SetActive(false);

			if (GameManager.instance.save.stars.ContainsKey(thisLevel)) {
				starsGotten.GetComponentInChildren<Text>().text = GameManager.instance.save.stars[thisLevel].ToString() + "/3";
			} else {
				starsGotten.GetComponentInChildren<Text>().text = "0/3";
			}

			// Changes the opacity of the button
			var tempColor = buttonImage.color;
			tempColor.a = 1f;
			buttonImage.color = tempColor;
		} else {
			starsGotten.SetActive(false);
			requirement.SetActive(true);

			requirement.GetComponentInChildren<Text>().text = "x" + requiredStars.ToString();

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