using System.Collections;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    CharacterController characterController;
    PlayerScript playerScript;
    Transform playerTransform;
    Transform cameraTransform;
    GameState gameState;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = gameObject.GetComponentInParent<CharacterController>();
        playerScript = gameObject.GetComponentInParent<PlayerScript>();
        playerTransform = gameObject.GetComponentInParent<Transform>();
        cameraTransform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        // nie mam kuraw zielonego pojecia dlaczego tutaj musi byc cameraTransform.localEulerAngles.y a obrocie wektora ruchu cameraTransform.localEulerAngles.y *2
        characterController.transform.localEulerAngles = new Vector3(0, cameraTransform.localEulerAngles.y, 0);

        

        if (characterController.isGrounded)
        {
             
            moveDirection = new Vector3(Input.GetAxis("Horizontal") , 0.0f, Input.GetAxis("Vertical") );
            int forward = 0;
            int side = 0;

            if (Input.GetKey(KeyCode.W))
            {
                forward = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                forward = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                side = 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                side = -1;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }
            if(moveDirection.magnitude == 0.0)moveDirection = new Vector3(side, 0.0f, forward);
            moveDirection *= GameState.walkingSpeed;
            Quaternion rotation = Quaternion.Euler(0, cameraTransform.localEulerAngles.y*2, 0);
            moveDirection = rotation * moveDirection;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;

            }
            if ( Input.GetButton("Fire1"))
            {
                Debug.Log("Strzał!");
                StartCoroutine(playerScript.Shoot());
            }
            if (Input.GetKey(KeyCode.R) || Input.GetButton("Fire3"))
            {
                StartCoroutine(playerScript.Reload());
            }
        }


        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}