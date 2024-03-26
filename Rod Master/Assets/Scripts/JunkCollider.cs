using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkCollider : MonoBehaviour
{

    private bool hooked_fish = false;
    
    // cheks if junk collides with fish
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Fish"))
        {
            // get collided fish
            Fish fishComponent = other.gameObject.GetComponent<Fish>();

            if (fishComponent != null)
            {   // if the fish is hooked then destroy it
                hooked_fish = fishComponent.is_hooked;
                if (hooked_fish)
                {
                    fishComponent.DestroyFish();
                }
            }
        }
    }
}
