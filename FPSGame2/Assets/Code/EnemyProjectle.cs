using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectle : MonoBehaviour
{
    private Rigidbody body;
    private float speed = 50f;
    private bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        body.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (collided != false)
        {
            Destroy(gameObject);
        }
    }

    void OnCollissionEnter(Collision other)
    {
        collided = true;
    }
}
