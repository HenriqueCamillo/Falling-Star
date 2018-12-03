using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crashable : Interactable {

	Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void effect(GameObject player){
		if(player.GetComponent<PowerUp>().checkPowerUp(3)){
			collider.enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;
			for(int i = 0; i < 3; i++)
				this.transform.GetChild(i).gameObject.SetActive(true);
			
			Invoke("DestroyPlatform", 1.0f);
		}
		
	}

	void DestroyPlatform(){
		Destroy(this.gameObject);
	}
}
