using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Interactable {

	public float angle, start;
	float prevX = 1.12f, factor = 1.0f;
	bool chase = false;
	Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!chase){
			float x = 1.12f - Mathf.PingPong(2* Time.time + start, 2.24f);

			if((factor == 1.0f && prevX < x) || (factor == -1.0f && prevX > x))
				factor = -factor;

			prevX = x;
			this.transform.localPosition = Quaternion.Euler(0.0f, 0.0f, angle) * (new Vector3(x, factor * Mathf.Sqrt(1.12f*1.12f - x*x)/5, 0.0f));
		}else{
			this.transform.position = Vector3.Lerp(this.transform.position, target.position, 0.3f);
			if((this.transform.position - target.position).magnitude < 0.1f)
				Destroy(this.gameObject);
		}

	}

	public void setTarget(Transform t){
		target = t;
		chase = true;
	}

	public override void effect(GameObject player){
		if(chase)
			Destroy(this.gameObject);
	}
}
