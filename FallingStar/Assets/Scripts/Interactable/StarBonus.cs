using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBonus : Interactable {

    Color erasedColor;
    public Image emblem;

    bool got = false;

    SpriteRenderer spr;
    // Use this for initialization
    void Start () {
        erasedColor = emblem.color;
        spr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void effect(GameObject player){
        if (!got){
            StageManager.instance.addStar();
            emblem.color = Color.yellow;
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
        emblem.color = erasedColor;
		
    }
}
