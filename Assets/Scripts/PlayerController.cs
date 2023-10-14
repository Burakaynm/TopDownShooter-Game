using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Camera cam;
    public Gun gun;
    public Animator animator;

    private Quaternion targetRotation;
    Vector3 input;
    Vector3 mousePos;
    private Vector3 currentVelocityMod;

    public float rotationSpeed = 450f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    private float acceleration = 5;

    public bool _isMoving;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ControlMouse();

        Shoot();

        MoveDirection();
    }

    void AnimationShoot()
    {
        gun.Shoot();
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isShooting", true);
        }

        if(Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isShooting", false);
        }
    }

    void ControlMouse()
    {
        mousePos = Input.mousePosition;

        mousePos = cam.ScreenToWorldPoint
            (new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));

        targetRotation = Quaternion.LookRotation
            (mousePos - new Vector3(transform.position.x, 0, transform.position.z));

        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle
                (transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxis("Vertical"));

        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityMod;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        //Ask 
        //motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);

    }
    void MoveDirection()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        animator.SetFloat("x", moveDirection.x);
        animator.SetFloat("z", moveDirection.z);
    }

}
