using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button buttonEasy;
    private Button buttonHard;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        buttonEasy = GameObject.Find("Easy Button").GetComponent<Button>();
        buttonHard = GameObject.Find("Hard Button").GetComponent<Button>();

        buttonEasy.onClick.AddListener(SetDifficultyEasy);
        buttonHard.onClick.AddListener(SetDifficultyHard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficultyEasy()
    {
        gameManager.NewGame("Easy");
    }
    
    void SetDifficultyHard()
    {
        gameManager.NewGame("Hard");
    }
}
