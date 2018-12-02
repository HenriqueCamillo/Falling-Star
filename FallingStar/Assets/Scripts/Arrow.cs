using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	private Star star;
	public float angle;
	// void Start () {
	// 	star = GameObject.FindGameObjectWithTag("Star").GetComponent<Star>();
	// }
	
	void Update () {
		// angle = Vector2.Angle(Vector2.right, star.impulseDirection);
		// if (star.impulseDirection.y < 0)
		// 	angle += 2 * (180 - angle);
		
		// this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, angle);

	}
}
