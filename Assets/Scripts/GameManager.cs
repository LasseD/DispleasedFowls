using UnityEngine;
using UnityEngine.UI; // Access text elements
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float resetDelay = 3; // Time in secs after game ended.
    public Text scoreText, highScoreText, altitudeText;
    public GameObject gameOver;
    public GameObject startScreen;
    public GameObject highscoreScreen;
    public GameObject gamingScreen;
    public GameObject airshipPrefab;
    public GameObject playerPrefab;
    public GameObject patchToClone, holeToClone;
    public static GameManager instance = null; // Use instance of this class.
    public GameState gameState;

    public enum GameState{ FrontPage, Gaming, HighScoreScreen};

    private Airship currentAirShip;

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
    }

    public Airship getAirship()
    {
        return currentAirShip;
    }

    public void Update()
    {
        if (gameState == GameState.Gaming)
        {
            altitudeText.text = "Altitude: " + "dummy" + " ft";
        }
    }

    public void CheckGameOver()
    {
        if (currentAirShip.altitudeInMeters <= 0)
        {
            gameOver.SetActive(true);
            Time.timeScale = 0.25f; // slowmo.
            Invoke("Reset", resetDelay); // Reset the game.
        }
    }

    private void Reset()
    {
        Time.timeScale = 1;
        //Application.LoadLevel(Application.loadedLevel); // Load scene. Reload last loaded scene.
        SceneManager.LoadScene(0); // Load scene. Reload last loaded scene.
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        currentAirShip = ((GameObject)Instantiate(airshipPrefab, new Vector2(0,0), Quaternion.identity)).GetComponent<Airship>();
        Instantiate(playerPrefab, currentAirShip.GetPlayerStartLocation(),Quaternion.identity);
    }

    public void ShowFrontPage()
    {

    }
}
