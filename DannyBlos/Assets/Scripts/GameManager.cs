using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject rootCanvas;
    public GameObject gameOverScreen;
    public GameObject menuScreen;

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    //Time Section
    public TMP_Text timeText;
    public float TimeLeft;
    public bool TimerOn = false;

    //Coin Section
    public TMP_Text coinsScore;

    //Health Section
    public int numOfHearts;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(rootCanvas);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {        
        Application.targetFrameRate = 60;
        NewGame();
        coinsScore.text = coins.ToString();
        MenuScreen();
        Time.timeScale = 0f;
    }

    public void MenuScreen() 
    {
        menuScreen.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        menuScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        Time.timeScale = 1f;
        timeText.text = TimeLeft.ToString();
        TimerOn = true;
        coins = 0;
        lives = 3;
        TimeLeft = 400;
        CheckLife();
        LoadLevel(1, 1);
        
    }

    public void GameOver()
    {
        TimerOn = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void LoadLevel(int world, int stage)
    {
          this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        UpdateLife();

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    public void AddCoin()
    {
        coins++;
		    AudioManager.PlaySound(AudioManager.main.coin, 1);

        if (coins == 100)
        {
            coins = 0;
            AddLife();
        }
        coinsScore.text = coins.ToString();
    }

    public void AddLife()
    {
        lives++;
    }

    public void UpdateLife(){

        lives-=1;

        if(lives > numOfHearts){
            lives = numOfHearts;
        }
        
        CheckLife();
    }
    
    public void CheckLife(){
        for ( int i = 0 ; i < numOfHearts; i++){

            if(i < lives){
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            } else {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerOn){
            if(TimeLeft > 0){
                TimeLeft -= Time.deltaTime;
                string[] temp_text= TimeLeft.ToString().Split('.');
                timeText.text = temp_text[0];
                Debug.Log(TimeLeft);
            } else {
                GameOver();
                TimerOn = false;
            }
        } 
    }
}
