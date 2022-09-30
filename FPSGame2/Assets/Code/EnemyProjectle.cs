using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectle : MonoBehaviour
{
    public float speed;

    private ClassPlayerController player;

    // Start is called before the first frame update
    void Start()
    {
       player = FindObjectOfType<ClassPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other) {
         if (other.CompareTag("Player")) {
                player.health -= 10;
                print("player hit"); 
            } 
    }
}
