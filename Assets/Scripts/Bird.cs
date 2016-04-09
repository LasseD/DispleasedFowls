using UnityEngine;
using System.Collections;

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
}
