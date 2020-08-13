using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float maxForwardVel = 30f;
    public float forwardSpeed = 10f;
    public float breakForce = 20f;
    public float pitchModifier;
    public float rollMod;
    public float turnModifier;
    public Vector3 minVelocity = new Vector3(0, 0, 1);
    
    //// up n down rotation w/ mouse
    //private Vector2 MouseClickPos = new Vector2(0,0);
    //private Vector2 DistFromClick = new Vector2(0,0);
    //private bool IsClicked = false;

    
    
    // forward back movement with getbutton("Vertical")
    private float PitchInput;
    // Turn / rolling with getbutton ("Horizontal")
    private float TurnInput;

    private bool IsBreaking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogError("No rigidbody on ship!");    
        }
    }

    private void Update()
    {
        //// mouse clicked/unclicked position check
        //if (Input.GetButtonDown("Fire3"))
        //{
        //    MouseClickPos = Input.mousePosition;
        //    IsClicked = true;
        //}
        //else if (Input.GetButtonUp("Fire3"))
        //{
        //    IsClicked = false;
        //    MouseClickPos = Vector2.zero;
        //}

        PitchInput = Input.GetAxisRaw("Vertical");
        TurnInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump"))
        {
            IsBreaking = true;
        }
        else
        {
            IsBreaking = false;
        }
    }

    private void FixedUpdate()
    {
        rb.AddRelativeTorque(PitchInput * pitchModifier, 0, -TurnInput * rollMod);
        rb.AddRelativeForce(0, 0, -TurnInput * turnModifier );


        if (IsBreaking)
        {
            if (rb.velocity.z <= 0)
            {
                rb.velocity = minVelocity;
            }
            else
            {
                rb.AddRelativeForce(0, 0, -breakForce);
            }    
        }
        else
        {
            if (rb.velocity.z < maxForwardVel)
            {
                rb.AddRelativeForce(0, 0, forwardSpeed);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxForwardVel);
            }
        }

        Debug.DrawLine(rb.position, new Vector3 (0, 0, rb.velocity.z), Color.red, 1f, false);



    }

}
