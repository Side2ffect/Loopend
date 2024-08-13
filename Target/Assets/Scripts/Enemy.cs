using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Timer timer;
    Animator zombieAnim;
    NavMeshAgent agent;

    [SerializeField] Transform targetLocation;
    [SerializeField] float bonusTime = 15.0f;
    [SerializeField] float walkingRange = 20.0f;
    [SerializeField] float runningRange = 12.0f;
    [SerializeField] float attackRange = 1.0f;
    [SerializeField] float normalSpeed = 0.5f;
    [SerializeField] float chasingSpeed = 0.85f;
    [SerializeField] GameObject player;

    private bool playerInRange;
    private bool isDead = false;
    private bool canAttackPlayer = true;
    private float distance;

    private float currentInvincibleTime;
    private float startInvincibleTime = 0.0f;
    private float invincibletime = 5.0f;

    private Vector3 direction;
    private Quaternion rotation;

    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("TimerManager").GetComponent<Timer>();
        agent = GetComponent<NavMeshAgent>();
        zombieAnim = GetComponent<Animator>();
        currentInvincibleTime = startInvincibleTime;
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

        CheckCanAttackPlayer();
    }

    private void CheckPlayerInRange()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
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
            agent.SetDestination(player.transform.position);
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
            direction = (player.transform.position - transform.position).normalized;
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * agent.angularSpeed);
            agent.isStopped = true;
            zombieAnim.SetBool("AttackRange", true);
            if (canAttackPlayer)
            {
                canAttackPlayer = false;
                currentInvincibleTime = invincibletime;
                timer.ReduceTime(bonusTime);
            }
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

    private void CheckCanAttackPlayer()
    {
        if (currentInvincibleTime > 0.0f)
        {
            currentInvincibleTime -= Time.deltaTime;
        }
        else
        {
            currentInvincibleTime = 0.0f;
            canAttackPlayer = true;
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
