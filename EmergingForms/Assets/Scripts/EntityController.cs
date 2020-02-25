using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityController : MonoBehaviour
{
    private const int DID_NOT_COLLIDE = 0;
    private const int DID_COLLIDE = 1;

    public NavMeshAgent agent;
    public GameObject goal;
    public GameObject spawner;
    public float resetRate = 2.0f;
    public float rethinkWanderRate = 5.0f;
    public float wanderRadius = 1000.0f;

    private PlatonicParentController platonicParentController;
    private SpawnEntities spawnEntities;
    private int entityCollision;
    private bool isWandering = false;

    public int EntityCollision { get => entityCollision; set => entityCollision = value; }

    // Start is called before the first frame update
    void Start()
    {
        spawnEntities = spawner.GetComponent<SpawnEntities>();
        platonicParentController = goal.GetComponent<PlatonicParentController>();
        EntityCollision = DID_NOT_COLLIDE;
        InvokeRepeating("ResetCollision", resetRate, resetRate);
        InvokeRepeating("Wander", 0.1f, rethinkWanderRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivePlatonics())
        {
            MoveToGoal();
            isWandering = false;
        }
        else
        {
            if (!isWandering)
            {
                Wander();
            }

        }
    }

    private void MoveToGoal()
    {
        Vector3 dest = goal.transform.position;
        agent.SetDestination(dest);
    }

    private void Wander()
    {
        if (!IsActivePlatonics())
        {
            var newPos = GameUtils.RandomNavCircle(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            isWandering = true;
        }
    }

    private bool IsActivePlatonics()
    {
        if (platonicParentController.NumActivePlatonics > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Entity"))
        {
            EntityCollision = DID_COLLIDE;
        }
    }

    void ResetCollision()
    {
        EntityCollision = DID_NOT_COLLIDE;
    }

    public void Die()
    {
        spawnEntities.NumEntities--;
    }

}
