using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Vector3 speed = new Vector3(1, 1, 1);
    public Vector3 maxspeed = new Vector3(3, 3, 3);
    public double jumpTime = 1.5;
    public bool jump = false;
    public float jumpHeight = 8;

    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("From the player script");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Jump");


        Vector3 movement = new Vector3(speed.x * inputX, 0, speed.z * inputZ);

        movement *= Time.deltaTime;
        if (movement.x > maxspeed.x) {
            movement.x = 5;
        }

        // position  -9.5 < x < 6
        //             0 < y < 9.7

        // if (-9.5 < transform.Position.x < 6) {

        // }
        // Debug.Log(transform.position.x);

        if (Input.GetKeyDown(KeyCode.Space)) {
            g = GameObject.Find("Player");
            Rigidbody r = GameObject.Find("Player").GetComponent<Rigidbody>();
            r.velocity = new Vector3 (r.velocity.x, jumpHeight, r.velocity.z);
            // .getComponent<Rigidbody>();
            // r.velocity = new Vector3(r.velocity.x, 8, r.velocity.z);
        }

        // if (inputY > 0) { jump = true; }

        // if (transform.position.y < 1) {
        //     jumpTime = 1.5;
        // }
        // else {
        //     jumpTime = 0;
        // }
        // if (jump) {
        //     movement.y = speed.y * inputY * Time.deltaTime;
        //     jumpTime -= Time.deltaTime;
        //     if (jumpTime < 0) { jump = false; }
        // }

        Debug.Log(movement.x);
        Debug.Log(movement.z);
        transform.Translate(movement, Space.World);
    }

    void OnCollisionEnter(Collision collision) 
     {
         if(collision.gameObject.name == "Wall (Left)" || collision.gameObject.name == "Wall (Right)" || collision.gameObject.name == "Wall (Top)")  // or if(gameObject.CompareTag("YourWallTag"))
         {
            Debug.Log("Collision");
            transform.Translate(Vector3.zero, Space.World);
         }
     }


}
