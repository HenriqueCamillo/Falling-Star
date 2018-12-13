using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	[SerializeField] GameObject levelSelect;
	[SerializeField] GameObject options;
	[SerializeField] GameObject deleteSaveConfirmation;
	[SerializeField] GameObject sucessfullyDeleted;
	[SerializeField] GameObject credits;
	[SerializeField] Text numberOfStars;

	void Start () {
		credits.SetActive(false);
		levelSelect.SetActive(false);
		options.SetActive(false);
		deleteSaveConfirmation.SetActive(false);
		sucessfullyDeleted.SetActive(false);

		GameManager.instance.CalculateStarTotal();
		numberOfStars.text = "x" + GameManager.instance.starTotal.ToString();
	}

	public void Play () {
		levelSelect.SetActive(true);
	}

	public void Back () {
		levelSelect.SetActive(false);
		credits.SetActive(false);
		options.SetActive(false);
		deleteSaveConfirmation.SetActive(false);
	}

	 public void Options() {
		options.SetActive(true);
	 }
	
	 public void Credits () {
		credits.SetActive(true);
	 }

	public void Quit () {
		Application.Quit();
	}

	// Save deletion mode
	private enum mode {
		askForConfirmation = 0,
		confirm = 1,
		cancel = 2

	}

	public void DeleteSaveData (int deletionMode) {
		if (deletionMode == (int)mode.askForConfirmation) {
			deleteSaveConfirmation.SetActive(true);
		} else {
			if (deletionMode == (int)mode.confirm){
				GameManager.instance.ResetSaveData();
				sucessfullyDeleted.SetActive(true);
			} else if (deletionMode == (int)mode.cancel) {
				deleteSaveConfirmation.SetActive(false);
			}
		}
	}

	// Dialogue after deleting save
	public void Ok () {
		SceneManager.LoadScene(0);
		options.SetActive(true);
	}
}
