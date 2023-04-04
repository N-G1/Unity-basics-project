using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f, gravity = -9.81f, jmpHeight = 2f, groundDist = 0.4f;
    private float reducedHeight, originalHeight, crouchSpeed = 4f;
    public Transform groundCheck, mainView;
    public LayerMask groundMask;
    Vector3 velocity, defaultCameraPos, defaultGrHeight, newCameraPos, newGrHeight;
    bool isGrounded, isCrouching;
     void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        reducedHeight = originalHeight / 2f;
        defaultCameraPos = mainView.localPosition;
        defaultGrHeight = groundCheck.localPosition;
        newCameraPos = new Vector3(0, 0.477f, 0);
        newGrHeight = new Vector3(0, -0.588f, 0);
    }
    void Update()
    {
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
            jump();

        if (isCrouching && isGrounded)
        {
            crouching();
            
        }
        else if (!isCrouching && isGrounded)
        {
            resetHeight();
        }
           
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void crouching() 
    {
        //if (controller.height > reducedHeight)
        //{
        //    updateCharHeight(reducedHeight);
        //    groundCheck.localPosition = newGrHeight;
        //    mainView.localPosition = newCameraPos;

        //    if (controller.height - 0.05f <= reducedHeight)
        //    {
        //        controller.height = reducedHeight;
        //    }

        //}
        controller.height = reducedHeight;
        groundCheck.localPosition = newGrHeight;
       // mainView.localPosition = newCameraPos;
    }
    void resetHeight() 
    {
        //if (controller.height < originalHeight) 
        //{
        //    float lastHeight = controller.height;
        //    updateCharHeight(originalHeight);
        //    groundCheck.localPosition = defaultGrHeight;

        //    if (controller.height + 0.05f >= originalHeight)
        //    {
        //        controller.height = originalHeight;
        //    }

        //    transform.position += new Vector3(0, (controller.height - lastHeight) / 2, 0); 
        //}

        controller.height = originalHeight;
        groundCheck.localPosition = defaultGrHeight;
       // mainView.localPosition = defaultCameraPos;
    }
    void updateCharHeight(float newHeight) 
    {
        controller.height = Mathf.Lerp(controller.height, newHeight, crouchSpeed * Time.deltaTime);
    }
    void jump() 
    {
        velocity.y = Mathf.Sqrt(jmpHeight * -2f * gravity);
    }
}
