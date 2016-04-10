using UnityEngine;
using UnityEngine.UI; // Access text elements
using UnityEngine.SceneManagement;


[RequireComponent(typeof(PointController), typeof(Spawner))]
public class GameManager : MonoBehaviour
{
    public float resetDelay = 3; // Time in secs after game ended.
    public Text scoreText, highScoreText, altitudeText;
    public GameObject gameOver;
    public GameObject startScreen;
    public GameObject gamingScreen;
    public GameObject airshipPrefab;
    public GameObject playerPrefab;
    public GameObject patchToClone, holeToClone;
    public static GameManager instance = null; // Use instance of this class.

    public GameState gameState;

    public static bool InGame() {
        return GameManager.instance != null && GameManager.instance.gameState == GameManager.GameState.Gaming;
    }

    public enum GameState{ FrontPage, Gaming, HighScoreScreen};

    private Airship currentAirShip;
    private Player currentPlayer;

    public int highScore = 1000;
    private PointController pointController;

    private Spawner spawner;
    
    public GameObject GetHoleToClone()
    {
        if (holeToClone == null)
            throw new MissingComponentException("Please add a hole to the game manager. Hole location should be 0,0.");
        return holeToClone;
    }

    public GameObject GetPatchToClone()
    {
        if (patchToClone == null)
            throw new MissingComponentException("Please add a patch to the game manager. Hole location should be 0,0.");
        return patchToClone;
    }



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
        spawner = GetComponent<Spawner>();
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
                altitudeText.text = "Health: " + Mathf.FloorToInt(currentAirShip.altitudeInMeters);
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

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Destroy");


        for (int i = 0; i < allObjects.Length; i ++)
        {
            Destroy(allObjects[i].gameObject);
        }

        ShowFrontPage();
    }

    public void StartGame()
    {
        spawner.ResetWaves();
        startScreen.SetActive(false);
        gamingScreen.SetActive(true);
        gameOver.SetActive(false);
        Time.timeScale = 1f;
        pointController.ResetPoints();
        currentAirShip = ((GameObject)Instantiate(airshipPrefab, new Vector2(0,0), Quaternion.identity)).GetComponent<Airship>();
        currentPlayer = ((GameObject)Instantiate(playerPrefab, currentAirShip.GetPlayerStartLocation(),Quaternion.identity)).GetComponent<Player>();
        gameState = GameState.Gaming;
        spawner.NextWave();
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

    public void CreateExplosionOnCurrentShip()
    {
        currentAirShip.CreateExplosion();
    }
}
