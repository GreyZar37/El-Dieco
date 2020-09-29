using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Components
    private Rigidbody2D rb2D;

    //Variabler
    private Vector3 Velocity = Vector3.zero;
    public float Speed;
    [Range(0, 1)] [SerializeField] public float MovementSmoothing;

    //Ting til Reverse
    private bool Reverse = true;
    public Transform CheckLeft;
     public Transform CheckRight;
    public LayerMask CheckWall;

    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if(Physics2D.OverlapCircle(CheckRight.position, 0.1f, CheckWall))
        {
            Reverse = false;
        }
        else if(Physics2D.OverlapCircle(CheckLeft.position, 0.1f, CheckWall))
        {
            Reverse = true;
        }

        if (Reverse)
        {
            Vector3 Targetvelocity = new Vector2(Speed * Time.fixedDeltaTime * 10, rb2D.velocity.y);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, Targetvelocity, ref Velocity, MovementSmoothing);
        }
        else if (!Reverse)
        {
            Vector3 Targetvelocity = new Vector2(-Speed * Time.fixedDeltaTime * 10, rb2D.velocity.y);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, Targetvelocity, ref Velocity, MovementSmoothing);
        }
       
    }
}
