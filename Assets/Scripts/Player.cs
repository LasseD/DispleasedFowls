using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;

    private PlayerController playerController;
    private AirshipController airshipController;

    public string sortingLayer = "Front";

    public void Start()
    {
        Debug.Log("Started player");
        playerController = GetComponent<PlayerController>();
        airshipController = GameObject.FindGameObjectWithTag("Airship").GetComponent<AirshipController>();
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
            airshipController.ApplyPatch(playerController.GetLocation());
        }
    }
}
