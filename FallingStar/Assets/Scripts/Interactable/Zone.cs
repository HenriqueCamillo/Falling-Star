using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : Interactable {

	public int powerUpIndex;
	public Particle[] particles;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void effect(GameObject player){
		player.GetComponent<PowerUp>().changePowerUp(powerUpIndex);
		transform.DetachChildren();
		anim.SetTrigger("die");
		foreach (Particle p in particles)
			p.setTarget(player.transform);
		
		Invoke("DestroyZone", 0.3f);
	}

	void DestroyZone(){
		Destroy(this.gameObject);
	}

}
