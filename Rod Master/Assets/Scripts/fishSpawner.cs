using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject sunFish;

    [SerializeField]
    private GameObject puffer;

    [SerializeField]
    private GameObject clownFish;

    [SerializeField]
    private GameObject sardine;



    int[] options = {8,-8};
    public Transform spawnerTransform;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFish(puffer.GetComponent<Fish>().spawntimer, puffer));
        StartCoroutine(spawnFish(clownFish.GetComponent<Fish>().spawntimer, clownFish));
        StartCoroutine(spawnFish(sardine.GetComponent<Fish>().spawntimer, sardine));
        StartCoroutine(spawnFish(sardine.GetComponent<Fish>().spawntimer, sunFish));
    }

    private IEnumerator spawnFish(float interval, GameObject fish)
    {
        yield return new WaitForSeconds(interval);
        Instantiate(fish, new Vector3(options[Random.Range(0,2)],Random.Range(-4f,2.5f),0), Quaternion.identity);
        StartCoroutine(spawnFish(interval,fish));
    }
}
