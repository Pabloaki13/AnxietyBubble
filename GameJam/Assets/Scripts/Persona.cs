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
    bool pathing = false;
    NavMeshAgent agent;
    GameObject Puerta;

    // Start is called before the first frame update
    void Start()
    {
        Puerta = GameObject.Find("Puerta movil");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (pathing) {
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
        else 
        {
            //this.gameObject.SetActive(false);
            int RandWaypoint = Random.Range(0, 1000);
            Debug.Log(RandWaypoint);
            if (RandWaypoint == 0)
            {
                //this.gameObject.SetActive(true);
                pathing = true;
                //Puerta.GetComponent<Puertamovil>().OpenDoorSequence();
            }
            
        }
               
    
    }

    //Pasa por los puntos en orden y de uno en uno
    void Patrol()
    {
        if (currentWaypoint == 1)
        {
            Puerta.GetComponent<Puertamovil>().OpenDoorSequence();
        }
        if (currentWaypoint >= waypoints.Length)
        {
            Puerta.GetComponent<Puertamovil>().OpenDoorSequence();
            pathing = false;
            currentWaypoint = 0;
        }
        agent.SetDestination(waypoints[currentWaypoint].transform.position);

        currentWaypoint++;
    }

    //Va a un punto aleatorio
    void RandomMove()
    {
        int maxWaypoint = waypoints.Length;
        int RandWaypoint = Random.Range(2, maxWaypoint - 1);

        agent.SetDestination(waypoints[RandWaypoint].transform.position);
    }

    //Se mueve solamente a un punto
    void MoveToPoint(int waypontPos)
    {
        agent.SetDestination(waypoints[waypontPos].transform.position);
    }
}
