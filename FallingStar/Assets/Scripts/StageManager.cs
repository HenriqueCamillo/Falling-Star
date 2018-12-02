using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public static StageManager instance;

    public StarBonus[] bonus;

    //public Player player;
    public GameObject player;
    Vector3 playerStartPos;

    public int stars = 0;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        playerStartPos = player.transform.position;
    }

    // Update is called once per frame
    void Update() {

    }

	public void Reset (){
        player.transform.position = playerStartPos;
        stars = 0;
        foreach (StarBonus b in bonus)
            b.Reset();
        
    }

	public void addStar(){
        stars++;
    }
}