using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float aggroRange;
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireRate;
    private float currTime = 0f;
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float currentRange = Vector3.Distance(player.position, transform.position);
        if (aggroRange >= currentRange)
        {
            enemy.SetDestination(player.position);
        }
        if (currentRange <= navMeshAgent.stoppingDistance)
        {
            if(currTime<=0f)
            {
                Instantiate(projectile, spawnPoint.position, transform.rotation);
            }
            currTime = 1f;
        }
        currTime = currTime - Time.deltaTime;
    }
}
