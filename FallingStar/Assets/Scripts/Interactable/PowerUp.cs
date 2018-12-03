using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public int index;
	public Sprite[] colors;

	SpriteRenderer spr;
	Collider2D collider;
	public PhysicsMaterial2D p_default, p_bounce;

	// Use this for initialization
	void Start () {
		spr = this.GetComponent<SpriteRenderer>();
		collider = this.GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changePowerUp(int powerUpIndex){
		if(powerUpIndex >= 0 && powerUpIndex < colors.Length){
			spr.sprite = colors[powerUpIndex];

			if (index != 1 && powerUpIndex == 1)
				collider.sharedMaterial = p_bounce;

			if (index == 1 && powerUpIndex != 1)
				collider.sharedMaterial = p_default;
			
			if (index == 3 && powerUpIndex != 3)
				this.GetComponent<Rigidbody2D>().mass = 1;
			
			if (index != 3 && powerUpIndex == 3)
				this.GetComponent<Rigidbody2D>().mass = 2;
			
			index = powerUpIndex;
		}
	}

	public bool checkPowerUp(int powerUpIndex){
		return powerUpIndex == index;
	}
}
