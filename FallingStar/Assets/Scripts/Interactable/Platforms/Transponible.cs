using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transponible : Interactable {

	Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void effect(GameObject player){
		if(player.GetComponent<PowerUp>().checkPowerUp(2))
			collider.enabled = false;
	}

	public override void release(GameObject player){
		if(!collider.enabled)
			collider.enabled = true;
	}
}
