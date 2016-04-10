using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    public float DPS = 100;
    private PlayerController playerController;
    private Airship airship;
    private Animator anim;

    public List<Hole> currentlyReachableHoles = new List<Hole>();

    public void Start()
    {
        Debug.Log("Started player");
        playerController = GetComponent<PlayerController>();
        airship = GameObject.FindGameObjectWithTag("Airship").GetComponent<Airship>();
        anim = GetComponent<Animator>();

        transform.SetParent(GameObject.FindGameObjectWithTag("Airship").transform);
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        playerController.Move(moveVelocity);

        if (moveInput.magnitude > 0.2f)
        {
            anim.SetBool("PlayerCrawling", true);
        }
        else
        {
            anim.SetBool("PlayerCrawling", false);
        }
        
        // Shoot input:
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(currentlyReachableHoles.Count>=1){
                currentlyReachableHoles[0].ApplyingDamage(DPS * Time.deltaTime);
                if (currentlyReachableHoles[0].isDead())
                {
                    currentlyReachableHoles.Remove(currentlyReachableHoles[0]);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Hole>() != null)
        {
            if (!currentlyReachableHoles.Contains(other.GetComponent<Hole>()))
            {
                currentlyReachableHoles.Add(other.GetComponent<Hole>());
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Hole>() != null)
        {
            if (currentlyReachableHoles.Contains(other.GetComponent<Hole>()))
            {
                currentlyReachableHoles.Remove(other.GetComponent<Hole>());
            }
        }
    }
}
