using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float TurnSpeed = 3.0f;
    public float MoveSpeed = 3.0f;
    public float JumpForce = 10.0f;
    
    float turn;
    float move;

    bool isInJump = false;
    bool isStartJump = false;
    bool isJumping = false;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        turn = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");

        //handle the initial state of jumping
        isJumping = Input.GetAxis("Jump") > 0;

        if (isJumping && isInJump == false)
        {
            isInJump = true;
            isStartJump = true;
        }

        //do my normal motion
        if (!isInJump)
        {
            if (move < 0)
            {
                turn *= -1;
            }

            //handle normal movement
            transform.Rotate(Vector3.up,
                              turn
                              * TurnSpeed
                              * Time.deltaTime
                              * 100.0f);

            transform.position = transform.position
                               + transform.forward
                               * move
                               * MoveSpeed
                               * Time.deltaTime;
        }

        //I am JUMPING!
        if (isInJump) 
        {
            //handle the jump

            //first check if i'm done jumping
            Vector3 velo = body.velocity;
            float yval = Mathf.Abs(velo.y);
            if(yval > 0)
            {
                isStartJump = false;
                isJumping = false;
                isInJump = false;
            }

            //start the jump
            if(isStartJump)
            {
                //Start the jump by applying an impulse
                body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isStartJump = false;
            }
        
        }
    }
}
