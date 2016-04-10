using UnityEngine;
using System.Collections;

public class Patch : MonoBehaviour {

    public int size;
    public Sprite[] sizes;
    public float fadeTime = 15;
    public float fadeOverTime = 2;
    private float time;
    private Color startColor;
    public GameObject VisualPoints;

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sizes[size];
        startColor = GetComponent<SpriteRenderer>().color;
        Instantiate(VisualPoints, transform.position, Quaternion.identity);
    }

    public void Update()
    {
        time += Time.deltaTime;
        if (time>fadeTime)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, Color.clear, (time - fadeTime) / fadeOverTime);
            if (time > fadeTime + fadeOverTime + 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
