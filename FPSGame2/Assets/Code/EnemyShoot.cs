using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//This Script is intended for enemies who should be stationary until a player is nearby 
public class EnemyShoot : MonoBehaviour
{
    public float health = 100;
    private float healthShouldBe = 100;
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
    private AudioSource aud;



    void Start()
    {
        aud = GetComponent<AudioSource>();
        //Set up navmesh and animator
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        GameObject newRightGun = (GameObject)Instantiate(rightGun);
        newRightGun.transform.parent = rightGunBone;
        newRightGun.transform.localPosition = Vector3.zero;
        newRightGun.transform.localRotation = Quaternion.Euler(90, 0, 0);
        scaleChange = Vector3.Scale(new Vector3(3, 3, 3), newRightGun.transform.localScale);
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
        if (healthShouldBe != health)
        {
            healthShouldBe = health;
            anim.SetInteger("DamageID", 1);
            anim.SetTrigger("Damage");
        }
        if (animWait == 120 && animTrigger == true)
        {
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 2), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 1), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 3), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 2) + (transform.right * 1),  transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 1) + (transform.right * 1), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 3) + (transform.right * 1), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 2) + (transform.right * -1), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 1) + (transform.right * -1), transform.rotation);
            Instantiate(projectile, spawnPoint.position + (transform.forward * 1) + (transform.up * 3) + (transform.right * -1), transform.rotation);
            if (aud != null)
            {
                aud.Play();
            }
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
            enemy.SetDestination(player.position);
            if (animWait == 0)
            {
                anim.SetTrigger("Attack");
                animTrigger = true;
            }
        }
        else if (aggroRange >= currentRange)
        {
            enemy.SetDestination(player.position);
            anim.SetBool("Squat", false);
            anim.SetFloat("Speed", 2f);
            anim.SetBool("Aiming", false);
        }
        else
        {
            anim.SetBool("Aiming", false);
            anim.SetFloat("Speed", 0f);
        }
    }
}
