using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;

    private PlayerController playerController;
    private Airship airship;
    
    public void Start()
    {
        Debug.Log("Started player");
        playerController = GetComponent<PlayerController>();
        airship = GameObject.FindGameObjectWithTag("Airship").GetComponent<Airship>();
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        playerController.Move(moveVelocity);

        // Shoot input:
        if (Input.GetKeyDown(KeyCode.Space))
        {
            airship.ApplyPatch(playerController.GetLocation());
        }
    }
}
