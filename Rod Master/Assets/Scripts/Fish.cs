using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Transform fish;
    public int speed = 10;
    public float spawntimer;
    public int value;
    public bool is_hooked = false;
    private bool bitten = false;
    private Hook hook;
    public float min_y = -4f;
    public float max_y = 1.5f;
    GameManager gm;

    private void Awake() {
        gm = GameManager.Instance;
    }

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
        if (fish.transform.position.y >= gm.fishCatchHeight)
        {
            gm.currency += value;
            // TODO should destroy and add currency for player
            DestroyFish();
        }
        
    }

    public void DestroyFish(){
       Destroy(gameObject);
       // Hook can catch another fish
            if (hook != null)
            {
                hook.hooked = false;
            } 
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            hook = other.gameObject.GetComponentInChildren<Hook>();
            if (hook != null)
            {
                bitten = hook.hooked;
                if (!bitten)
                {
                    hook.hooked = true;
                    fish.transform.Rotate(0f,0f,90f);
                    fish.transform.parent = other.transform;
                    fish.transform.position = other.transform.position;
                    is_hooked = true;
                }
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



