using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transponible : Interactable {

	Collider2D collider;
	SpriteRenderer spr;

	// Use this for initialization
	void Start () {
		collider = this.GetComponent<Collider2D>();
		spr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(collider.enabled)
			spr.color = new Color(spr.color.r,spr.color.g,spr.color.b, Mathf.Lerp(spr.color.a, 1.0f, 0.3f));
		else
			spr.color = new Color(spr.color.r,spr.color.g,spr.color.b, Mathf.Lerp(spr.color.a, 0.3f, 0.3f));
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
