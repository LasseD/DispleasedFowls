using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D myRigidBody;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public Vector2 GetLocation()
    {
        return new Vector2(myRigidBody.position.x, myRigidBody.position.y);
    }

    // Slow frame rate update.
    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }
}
