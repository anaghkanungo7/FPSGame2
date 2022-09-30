using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public float heightOffset = 0f;
    public AnimationCurve myCurve;
    private AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
        Vector3 pos = transform.position;
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * 3f);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, (newY * 0.1f) + heightOffset, pos.z);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            aud.Play();
            Destroy(gameObject);
        }
    }
}
