using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    private enum State
    {
        Incoming, Pecking, Leaving
    }

    public float scareDistance = 1.5f; // Player within this distance => scare away.
    public Vector2 spawnLocation;
    public float flightSpeed = 30f;

    private Vector2 target;
    private State currentState;
    private GameObject player;

    public void Start()
    {
        currentState = State.Incoming;
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameManager.instance.getAirship().GetRandomPointOnAirship();
    }

    void Update()
    {

    }

    private void Flee()
    {

    }
    private void MoveTowardTarger()
    {

    }
    private void Peck()
    {

    }
}
