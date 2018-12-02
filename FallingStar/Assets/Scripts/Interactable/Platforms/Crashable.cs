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
			Invoke("DestroyPlatform", 0.03f);
		}
		
	}

	void DestroyPlatform(){
		Destroy(this.gameObject);
	}
}
