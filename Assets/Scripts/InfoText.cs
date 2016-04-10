using UnityEngine;
using UnityEngine.UI;

public class InfoText : MonoBehaviour {
    public string textToDisplay = "INFO";
    public float speed = 5f;
    public float lifetime = 2f;

    private Text TextPrefab;
    private float timeLeft;

	void Start()
    {
        timeLeft = lifetime;
        TextPrefab = GetComponentInChildren<Text>();
        TextPrefab.text = textToDisplay;
    }
	
	void Update() {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(gameObject);
            return;
        }
        if (speed > 0)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
