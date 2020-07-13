using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Star : MonoBehaviour {
	[SerializeField] Camera cmCamera;
	private Vector3 screenPoint;
	private Rigidbody2D rBody;
	private Vector2 impulseDirection;
	private Vector2 impulse;
	[SerializeField][Range(1f, 100f)] float impulseForce;
	[SerializeField][Range(1, 500)] float maxImpulse;
	[SerializeField] float maxShine;
	[SerializeField] float shine;
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
	private bool canImpulse = true;
	[SerializeField] bool noShine = false;
	[SerializeField] RectTransform pauseButton;
	private bool clickOnUI = false;

	public float Shine {
		get{return shine;}
		set {
			// If tried to use shine it didn't have, game over
			if (noShine && value <= shine) {
				GameManager.instance.inGame = false;
				StageManager.instance.GameOver();
				return;
			}

			// If recovering shine
			if (value > shine) {
				noShine = false;

				// Sets shine to value, limiting it to maxShine
				if (value > maxShine) {
					shine = maxShine;
				} else {
					shine = value;
				}
			} else {
				// Sets shine to value, limiting it to positive values
				if (value <= 0) {
					shine = 0;
					noShine = true;
				} else {
					shine = value;
				}
			}

			//  Moves the bar
            shineBar.fillAmount = shine / maxShine;
		}
	}
	
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		arrow = GameObject.Find("Arrow");
		arrowSprite = arrow.gameObject.GetComponent<SpriteRenderer>();
		shineBar = GameObject.FindGameObjectWithTag("ShineBar")	.GetComponent<Image>();

		Shine = maxShine;
	}

	void Update () {
		Vector3 mouse = cmCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 pause = cmCamera.ScreenToWorldPoint(pauseButton.position);

		// hit = Physics2D.Raycast(cmCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.up);
		
		// if ((pause - mouse).magnitude < 1) {
		// 	canImpulse = false;
		// 	Debug.Log("Foi");
		// } else {
		// 	canImpulse = true;
		// }

		clickOnUI = EventSystem.current.IsPointerOverGameObject() | IsPointerOverUIObject();

		// if (hit.collider && hit.collider.name == "Pause") {
		// 	canImpulse = false;
		// } else {
		// 	canImpulse = true;
		// }

		canImpulse = !clickOnUI & GameManager.instance.inGame;

		if (Input.GetMouseButtonDown(0) && !inSlowMotion && canImpulse) {
			inSlowMotion = true;
			rBody.velocity = Vector3.zero;
			StartCoroutine("CalculateImpulse");
			Time.timeScale = 0.1f;
			arrowSprite.enabled = true;
			Invoke("SlowMotion", slowMotionDuration * Time.timeScale);
		} else if (Input.GetMouseButtonUp(0) && inSlowMotion) {
			CancelInvoke("SlowMotion");
			SlowMotion();
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

		if (impulse.magnitude > maxImpulse) {
			impulse = impulse.normalized * maxImpulse;
		}
		arrowSize = impulse;
	}
	void ImpulseStar() {

		if (impulse.magnitude > Shine) {
			impulse = impulse.normalized * Shine;
		}

		Debug.DrawLine(this.transform.position,impulse, Color.green, 0.3f);
		rBody.AddForce(impulse, ForceMode2D.Impulse);

		Shine -= impulse.magnitude;
	}


	private bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}