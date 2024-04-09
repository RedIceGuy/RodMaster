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
    int[] Options = {10,-10};
    void Start()
    {
        StartCoroutine(SpawnJunk(spawntimer,junk1));
    }

    private IEnumerator SpawnJunk(float interval, GameObject junk)
    {
        yield return new WaitForSeconds(interval);
        Instantiate(
            junk, 
            new Vector3(
                Options[Random.Range(0, 2)], 
                Random.Range(-4f, 2),
                0
            ), 
            Quaternion.identity
        );
        StartCoroutine(SpawnJunk(interval,junk));
    }
}
