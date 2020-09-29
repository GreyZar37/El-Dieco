using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    //components
    private Rigidbody2D rb2D;
    private BoxCollider2D DisableBox;
    [SerializeField] private LayerMask Ground;
    public LayerMask Enemy;
    //variabler
    Vector3 Velocity = Vector3.zero;


    public float JumpForce;
    public Transform GroundCheck;
    public Transform CeilingCheck;

    const float GroundRadius = 0.2f;

    private bool Grounded;
    private bool InAir;
    private bool FacingRight;

    [Range(0, 1)] [SerializeField] private float CrouchSpeed;
    [Range(0, 1)] [SerializeField] private float MovementSmoothing;
    [Range(0, 1)] [SerializeField] private float MovementInAirSmoothing;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        DisableBox = GetComponent<BoxCollider2D>();

    }
    void FixedUpdate()
        {
            bool WasGrounded = Grounded;

            Grounded = false;
            InAir = true;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundRadius, Ground);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    Grounded = true;
                    InAir = false;

                }
            }

        }

        public void Move(float move, bool Crouch, bool Jump)
        {
            if (!Crouch)
            {
            DisableBox.enabled = true;
            if (Physics2D.OverlapCircle(CeilingCheck.position, GroundRadius, Ground))
                {
                    DisableBox.enabled = false;
                Crouch = true;
                }
            }
            else if (Crouch)
        {
            DisableBox.enabled = false;
           
        }

            if (Grounded )
            { 
                Vector3 Targetvelocity = new Vector2(move * 10, rb2D.velocity.y);

                rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, Targetvelocity, ref Velocity, MovementSmoothing);
            }
            else if (InAir)
            {
            Vector3 Targetvelocity = new Vector2(move * 10, rb2D.velocity.y);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, Targetvelocity, ref Velocity, MovementInAirSmoothing);
            }
            if (Grounded && Jump)
            {
                Grounded = false;
                rb2D.AddForce(new Vector2(0, JumpForce));
            }
            if(move > 0 && FacingRight)
            {
                 Flip();
            }
            else if(move < 0 && !FacingRight)
            {
                 Flip();
            }

        }
        void Flip()
        {
        FacingRight = !FacingRight;

        Vector3 TheScale = transform.localScale;

        TheScale.x *= -1;

        transform.localScale = TheScale;
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, Enemy))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
            
        }
    }

}
