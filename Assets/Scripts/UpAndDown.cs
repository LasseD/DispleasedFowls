using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour
{

    public float amplityde;
    public float speed;
    private float startY;
    private float timer = 0;

    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector2(transform.position.x, startY + amplityde * Mathf.Sin(speed * timer));
    }
}
