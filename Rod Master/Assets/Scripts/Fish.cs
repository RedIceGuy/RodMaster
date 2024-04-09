using System.Collections;
using UnityEngine;
using RodMaster.Enums;
using System.Linq;

public class Fish : MonoBehaviour
{
    public Transform fish;
    public FishType fishType;
    public int speed = 10;
    public float spawntimer;
    public int value;
    public bool is_hooked = false;
    private bool bitten = false;
    private int quantity;
    private Hook hook;
    public float min_y = -4f;
    public float max_y = 1.5f;
    GameManager gm;
    AudioManager _audioManager;

    private void Awake() {
        gm = GameManager.Instance;
        _audioManager = AudioManager.Instance;
    }

    void Start()
    {
        if (fish.transform.position.x >= 8)
        {
            fish.transform.Rotate(0f,180f,0);
        }
        StartCoroutine(DestroyAfterDelay());
    }

    void Update()
    {
        if (!is_hooked)
        {
            fish.transform.position += speed * Time.deltaTime * transform.right;
        }
    }

    public void DestroyFish(){
       Instantiate(Resources.Load("MONEY"), transform.position, Quaternion.identity);
        // Hook can catch another fish
        if (hook != null)
        {
            hook.hooked = false;
        } 
        Destroy(gameObject);
    }

    public void EscapeFish(){
       Instantiate(Resources.Load("BUBBLES"), transform.position, Quaternion.identity);
        // Hook can catch another fish
        if (hook != null)
        {
            hook.hooked = false;
        } 
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            // Get the hook component
            hook = other.gameObject.GetComponentInChildren<Hook>();
            // Check if the player has a good enough fishing rod to catch a fish of this size
            // bool canCatch = gm.equippedRod.GetComponent<FishingRod>().CanCatch.Contains(fishType);
            // Fish has been hooked
            if (hook != null)
            {
                bitten = hook.hooked;
                if (!bitten)
                {
                    FishHooked(other.gameObject);
                }
            }
        }
        // Fish has been reeled back to the boat
        else if (other.gameObject.CompareTag("Boat")) {
            FishCaught();
        }
    }

    void FishHooked(GameObject other) {
        _audioManager.PlayFishHooked();
        hook.hooked = true;
        fish.transform.Rotate(0f, 0f, 90f);
        fish.transform.parent = other.transform;
        fish.transform.position = other.transform.position;
        is_hooked = true;
        quantity = PlayerPrefs.GetInt(fish.name, 0);
        PlayerPrefs.SetInt(fish.name, quantity + 1);
    }

    void FishCaught() {
        gm.currency += value;
        gm.DisplayFishCaughtText(gameObject);
        DestroyFish();
    }

    IEnumerator DestroyAfterDelay(){
        yield return new WaitForSeconds(15f);
        // If fish is not hooked, destroy it
        if (!is_hooked)
        {
            Destroy(gameObject);
        }
    }
}



