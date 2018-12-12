using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
	private Touch touch;
	private Rigidbody2D rBody;
	private Vector2 impulseDirection;
	[SerializeField][Range(1f, 100f)] float impulseForce;
	[SerializeField][Range(0f, 1f)] float slowMotionDuration;
	[SerializeField] float maxShine;
	private float shine;
	private bool inSlowMotion;
	private GameObject arrow;
	private SpriteRenderer arrowSprite;
	private float arrowAngle;
	[SerializeField][Range(1f, 50f)] float arrowSizeController;
	private Vector2 arrowSize;
	private Vector2 impulse;
	[SerializeField][Range(1, 500)] float maxImpulse;
	private Vector3 screenPoint;
	[SerializeField][Range(1, 10)] float maxFreeFalling;
	[SerializeField] Image shineBar;
	[SerializeField] Camera cmCamera;
	public GameObject gameOverScreen;
	[SerializeField] float maxVelocity;

	public float Shine {
		get{return shine;}
		set {
			shine = value;
			if (shine <= 0){
				shine = 0;
				GameManager.instance.inGame = false;
				Time.timeScale = 0f;
				gameOverScreen.SetActive(true);
			}
			if(shine > maxShine)
                shine = maxShine;
            shineBar.fillAmount = shine / maxShine;
		}
	}
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
		Shine = maxShine;
		gameOverScreen.SetActive(false);
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0) && !inSlowMotion && GameManager.instance.inGame) {
			inSlowMotion = true;
			rBody.velocity = Vector3.zero;
			StartCoroutine("CalculateImpulse");
			Time.timeScale = 0.1f;
			arrowSprite.enabled = true;
			Invoke("SlowMotion", slowMotionDuration * Time.timeScale);
		}

		if(inSlowMotion)
			CalculateImpulse();

		if(rBody.velocity.y <= -maxFreeFalling) {
			rBody.gravityScale = 0f;
		} else {
			rBody.gravityScale = 1f;
		}

		if (rBody.velocity.magnitude > maxVelocity) {
			rBody.velocity = rBody.velocity.normalized * maxVelocity;
		}
	}

	void SlowMotion() {
		Time.timeScale = 1f;
		inSlowMotion = false;
		arrowSprite.enabled = false;
		ImpulseStar();
	}

	void CalculateImpulse() {
		screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		screenPoint.z = 10f; // Distance from camera
		
		impulseDirection = this.transform.position - cmCamera.ScreenToWorldPoint(screenPoint);
		Impulse = impulseDirection * impulseForce;
		impulseDirection = impulseDirection * impulseForce;

		if (impulse.magnitude > maxImpulse) {
			impulse = impulse.normalized * maxImpulse;
		}
		arrowSize = impulse;
	}
	void ImpulseStar() {
		Debug.DrawLine(impulse, this.transform.position, Color.green, 0.3f);
		rBody.AddForce(impulse, ForceMode2D.Impulse);

		Shine -= Vector3.SqrMagnitude(impulse);
	}
}