using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : MonoBehaviour
{

    public Transform junk;
    public Transform junkChild;
    public float speed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        if (junk.transform.position.x >= 8)
        {
            junk.transform.Rotate(0f,180f,0);
        }
        junkChild.transform.Rotate(0f,0f,Random.Range(0,181));
        Destroy(gameObject, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        junk.transform.position += transform.right *speed * Time.deltaTime;
    }
}
