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
    public GameObject audioManager;

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    public int health;
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
    }

    public void NewGame()
    {
        gameOverScreen.gameObject.SetActive(false);

        coins = 0;

        LoadLevel(1, 1);
        
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        //NewGame();
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

        if (health > 0) {
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
    }

    public void AddLife()
    {
        lives++;
    }

    public void UpdateLife(){

        health-=1;

        if(health > numOfHearts){
            health = numOfHearts;
        }

        for ( int i = 0 ; i < numOfHearts; i++){

            if(i < health){
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            } else {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}
