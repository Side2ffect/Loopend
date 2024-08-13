using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RideAi : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool canMove;

    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private int maxWaypoints;
    private int currentWaypoint;

    private int speed;
    [SerializeField]
    private int maxSpeed;

    private bool isPaused;

    private GameManager gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartRide()
    {
        if(!canMove)
        {
            canMove = true;
            MoveRide();
            StartCoroutine(SpeedRandomizer());
        }  
    }

    void Update()
    {
        if(canMove)
        {
            float dist = Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position); //Gets the distance between ride & current waypoint

            if (dist < 3.5)
            {
                StartCoroutine(PauseRide());       
            }
        }       
    }
    
    private void MoveRide() //Set the rides next destination
    {
        if(currentWaypoint < maxWaypoints)
        {
            agent.SetDestination(waypoints[currentWaypoint].transform.position);           
        }
        else
        {
            canMove = false;
            gameManager.EndGame();
        }
    }
    IEnumerator PauseRide()
    {
        if (!isPaused)
        {
            isPaused = true;
            
            yield return new WaitForSeconds(.75f);

            currentWaypoint++;
            MoveRide();

            isPaused = false;
        }
        
    }

    IEnumerator SpeedRandomizer() //This will control the speed of the ride. It will randomly change and choose a random speed between 0 and the maxnumber
    {
        while(canMove)
        {
            speed = Random.Range(2, maxSpeed);
            agent.speed = speed;
            yield return new WaitForSeconds(Random.Range(0, 10));
        }
    }
}
