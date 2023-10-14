using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject playerObject;
    public Animator animator;

    public float speed = 0.1f;
    public int point = 0;
    public int offset = 5;
    public int maxDistance = 40;
    float distance = 0;
    public float projectileSpeed = 10.0f;
    public float rpm;

    public bool playerCaught = false;
    public bool playerHasDistance = false;
    public bool direction = true;

    public List<Vector3> targets = new();

    Vector3 target;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        distance = Vector3.Distance(transform.position, playerObject.transform.position);



        if(distance <= offset)
        {
            animator.SetBool("isReached", true);
            return;
        }
        else
        {
            animator.SetBool("isReached", false);
        }
    }

    void CheckTarget()
    {
        playerHasDistance = distance > maxDistance;

        if(!playerCaught)
        {
            target = targets[point];

            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
        else
        {
            if(!playerHasDistance)
            {
                target = playerObject.transform.position;
            }
            else
            {
                playerCaught = false;
                target = targets[point];
            }
        }

    }

    private void FixedUpdate()
    {
        CheckTarget();

        transform.LookAt(target);

        if(!playerCaught)
        {
            if(transform.position == targets[point])
            {
                isArrived();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerCaught = true;
        }
    }

    public void isArrived()
    {
        if(direction)
        {
            if(point < targets.Count - 1)
            {

                point += 1;
            }
            else
            {
                direction = false;
            }
        }
        else
        {
            if(point > 1)
            {
                point -= 1;
            }
            else
            {
                direction = true;
            }
        }
    }






}


