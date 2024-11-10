using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeker_controller : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform player;
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    //public float walkPointRange;

    public float chargeCooldown;

    public float audioRange, likeRange, sightRange;

    public bool canHearPlayer, canSeeLight, canSeePlayer;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!canSeePlayer)
        {

        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
}
