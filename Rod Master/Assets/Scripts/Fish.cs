using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Transform fish;
    public int speed = 10;
    public float spawntimer = 3.5f;
    public int value;
    private bool is_hooked = false;
    private bool bitten = false;
    private Hook hook;
    // Start is called before the first frame update

    void Start()
    {
        if (fish.transform.position.x >= 8)
        {
            fish.transform.Rotate(0f,180f,0);
        }
        StartCoroutine(DestroyAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_hooked)
        {
            fish.transform.position += transform.right *speed * Time.deltaTime;
        }
        if (fish.transform.position.y >= 6)
        {
            // TODO should destroy and add currency for player
            Destroy(gameObject);
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
            {
                hook = other.gameObject.GetComponentInChildren<Hook>();
                bitten = hook.hooked;
                if (!bitten)
                {
                    hook.hooked = true;
                    fish.transform.Rotate(0f,0f,90f);
                    fish.transform.parent = other.transform;
                    is_hooked = true;
                }
            }
        }

    IEnumerator DestroyAfterDelay(){
        yield return new WaitForSeconds(10f);
        // is fish is not hooked destroy it
        if (!is_hooked)
        {
            Destroy(gameObject);
        }
    }
}



