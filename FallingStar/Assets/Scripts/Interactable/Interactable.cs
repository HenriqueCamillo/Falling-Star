using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player")
			effect(col.gameObject);
	}

	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "Player")
			release(col.gameObject);
	}

	public virtual void effect(GameObject player){}

	public virtual void release(GameObject player){}


}
