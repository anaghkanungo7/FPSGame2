using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//This Script is intended for enemies who should be stationary until a player is nearby 
public class EnemyShoot : MonoBehaviour
{
    public float health = 100;
    //related to movement
    public float aggroRange;
    public NavMeshAgent enemy;
    public Transform player;
    private NavMeshAgent navMeshAgent;

    //related to animation
    public Transform rightGunBone;
    private Animator anim;
    public GameObject rightGun;
    public RuntimeAnimatorController controller;
    private Vector3 scaleChange;

    //related to shooting
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireRate;
    private float currTime = 0f;
    private float animWait = 0f;
    private bool animTrigger = false;



    void Start()
    {

        //Set up navmesh and animator
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        GameObject newRightGun = (GameObject)Instantiate(rightGun);
        newRightGun.transform.parent = rightGunBone;
        newRightGun.transform.localPosition = Vector3.zero;
        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
        scaleChange = Vector3.Scale(new Vector3(1, 1, 1), newRightGun.transform.localScale);
        newRightGun.transform.localScale = scaleChange;
        anim.runtimeAnimatorController = controller;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject, 5);
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            health = 0;
        }
        if (animWait == 300 && animTrigger == true)
        {
            Instantiate(projectile, spawnPoint.position + (transform.forward * 2) + (transform.up * 4), transform.rotation);
            animWait = 0;
            animTrigger = false;
        }
        else if (animTrigger == true)
        {
            animWait++;
        }
        float currentRange = Vector3.Distance(player.position, transform.position);

        //if the player is within range, the enemy should shoot
        if (currentRange <= navMeshAgent.stoppingDistance)
        {
            anim.SetBool("Squat", false);
            anim.SetFloat("Speed", 0f);
            anim.SetBool("Aiming", true);
            if (currTime <= 0f)
            {
                anim.SetTrigger("Attack");
                animTrigger = true;
            }
            currTime = 1f;
        }
        else if (aggroRange >= currentRange)
        {
            enemy.SetDestination(player.position);
        }
        else
        {
            anim.SetBool("Aiming", false);
            anim.SetFloat("Speed", 0f);
        }

        currTime -= Time.deltaTime;
    }
}
