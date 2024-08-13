using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAllyChasing : MonoBehaviour
{
    private Timer timer;

    Animator zombieAnim;

    NavMeshAgent agent;
    [SerializeField] Transform targetLocation;
    [SerializeField] float bonusTime = 15.0f;
    [SerializeField] float walkingRange = 20.0f;
    [SerializeField] float runningRange = 12.0f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float normalSpeed = 0.5f;
    [SerializeField] float chasingSpeed = 1.0f;
    [SerializeField] GameObject target;
    private bool playerInRange;
    private bool isDead = false;
    private float distance;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("TimerManager").GetComponent<Timer>();
        agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");
        zombieAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            CheckPlayerInRange();
            if (playerInRange == false)
            {
                RegularRecon();
            }
        }
    }

    private void CheckPlayerInRange()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > walkingRange)
        {
            playerInRange = false;
            agent.speed = normalSpeed;
            zombieAnim.SetBool("WalkingRange", false);
        }
        else
        {
            playerInRange = true;
            agent.speed = chasingSpeed;
            agent.SetDestination(target.transform.position);
            Chasing();
        }
    }

    private void Chasing()
    {
        if (distance > runningRange)
        {
            zombieAnim.SetBool("RunningRange", false);
            zombieAnim.SetBool("WalkingRange", true);
        }
        else if (distance > attackRange)
        {
            agent.isStopped = false;
            zombieAnim.SetBool("AttackRange", false);
            zombieAnim.SetBool("RunningRange", true);
        }
        else
        {
            agent.isStopped = true;
            zombieAnim.SetBool("AttackRange", true);
        }
    }

    private void RegularRecon()
    {
        if (Vector3.Distance(transform.position, targetLocation.transform.position) < attackRange)
        {
            agent.isStopped = true;
            zombieAnim.SetBool("IsMoving", false);
        }
        else
        {
            agent.isStopped = false;
            zombieAnim.SetBool("IsMoving", true);
            agent.SetDestination(targetLocation.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            timer.AddTime(bonusTime);
            isDead = true;
            agent.isStopped = true;
            zombieAnim.SetBool("IsDead", true);
            Invoke(nameof(DestroyZombie), 5.0f);
            Destroy(other);
        }
    }

    private void DestroyZombie()
    {
        Destroy(gameObject);
    }
}
