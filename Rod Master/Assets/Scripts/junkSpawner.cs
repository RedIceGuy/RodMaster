using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject junk1;

    [SerializeField]
    private float spawntimer = 4f;
    // Start is called before the first frame update
    int[] options = {10,-10};
    void Start()
    {
        StartCoroutine(spawnJunk(spawntimer,junk1));
    }

    private IEnumerator spawnJunk(float interval, GameObject junk)
    {
        yield return new WaitForSeconds(interval);
        GameObject newFish = Instantiate(junk, new Vector3(options[Random.Range(0,2)],Random.Range(-5f,5),0), Quaternion.identity);
        StartCoroutine(spawnJunk(interval,junk));
    }
}
