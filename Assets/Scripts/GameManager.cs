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
    public Airship airshipPrefab;
    public static GameManager instance = null; // Use instance of this class.
    public Spawner spawner;
    private Airship cloneAirship;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this; // Only one GM.
        else if (instance != this)
            Destroy(instance); // Prevent multiple GMs when additional scenes are added.
        Setup();
    }

    public Airship getAirship()
    {
        return cloneAirship;
    }

    public void Setup()
    {
        cloneAirship = (Airship)Instantiate(airshipPrefab, transform.position, Quaternion.identity); // position => identity position at start. identity => no rotation.
        Instantiate(airshipPrefab, transform.position, Quaternion.identity); // Not accessing them.
    }

    public void CheckGameOver()
    {
        if (cloneAirship.altitudeInMeters <= 0)
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
}
