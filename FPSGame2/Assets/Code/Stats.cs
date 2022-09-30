using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private float enemyCount = 0f;
    private float collectablesFound = 0f;
    public float collectablesGoal = 10f;
    public float enemyGoal = 10f;
    public float playerHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            collectablesFound++;
        }
    }
}
