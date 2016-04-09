using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Incoming, Pecking, Leaving
    }

    public float scareDistance = 1.5f; // Player within this distance => scare away.
    public Transform spawnLocation;

    private Transform target;
    private State currentState;
    private GameObject player;

    public void Start()
    {
        currentState = State.Incoming;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }
=======
public class Bird : MonoBehaviour {

    public Vector2 location;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
>>>>>>> f492cbe46b83ec391abfd3b3e1d0fbadd194622e
}
