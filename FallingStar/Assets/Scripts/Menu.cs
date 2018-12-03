using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	[SerializeField] GameObject levelSelect;
	public void Play () {
		levelSelect.SetActive(true);
	}

	public void Back () {
		levelSelect.SetActive(false);
	}
	
	public void LoadLevel (int level) {
		SceneManager.LoadScene(level);		
	}

	public void Quit () {
		Application.Quit();
	}
}
