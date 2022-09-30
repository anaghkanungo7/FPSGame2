using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int enemyCount = 0;
    public int collectablesFound = 0;
    public int collectablesGoal = 10;
    public int enemyGoal = 10;

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
