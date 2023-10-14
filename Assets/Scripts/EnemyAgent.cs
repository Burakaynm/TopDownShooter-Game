using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public float minWaitTime = 1;
    public float maxWaitTime = 5;
    public float TravelRadius = 15f;

    NavMeshAgent agent;

    private NavMeshPath path;

    private Vector3 OriginPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        path = new NavMeshPath();
        OriginPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.hasPath)
        {
            if(!IsInvoking(nameof(FindRandomPos)))
            {
                Invoke(nameof(FindRandomPos), Random.Range(minWaitTime, maxWaitTime));
            }
        }
    }

    void FindRandomPos()
    {
        Vector3 temp = OriginPos + (Random.insideUnitSphere * TravelRadius);
        temp.y = OriginPos.y;

        if(agent.CalculatePath(temp, path))
        {
            agent.SetDestination(temp);
            return;
        }

        FindRandomPos();
    }
}
