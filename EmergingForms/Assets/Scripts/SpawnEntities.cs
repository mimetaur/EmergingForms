using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEntities : MonoBehaviour
{
    public GameObject spawnablePrefab;
    public Transform spawnPlane;
    public GameObject goal;
    public int maxEntities = 20;
    public float minSpeed = 0.1f;
    public float maxSpeed = 10.0f;


    private readonly float throttleRate = 0.1f;
    private int numEntities = 0;
    private Bounds planeBounds;
    private bool needToSpawn;

    public int NumEntities { get => numEntities; set => numEntities = value; }

    void Start()
    {
        needToSpawn = false;
        planeBounds = spawnPlane.GetComponent<Renderer>().bounds;
        InvokeRepeating("Spawn", throttleRate, throttleRate);
    }

    public void SetSpawn()
    {
        needToSpawn = true;
    }

    private void Spawn()
    {
        if (needToSpawn == true && NumEntities < maxEntities)
        {
            var location = GameUtils.RandomPointInBounds(planeBounds);
            GameObject entity = Instantiate(spawnablePrefab, location, Quaternion.identity) as GameObject;
            var ec = entity.GetComponent<EntityController>();
            ec.spawner = this.gameObject;
            ec.goal = goal;


            var agent = entity.GetComponent<NavMeshAgent>();
            agent.speed = Random.Range(minSpeed, maxSpeed);

            NumEntities++;
            needToSpawn = false;
        }
    }
}
