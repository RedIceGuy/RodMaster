using System.Collections;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public enum FishType{
        Big,
        Medium,
        Small,
    }


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
            fish.transform.position += speed * Time.deltaTime * transform.right;
        }
        if (fish.transform.position.y >= gm.fishCatchHeight)
        {
            gm.currency += value;
            gm.DisplayFishCaughtText(gameObject);
            DestroyFish();
        }
        
    }

    public void DestroyFish(){
       // Hook can catch another fish
       Instantiate(Resources.Load("BLOOD"), transform.position, Quaternion.identity);
        if (hook != null)
        {
            hook.hooked = false;
        } 
        Destroy(gameObject);
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
                    quantity = PlayerPrefs.GetInt(fish.name, 0);
                    PlayerPrefs.SetInt(fish.name, quantity+1);
                    Debug.Log(quantity);
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



