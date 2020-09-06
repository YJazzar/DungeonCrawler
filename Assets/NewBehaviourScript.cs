using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody rb;

    public Vector3 speed = new Vector3(50, 50, 50);

    // String constants for movement keys:
    const string forwardKey = "w";
    const string backwardKey = "s";
    const string leftKey = "a";
    const string rightKey = "d";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hellow World!11");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Jump");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, speed.z * inputZ);

        movement *= Time.deltaTime;

        transform.Translate(movement, Space.World);
    }


    //void addKeyListeners()
    //{
    //    Debug.Log(Input.GetKey());
    //    // Forward
    //    if (Input.GetKey(forwardKey))
    //    {
    //        setVelocity("z", -1 * moveSpeed);
    //    }
    //    else
    //    {
    //        this.setVelocity("z", 0);
    //    }


    //    // Backward
    //    if (Input.GetKey(backwardKey))
    //    {
    //        this.setVelocity("z", -1 * moveSpeed);
    //    }
    //    else
    //    {
    //        this.setVelocity("z", 0);
    //    }


    //    // left
    //    if (Input.GetKey(leftKey))
    //    {
    //        this.setVelocity("x", moveSpeed);
    //    }
    //    else
    //    {
    //        this.setVelocity("x", 0);
    //    }


    //    // right
    //    if (Input.GetKey(rightKey))
    //    {
    //        this.setVelocity("x", moveSpeed);
    //    }
    //    else
    //    {
    //        this.setVelocity("x", 0);
    //    }
    //}

    //void setVelocity(string axis, float newSpeed)
    //{
    //    Debug.Log("Moving" + axis + newSpeed);
    //    Vector3 oldVelocity = rb.velocity;
    //    newSpeed *= Time.deltaTime;     // Scale down the speed according to the framerate

    //    if (Equals(axis, "x")) 
    //    {
    //        rb.velocity = new Vector3(newSpeed, oldVelocity.y, oldVelocity.z);
    //    }
    //    if (Equals(axis, "y"))
    //    {
    //        rb.velocity = new Vector3(oldVelocity.x, newSpeed, oldVelocity.z);
    //    } 
    //    if (Equals(axis, "z"))
    //    {
    //        Debug.Log("Moving 22222 " + newSpeed);
    //        rb.velocity = new Vector3(oldVelocity.x, oldVelocity.y, newSpeed);
    //    }
    //}

}
