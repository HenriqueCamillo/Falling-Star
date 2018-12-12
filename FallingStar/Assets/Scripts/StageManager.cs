using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public static StageManager instance;

    public GameObject[] bonus;

    public GameObject finalScreen;
    public GameObject pauseScreen;
    public GameObject endGameScreen;
    public Text stageName;
    public int stars = 0;
    private float currentTimeScale;
    private bool gamePaused;

    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        currentTimeScale = Time.timeScale;

        stageName.text = SceneManager.GetActiveScene().name;
        finalScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endGameScreen.SetActive(false);

        foreach(GameObject gm in bonus)
            gm.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseGame();
        }
    }

    public void pauseGame() {
        if (gamePaused && !GameManager.instance.inGame) {
            gamePaused = false;
            GameManager.instance.inGame = true;
            Time.timeScale = currentTimeScale;
            pauseScreen.SetActive(false);
        } else if (!gamePaused && GameManager.instance.inGame) {
            gamePaused = true;
            GameManager.instance.inGame = false;
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
    }

    private void Resume () {
		GameManager.instance.inGame = true;
        Time.timeScale = currentTimeScale;
        pauseScreen.SetActive(false);
    }

	public void Reset (){
        Time.timeScale = 1.0f;
		GameManager.instance.inGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	public void addStar(){
        bonus[stars].SetActive(true);
        stars++;
    }

    public void endStage(){
        Time.timeScale = 0f;
        GameManager.instance.inGame = false;
        GameManager.instance.SaveGame(stars);
        if (GameManager.instance.currentLevel == GameManager.instance.numberOfLevels) {
            endGameScreen.SetActive(true);
        } else {
            finalScreen.SetActive(true);
        }
    }

    public void nextStage(){
        GameManager.instance.NextStage();
    }

    public void goMenu(){
        GameManager.instance.audioSource.Stop();
		GameManager.instance.audioSource.PlayOneShot(GameManager.instance.audioClip[0]);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}