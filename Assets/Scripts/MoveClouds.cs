using UnityEngine;
using System.Collections;

public class MoveClouds : MonoBehaviour {

    public Sprite[] sprites;
    public float speed;

	void Start () {
	
	}

    void Update () {
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);

        if ((transform.position).x > 20)
        {
            transform.position = new Vector2(-50, transform.position.y);
        }
	}
}
