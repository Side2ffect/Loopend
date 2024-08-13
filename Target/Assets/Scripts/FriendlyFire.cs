using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyFire : MonoBehaviour
{
    private Timer timer;

    Animator allyAnimator;
    NavMeshAgent ally;

    [SerializeField] float penaltyTime;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float distanceCheck;
    private Transform targetPoint;
    private int currentWaypoint = 0;
    private bool isDead = false;
    private bool isMovable;
    private float distance;
    private float currentStayTime = 5.0f;
    private float setStaytime = 5.0f;
    private int penaltyScore = -150;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("TimerManager").GetComponent<Timer>();
        targetPoint = wayPoints[0];
        ally = GetComponent<NavMeshAgent>();
        allyAnimator = GetComponent<Animator>();
        currentStayTime = setStaytime;
    }

    void Update()
    {
        if (!isDead) 
        {
            currentStayTime -= Time.deltaTime;
            CheckMovable();
        }

        if (isMovable)
        {
            Recon();
        }
    }   
    private void CheckMovable()
    {
        if (currentStayTime < 0.0f)
        {
            currentStayTime = 0.0f;
            isMovable = true;
        }
        else
        {
            isMovable = false;
        }
    }

    private void Recon()
    {
        if (Vector3.Distance(transform.position, targetPoint.transform.position) < distanceCheck)
        {
            ally.isStopped = true; 
            targetPoint = GetNextWaypoint();
            allyAnimator.SetBool("IsMoving", false);
            currentStayTime = setStaytime;
        }
        else
        {
            ally.isStopped = false;
            allyAnimator.SetBool("IsMoving", true);
            ally.SetDestination(targetPoint.transform.position);
        }
    }

    private Transform GetNextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= wayPoints.Length)
        {
            currentWaypoint = 0;
        }

        return wayPoints[currentWaypoint];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            timer.ReduceTime(penaltyTime);
            gameManager.UpdateScore(penaltyScore);
            isDead = true;
            ally.isStopped = true;
            allyAnimator.SetBool("IsDead", true);
            Invoke(nameof(DestroyAlly), 5.0f);
            Destroy(other);
        }
    }

    private void DestroyAlly()
    {
        Destroy(gameObject);
    }
}
