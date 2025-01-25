using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Persona : MonoBehaviour
{
    public enum PatrolPatern { ePatrol,eRandom, eMoveTo};
    public PatrolPatern m_PatrolPatern;
    public Transform target;
    public GameObject[] waypoints;
    public float stoppingDistance = 0.1f;
    int currentWaypoint = 0;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        // Comprobar si ha llegado al destino
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        { 
            //Comprobar el tipo de movimiento
            switch (m_PatrolPatern )
            {
                case PatrolPatern.ePatrol:
                    Patrol();
                    break;
                case PatrolPatern.eRandom:
                    RandomMove();
                    break;
                case PatrolPatern.eMoveTo:
                    MoveToPoint(0);
                    break;
            }
        }
    }

    //Pasa por los puntos en orden y de uno en uno
    void Patrol()
    {
        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }
        agent.SetDestination(waypoints[currentWaypoint].transform.position);

        currentWaypoint++;
    }

    //Va a un punto aleatorio
    void RandomMove()
    {
        int maxWaypoint = waypoints.Length;
        int RandWaypoint = Random.Range(0, maxWaypoint - 1);

        agent.SetDestination(waypoints[RandWaypoint].transform.position);
    }

    //Se mueve solamente a un punto
    void MoveToPoint(int waypontPos)
    {
        agent.SetDestination(waypoints[waypontPos].transform.position);
    }
}
