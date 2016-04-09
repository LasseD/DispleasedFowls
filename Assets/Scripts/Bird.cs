using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public event System.Action OnDone;

    private enum State
    {
        Incoming, Pecking, Leaving
    }

    public float scareDistance = 1.5f; // Player within this distance => scare away.
    public float flightSpeed = 30f;

    private Vector2 spawnLocation, target;
    private State currentState;
    private GameObject player;

    public void Start()
    {
        currentState = State.Incoming;
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameManager.instance.getAirship().GetRandomPointOnAirship();
        spawnLocation = transform.position;
    }

    void Update()
    {
        switch(currentState)
        {
            case State.Incoming:
                MoveTowardTarget();
                break;

        }
    }

    private void Flee()
    {

    }
    private void MoveTowardTarget()
    {

    }
    private void Peck()
    {

    }
}
