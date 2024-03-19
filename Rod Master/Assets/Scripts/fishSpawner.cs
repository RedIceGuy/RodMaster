using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject puffer;

    [SerializeField]
    private GameObject clownFish;

    [SerializeField]
    private GameObject sardine;

    [SerializeField]
    private float spawntimer = 3.5f;

    public Transform spawnerTransform;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnFish(spawntimer, puffer));
        StartCoroutine(spawnFish(spawntimer-1f, clownFish));
        StartCoroutine(spawnFish(spawntimer-2.5f, sardine));
    }

    private IEnumerator spawnFish(float interval, GameObject fish)
    {
        yield return new WaitForSeconds(interval);
        GameObject newFish = Instantiate(fish, new Vector3(spawnerTransform.position.x,Random.Range(-3f,3),0), Quaternion.identity);
        StartCoroutine(spawnFish(interval,fish));
    }
}