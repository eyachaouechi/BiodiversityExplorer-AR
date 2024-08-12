using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PatrollingAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndex;
    UnityEngine.Vector3 target;

 void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();

    }
    void Update()
    {
        if (UnityEngine.Vector3.Distance(transform.position, target) < 0.1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
      

    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }


    void IterateWaypointIndex()
    {
        waypointIndex++;

        if (waypointIndex == waypoints.Length)
            waypointIndex = 0;
    }


}