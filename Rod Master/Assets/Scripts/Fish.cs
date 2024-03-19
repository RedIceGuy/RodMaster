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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fish.transform.position.x >10){
            Destroy(gameObject, 1.0f);
        }
        fish.transform.position += transform.right *speed * + Time.deltaTime;
    }
}
