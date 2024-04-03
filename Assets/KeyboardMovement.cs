using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    void Update()
    {
        // Example: Move Forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime;
        }

        // Example: Move Backward
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime;
        }
        Debug.Log("updating");

        // Add more controls as needed for turning, jumping, interacting, etc.
    }
}