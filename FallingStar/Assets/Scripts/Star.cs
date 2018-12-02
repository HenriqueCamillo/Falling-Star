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
	private bool inSlowMotion;
	private GameObject arrow;
	private SpriteRenderer arrowSprite;
	private float arrowAngle;
	[SerializeField][Range(1f, 50f)] float arrowSizeController;
	private Vector2 arrowSize;
	private Vector2 impulse;
	[SerializeField][Range(1, 500)] float maxImpulse;

	public Vector2 Impulse {
		get{return impulse;}
		set {
			impulse = value;
			
			arrowAngle = Vector2.Angle(Vector2.right, impulse);
			if (impulse.y < 0)
				arrowAngle += 2 * (180 - arrowAngle);

			arrow.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, 
													this.transform.eulerAngles.y, arrowAngle);
			arrow.transform.localScale = new Vector3 (arrowSize.magnitude / arrowSizeController,
											arrow.transform.localScale.y, arrow.transform.localScale.z);
		}
	}
	
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		arrow = GameObject.Find("Arrow");
		arrowSprite = arrow.gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0) && !inSlowMotion) {
			inSlowMotion = true;
			StartCoroutine("CalculateImpulse");
			Time.timeScale = 0.1f;
			arrowSprite.enabled = true;
			Invoke("SlowMotion", slowMotionDuration * Time.timeScale);
		}

		if(inSlowMotion)
			CalculateImpulse();
	}

	void SlowMotion() {
		Time.timeScale = 1f;
		inSlowMotion = false;
		arrowSprite.enabled = false;
		ImpulseStar();
	}

	void CalculateImpulse() {
		#if UNITY_EDITOR_
		#endif
		#if UNITY_WINDOWS
		#endif
		#if UNITY_ANDROID
		#endif

		var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		screenPoint.z = 10f; // Distance from camera
		impulseDirection = this.transform.position - Camera.main.ScreenToWorldPoint(screenPoint);
		Impulse = impulseDirection * impulseForce;
		impulseDirection = impulseDirection * impulseForce;

		Debug.Log(impulse.magnitude);
		if (impulse.magnitude > maxImpulse) {
			Debug.Log("Entrou");
			impulse = impulse.normalized * maxImpulse;
			Debug.Log(impulse.magnitude);
		}
		arrowSize = impulse;
	}
	void ImpulseStar() {
		Debug.DrawLine(impulse, this.transform.position, Color.green, 0.3f);
		rBody.AddForce(impulse, ForceMode2D.Impulse);

		shine -= Vector3.SqrMagnitude(impulseDirection) / impulseForce;
	}
}