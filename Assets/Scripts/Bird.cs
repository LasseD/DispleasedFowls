using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public event System.Action OnDone, OnPeckingStarted, OnBirdScared, OnHoleCreated;

    private enum State
    {
        Incoming, Pecking, Leaving
    }

    public float scareDistance = 1.5f; // Player within this distance => scare away.
    public float flightSpeed = 30f;
    public float peckingTimeSeconds = 4f;
    public int pointsForScare = 100;
    public GameObject InfoTextPrefabForScare;

    private Vector2 spawnLocation, target;
    private State currentState;
    private GameObject player;
    private float peckingTimeRemaining;
    private Animator anim;
    private SpriteRenderer renderer;
    private AudioSource audio;

    public void Start()
    {
        if (!GameManager.InGame()) return;
        currentState = State.Incoming;
        player = GameObject.FindGameObjectWithTag("Player");
        target = GameManager.instance.getAirship().GetRandomPointOnAirship();
        spawnLocation = transform.position;
        anim = GetComponent<Animator>();
        anim.SetBool("BirdPecking", false);
        renderer = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.InGame()) return;
        switch (currentState)
        {
            case State.Incoming:
                MoveTowardTarget();
                break;
            case State.Pecking:
                Peck();
                break;
            default:
                Flee();
                break;
        }
    }

    private void Flee()
    {
        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        float movedDistance = flightSpeed * Time.deltaTime;
        Vector2 location = transform.position;
        if ((spawnLocation - location).sqrMagnitude <= movedDistance * movedDistance)
        {
            // Done!
            if (OnDone != null)
                OnDone();
            Destroy(gameObject);
            return;
        }
        Vector2 direction = (spawnLocation - location).normalized;
        if (direction.x < 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }
        transform.position = location + (direction * flightSpeed * Time.deltaTime);
    }
    private void CheckScared()
    {
        Vector2 playerLocation = player.transform.position;
        Vector2 playerToBird = playerLocation - ((Vector2)transform.position);
        if (playerToBird.SqrMagnitude() <= scareDistance * scareDistance)
        {
            if (OnBirdScared != null)
                OnBirdScared();
            anim.SetBool("BirdPecking", false);
            currentState = State.Leaving;
            GameManager.instance.GetPointController().GivePoints(pointsForScare);
            Instantiate(InfoTextPrefabForScare, transform.position, Quaternion.identity);
        }
    }
    private void MoveTowardTarget()
    {
        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }

        float movedDistance = flightSpeed * Time.deltaTime;
        Vector2 location = transform.position;
        if ((target - location).sqrMagnitude <= movedDistance * movedDistance)
        {
            // Start pecking!
            currentState = State.Pecking;
            peckingTimeRemaining = peckingTimeSeconds;
            if (OnPeckingStarted != null)
                OnPeckingStarted();
            anim.SetBool("BirdPecking", true);
            return;
        }
        Vector2 direction = (target - location).normalized;
        if (direction.x<0)
        {
            renderer.flipX = true;
        } else
        {
            renderer.flipX = false;
        }


        transform.position = location + (direction * flightSpeed * Time.deltaTime);
        CheckScared();
    }
    private void Peck()
    {

        transform.SetParent(GameObject.FindGameObjectWithTag("Airship").transform);
        peckingTimeRemaining -= Time.deltaTime;
        if (peckingTimeRemaining <= 0)
        {
            Instantiate(GameManager.instance.GetHoleToClone(), transform.position, Quaternion.identity);
            currentState = State.Leaving;
            if (OnHoleCreated != null)
                OnHoleCreated();
            anim.SetBool("BirdPecking", false);
            return;
        }
        CheckScared();
    }
}
