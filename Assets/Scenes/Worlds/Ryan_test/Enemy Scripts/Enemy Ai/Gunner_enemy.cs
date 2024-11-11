using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gunner_enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool flashed = false;
    public float fl_time = 0;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float bullet_speed = 32f;

    float time = 0;
    public float timeBetweenShots = 0.5f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    int shots = 0;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!GetComponent<Health>().dead && !flashed) {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
                Patrolling();
            if (playerInSightRange && !playerInAttackRange)
                Chase();
            if (playerInSightRange && playerInAttackRange)
                Attack();
            time += Time.deltaTime;
        }
        if (flashed == true)
        {
            fl_time += Time.deltaTime;
            if (fl_time > 10)
            {
                flashed = false;
            }
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Vector3 forward = transform.forward;
            //Vector3 right = transform.right;

            if(time > timeBetweenShots)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * bullet_speed, ForceMode.Impulse);
                rb.AddForce(transform.up * 3f, ForceMode.Impulse);
                rb.GetComponent<DestroyAfterTIme>().destructable = true;
                rb.GetComponent<Enemy_Damage_box>().parent = gameObject;

                shots++;
                time = 0;
            }

            if (shots > 3)
            {
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
        shots = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}
