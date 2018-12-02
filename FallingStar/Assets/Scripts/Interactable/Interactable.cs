using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

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
