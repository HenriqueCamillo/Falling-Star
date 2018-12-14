using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public static StageManager instance;

    [SerializeField] GameObject[] bonus;
    [SerializeField] GameObject finalScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject endGameScreen;
	[SerializeField] GameObject gameOverScreen;
	public GameObject insufficientStars;
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
		gameOverScreen.SetActive(false);
		insufficientStars.SetActive(false);

        foreach(GameObject gm in bonus)
            gm.SetActive(false);
    }

    /// <summary>
    /// Checks if pause button is pressed. If so, calls Pause function. 
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void GameOver() {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    /// <summary>
    /// Pauses the game. If it's already paused, resumes it.
    /// </summary>
    public void PauseGame () {
        if (gamePaused && !GameManager.instance.inGame) {
            gamePaused = false;
            GameManager.instance.inGame = true;
            pauseScreen.SetActive(false);
            Time.timeScale = currentTimeScale;
        } else if (!gamePaused && GameManager.instance.inGame) {
            gamePaused = true;
            GameManager.instance.inGame = false;
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
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