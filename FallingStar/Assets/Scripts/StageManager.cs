using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public static StageManager instance;

    public GameObject[] bonus;

    public GameObject finalScreen;

    public Text stageName;

    public int stars = 0;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        stageName.text = SceneManager.GetActiveScene().name;
        finalScreen.SetActive(false);
        foreach(GameObject gm in bonus)
            gm.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

	public void Reset (){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void addStar(){
        bonus[stars].SetActive(true);
        stars++;
        
    }

    public void endStage(){
        finalScreen.SetActive(true);
    }

    public void nextStage()
    {
        GameManager.instance.NextStage();
    }

    public void goMenu(){
        GameManager.instance.currentLevel = 0;
        SceneManager.LoadScene(0);
    }
}