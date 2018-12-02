using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
	private Touch touch;
	private Rigidbody2D rBody;
	private Vector2 impulseDirection;
	[SerializeField][Range(1f, 10f)] float impulseForce;
	[SerializeField][Range(0f, 1f)] float slowMotionDuration;
	[SerializeField] float shine;
	[SerializeField] private bool inSlowMotion;

	void Start () {
		rBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0) && !inSlowMotion) {
			inSlowMotion = true;
			Time.timeScale = 0.1f;
			Invoke("SlowMotion", slowMotionDuration * Time.timeScale);
		}
		// if (Input.GetMouseButton(0)) {
		// 	var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		// 	screenPoint.z = 10f; // Distance from camera
		// 	this.transform.position =  Camera.main.ScreenToWorldPoint(screenPoint);
		// }
		// if (Input.touchCount > 0) {
		// 	touch = Input.GetTouch(0);		
		// 	this.transform.position = touch.position;
		// }
	}

	void SlowMotion() {
		Time.timeScale = 1f;
		inSlowMotion = false;
		Impulse();
	}

	void Impulse() {
		var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		screenPoint.z = 10f; // Distance from camera
		impulseDirection = this.transform.position - Camera.main.ScreenToWorldPoint(screenPoint);
		impulseDirection *= impulseForce;
		
		Debug.Log(Vector3.SqrMagnitude(impulseDirection));
		Debug.DrawLine(impulseDirection, this.transform.position, Color.green, 0.3f);
		rBody.AddForce(impulseDirection, ForceMode2D.Impulse);

		shine -= Vector3.SqrMagnitude(impulseDirection) / impulseForce;
	}
}