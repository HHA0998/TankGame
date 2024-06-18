using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Highscores m_HighScores;

    public TextMeshProUGUI m_MessageText;
    public TextMeshProUGUI m_TimerText;
    public TextMeshProUGUI m_GameTitle;

    public GameObject m_HighScorePanel;
    public TextMeshProUGUI m_HighScoresText;

    public Button m_NewGameButton;
    public Button m_HighScoresButton;
    public Button m_BackButton;

    public AudioSource m_Menu;
    public AudioSource m_Game;

    public GameObject[] m_Tanks;

    private float m_gameTime = 0;

    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        Gameover
    };

    private GameState m_GameState;

    public GameState State { get { return m_GameState; } }


    // Start is called before the first frame update
    private void Awake()
    {
        m_GameState = GameState.Start;
    }
    void Start()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }
        m_MessageText.text = "Get Ready!";
        OnMenu(); //This just resets the ui to default basicly
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        switch (m_GameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.Gameover:
                GameStateGameOver();
                break;
        }
    }
    private void GameStateStart()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
            /*m_GameState = GameState.Playing;

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].SetActive(true);
            }*/
        }
    }
    private void GameStatePlaying()
    {
        bool isGameOver = false;

        m_gameTime += Time.deltaTime;
        int seconds = Mathf.RoundToInt(m_gameTime);
        m_TimerText.text = string.Format("{0:D2}:{1:D2}", (seconds / 60), (seconds % 60));

        if(IsPlayerDead() == true)
        {
            //Debug.Log("You Lose! Hooray!");
            m_MessageText.text = "HAVE ANOTHER GO!";
            isGameOver = true;
        }
        else if (OneTankLeft() == true)
        {
            //Debug.Log("You Win! Darneit!");
            m_MessageText.text = "WINNER!";
            isGameOver = true;
            m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
            m_HighScores.SaveScoreToFile();
        }

        if (isGameOver == true)
        {
            m_GameState = GameState.Gameover;
            OnMenu();
        }
    }
    private void GameStateGameOver()
    {
        if (Input.GetKeyUp(KeyCode.Return) == true)
        {
            OnNewGame();
            /*m_gameTime = 0;
            m_GameState = GameState.Playing;

            for (int i = 0; i < m_Tanks.Length; i++)
            {
                m_Tanks[i].SetActive(true);
            }*/
        }
    }

    public void OnHighScores()
    {
        m_MessageText.text = "";

        m_HighScoresButton.gameObject.SetActive(false);
        m_BackButton.gameObject.SetActive(true);
        m_HighScorePanel.SetActive(true);
        m_GameTitle.gameObject.SetActive(false);

        string text = "";
        for(int i = 0; i < m_HighScores.scores.Length; i++)
        {
            int seconds = m_HighScores.scores[i];
            text += string.Format("{0:D2}:{1:D2}\n", (seconds / 60), (seconds % 60));
        }
        m_HighScoresText.text = text;
    }
    public void OnMenu() //Better Menu
    {
        m_Menu.UnPause();
        m_Game.Stop();
        m_TimerText.gameObject.SetActive(false);
        m_HighScorePanel.gameObject.SetActive(false);
        m_BackButton.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(true);
        m_HighScoresButton.gameObject.SetActive(true);
        m_GameTitle.gameObject.SetActive(true);
    }

    public void OnNewGame()
    {
        m_Menu.Pause();
        m_Game.Play();
        m_HighScorePanel.gameObject.SetActive(false);
        m_BackButton.gameObject.SetActive(false);
        m_NewGameButton.gameObject.SetActive(false);
        m_HighScoresButton.gameObject.SetActive(false);
        m_GameTitle.gameObject.SetActive(false);


        m_TimerText.gameObject.SetActive(true);
        m_MessageText.text = "";

        m_gameTime = 0;
        m_GameState = GameState.Playing;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
            m_Tanks[i].SetActive(true); // Not sure if this is how its "propper" done but does the trick : > )
        }
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player")
                {
                    return true;
                }
            }
        }
        return false;
    }
}
