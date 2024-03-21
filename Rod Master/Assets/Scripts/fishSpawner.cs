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

    [SerializeField]
    private float spawntimer = 3.5f;

    int[] options = {8,-8};
    public Transform spawnerTransform;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFish(spawntimer, sunFish));
        StartCoroutine(spawnFish(spawntimer, puffer));
        StartCoroutine(spawnFish(spawntimer-1f, clownFish));
        StartCoroutine(spawnFish(spawntimer-2.5f, sardine));
    }

    private IEnumerator spawnFish(float interval, GameObject fish)
    {
        yield return new WaitForSeconds(interval);
        GameObject newFish = Instantiate(fish, new Vector3(options[Random.Range(0,2)],Random.Range(-4f,4),0), Quaternion.identity);
        StartCoroutine(spawnFish(interval,fish));
    }
}
