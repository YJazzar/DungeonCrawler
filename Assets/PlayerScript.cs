using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Vector3 speed = new Vector3(30, 30, 30);

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


}
