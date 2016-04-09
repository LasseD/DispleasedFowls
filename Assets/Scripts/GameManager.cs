﻿using UnityEngine;
using UnityEngine.UI; // Access text elements
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PointController))]
public class GameManager : MonoBehaviour
{
    public float resetDelay = 3; // Time in secs after game ended.
    public Text scoreText, highScoreText, altitudeText;
    public GameObject gameOver;
    public GameObject startScreen;
    public GameObject gamingScreen;
    public GameObject airshipPrefab;
    public GameObject playerPrefab; 
    public static GameManager instance = null; // Use instance of this class.
    public GameState gameState;

    public enum GameState{ FrontPage, Gaming, HighScoreScreen};

    public Airship currentAirShip;
    public Player currentPlayer;

    public int highScore = 1000;
    private PointController pointController;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this; // Only one GM.
        else if (instance != this)
            Destroy(instance); // Prevent multiple GMs when additional scenes are added.

        pointController = GetComponent<PointController>();
        startScreen.SetActive(true);
        gameOver.SetActive(false);
        gamingScreen.SetActive(false);
    }

    public Airship getAirship()
    {
        return currentAirShip;
    }

    public void Update()
    {
        if (gameState == GameState.Gaming)
        {
            if (altitudeText != null) {
                altitudeText.text = "Altitude: " + currentAirShip.altitudeInMeters + " ft";
            }
            if (scoreText != null) {
                scoreText.text = "Points: " + pointController.GetPoints();
            }


            CheckGameOver();
        }
    }

    public void CheckGameOver()
    {
        if (currentAirShip.altitudeInMeters <= 0)
        {
            gameState = GameState.HighScoreScreen;
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Death");
        Time.timeScale = 0.25f; // slowmo.
        Invoke("ShowHighScoreScreen", resetDelay * Time.timeScale); // Reset the game.
    }

    private void ShowHighScoreScreen()
    {
        if (pointController.GetPoints() > highScore)
        {
            highScoreText.text = "NEW HIGHSCORE!!!\n";
            highScoreText.text += pointController.GetPoints().ToString();
            highScore = pointController.GetPoints();
        }
        else
        {
            highScoreText.text = pointController.GetPoints().ToString();
        }

        gameOver.SetActive(true);
        gamingScreen.SetActive(false);
        if (currentAirShip != null)
        {
            Destroy(currentAirShip.gameObject);
        }
        if (currentPlayer != null)
        {
            Destroy(currentPlayer.gameObject);
        }

        ShowFrontPage();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gamingScreen.SetActive(true);
        gameOver.SetActive(false);
        pointController.ResetPoints();
        currentAirShip = ((GameObject)Instantiate(airshipPrefab, new Vector2(0,0), Quaternion.identity)).GetComponent<Airship>();
        currentPlayer = ((GameObject)Instantiate(playerPrefab, currentAirShip.GetPlayerStartLocation(),Quaternion.identity)).GetComponent<Player>();
        gameState = GameState.Gaming;
    }

    public void ShowFrontPage()
    {
        startScreen.SetActive(true);
    }

    public PointController GetPointController()
    {
        return pointController;
    }

    public void LoseAltitude(int height)
    {
        currentAirShip.ReduceAltitude(height);
    }
}
