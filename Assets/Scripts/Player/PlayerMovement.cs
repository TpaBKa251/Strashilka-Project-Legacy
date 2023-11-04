using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public MouseLook Cum;

    public float startspeed = 4f;
    public float speed = 4;
    public float difSpeed;
    public float koefSpeed = 1f;
    public static bool isMove = false;
    public static bool isSquat;
    
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1)
        {
            move = move.normalized;
        }
        
        controller.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        { 
            velocity.y = -2f;
        }



/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //if (speed <= startspeed * 2 && speed >= startspeed && Input.GetButton("RUN"))
        //{
        //    koefSpeed = Mathf.Lerp(koefSpeed, 2f, .03f);
        //}
        //else if (speed > startspeed && !Input.GetButton("RUN"))
        //{
        //    koefSpeed = Mathf.Lerp(koefSpeed, 1f, .03f);
        //}

        //speed = startspeed * koefSpeed;



        //if (isGrounded && Input.GetButtonDown("Squat") && !isSquat && koefSpeed >= 1f)
        //{
        //    isSquat = true;
        //    koefSpeed = .99f;
        //    StartCoroutine(SquatWalkLow());
        //}
        //else if (isGrounded && Input.GetButtonDown("Squat") && isSquat && koefSpeed <= .3f)
        //{
        //    isSquat = false;
        //    StartCoroutine(SquatWalkMore());
        //}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    private void LateUpdate()
    {
        speed = Mathf.Lerp(speed, difSpeed, 2 * Time.deltaTime);
    }

    public void SetSpeed(float value)
    {
        difSpeed = value;
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //IEnumerator SquatWalkLow()
    //{
    //    while(koefSpeed >.33f)
    //    {
    //        koefSpeed -= 0.03f;
    //        yield return new WaitForSeconds(.02f);
    //    }
    //    koefSpeed = .3f;
    //}
    //IEnumerator SquatWalkMore()
    //{
    //    while (koefSpeed < 0.97f)
    //    {
    //        koefSpeed += 0.03f;
    //        yield return new WaitForSeconds(.02f);
    //    }
    //    koefSpeed = 1f;
    //}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
