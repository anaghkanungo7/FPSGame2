using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    EnemyFollow enemyScript;

    private void OnTriggerEnter(Collider other) {
        // other.transform.GetChild(0).;
        print("collided with something!!");

        if (other.CompareTag("Enemy")) {
                enemyScript = other.GetComponent<EnemyFollow>();
                enemyScript.health -= 10;
                print("hit"); 
        } 

    }
}
