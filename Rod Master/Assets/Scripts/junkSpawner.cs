using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] junkToSpawn;
    public GameObject BoatLocation;
    // Start is called before the first frame update
    float[] options = {10,-10};
    void Start()
    {
        foreach (GameObject junkObject in junkToSpawn)
        {
            Junk junk = junkObject.GetComponent<Junk>();
            StartCoroutine(SpawnJunk(junk.spawntimer,junkObject));
        };
    }

    void Update(){
        options[0] = BoatLocation.transform.position.x +20;
        options[1] = BoatLocation.transform.position.x -20;
    }

    private IEnumerator SpawnJunk(float interval, GameObject junk)
    {
        yield return new WaitForSeconds(interval);
        Instantiate(
            junk, 
            new Vector3(
                options[Random.Range(0, 2)], 
                Random.Range(-4f, 2),
                0
            ), 
            Quaternion.identity
        );
        StartCoroutine(SpawnJunk(interval,junk));
    }
}
