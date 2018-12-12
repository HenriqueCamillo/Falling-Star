using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {
	[SerializeField] GameObject levelSelect;
	[SerializeField] GameObject credits;

	void Start () {
		credits.SetActive(false);
		levelSelect.SetActive(false);
	}

	public void Play () {
		levelSelect.SetActive(true);
	}

	public void Back () {
		levelSelect.SetActive(false);
		credits.SetActive(false);
	}
	
	 public void Credits () {
		credits.SetActive(true);
	 }

	public void Quit () {
		Application.Quit();
	}
}
