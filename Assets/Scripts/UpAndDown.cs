using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour
{

    public float amplityde;
    public float speed;
    private Vector3 startPos;
    private float timer = 0;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector3(startPos.x, startPos.y + amplityde * Mathf.Sin(speed * timer),startPos.z);
    }
}
