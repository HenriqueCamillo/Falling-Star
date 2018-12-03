using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBonus : Interactable {

    bool got = false;

    SpriteRenderer spr;
    // Use this for initialization
    void Start () {
        spr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void effect(GameObject player){
        if (!got){
            StageManager.instance.addStar();
            got = true;
            Invoke("DestroyStar", 0.05f);
		}
    }

	void DestroyStar(){
        spr.enabled = false;
    }

	public void Reset(){
        spr.enabled = true;
        got = false;
    }
}
