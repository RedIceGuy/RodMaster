using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Transform fish;
    public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (fish.transform.position.x >= 8)
        {
            fish.transform.Rotate(0f,180f,0);
        }
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        fish.transform.position += transform.right *speed * Time.deltaTime;
        
    }
}
