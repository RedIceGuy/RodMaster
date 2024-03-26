using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkCollider : MonoBehaviour
{

    private bool is_hooked = false;
    private Hook hook;
    
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("JUNK HIT");
        }
    }
}
