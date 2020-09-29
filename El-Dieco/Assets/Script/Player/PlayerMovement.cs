using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;

    public float Speed;

    float xAxis;

    private bool Jump = false;
    private bool Crouch = false;
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Crouch = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Controller.Move(xAxis * Time.fixedDeltaTime, Crouch, Jump);
        Jump = false;
    }
}
