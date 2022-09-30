using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationTest : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        
    }

    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.E) && (other.CompareTag("Player"))) {
          anim.Play("door_1_open");
        }

    }
}
