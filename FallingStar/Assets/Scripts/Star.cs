using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour {
	[SerializeField] Camera cmCamera;
	private Vector3 screenPoint;
	private Rigidbody2D rBody;
	private Vector2 impulseDirection;
	private Vector2 impulse;
	[SerializeField][Range(1f, 100f)] float impulseForce;
	[SerializeField][Range(1, 500)] float maxImpulse;
	[SerializeField] float maxShine;
	private float shine;
	private Image shineBar;
	[SerializeField][Range(0f, 1f)] float slowMotionDuration;
	private bool inSlowMotion;
	[SerializeField] float maxVelocity;
	[SerializeField][Range(1f, 50f)] float arrowSizeController;
	private GameObject arrow;
	private SpriteRenderer arrowSprite;
	private float arrowAngle;
	private Vector2 arrowSize;
	[SerializeField][Range(1, 10)] float maxFreeFalling;
	private RaycastHit2D hit;
	private bool canImpulse;

	public float Shine {
		get{return shine;}
		set {
			shine = value;
			if (shine <= 0){
				shine = 0;
				GameManager.instance.inGame = false;
				StageManager.instance.GameOver();
			}
			if (shine > maxShine) {
                shine = maxShine;
			}
            shineBar.fillAmount = shine / maxShine;
		}
	}
	
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		arrow = GameObject.Find("Arrow");
		arrowSprite = arrow.gameObject.GetComponent<SpriteRenderer>();
		shineBar = GameObject.FindGameObjectWithTag("ShineBar")	.GetComponent<Image>();

		Shine = maxShine;
		canImpulse = true;
	}

	void Update () {
		hit = Physics2D.Raycast(cmCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.up);
		if (hit.collider && hit.collider.name == "Pause") {
			canImpulse = false;
		} else {
			canImpulse = true;
		}

		canImpulse = canImpulse & GameManager.instance.inGame;

		if (Input.GetMouseButtonDown(0) && !inSlowMotion && canImpulse) {
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
		Debug.DrawLine(this.transform.position,impulse, Color.green, 0.3f);
		rBody.AddForce(impulse, ForceMode2D.Impulse);

		Shine -= Vector3.SqrMagnitude(impulse);
	}
}