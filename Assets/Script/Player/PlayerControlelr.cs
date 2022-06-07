using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlelr : MonoBehaviour
{
    public Rigidbody Body;
    public float Speed;
    public float MaxSpeed;
    public float SlownessMod;

    private float AXIS_HORIZONTAL;
    private float AXIS_VERTICAL;

    void Start()
    {
        
    }

    void Update()
    {
        GatherKeys();
        Move();
    }

    private void GatherKeys()
    {
        AXIS_HORIZONTAL = Input.GetAxisRaw("Horizontal");
        AXIS_VERTICAL = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        Vector3 Movement = ((transform.forward * AXIS_VERTICAL) + (transform.right * AXIS_HORIZONTAL)) * Speed - (Body.velocity * 2);
        Body.AddForce(Movement);
        if (Body.velocity.magnitude < 1 && AXIS_HORIZONTAL == 0 && AXIS_VERTICAL == 0) 
        {
            Body.velocity = new Vector3();
        }
        Debug.DrawLine(transform.position, transform.position + Movement, Color.black);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + Body.velocity);
        Debug.DrawLine(transform.position, transform.position + transform.forward, Color.blue);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
    }
}
