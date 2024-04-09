using System.Collections;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] fishesToSpawn;
    // The X-axis spawn radius
    readonly int[] options = {20, -20};
    void Start() {
        foreach (GameObject fishObject in fishesToSpawn) {
            Fish fish = fishObject.GetComponent<Fish>();
            StartCoroutine(SpawnFish(fish.spawntimer, fishObject));
        }
    }

    private IEnumerator SpawnFish(float interval, GameObject fish) {
        yield return new WaitForSeconds(interval);

        GameObject fishClone = Instantiate(
            fish, 
            new Vector3(
                options[Random.Range(0,2)],
                Random.Range(
                    fish.GetComponent<Fish>().min_y,
                    fish.GetComponent<Fish>().max_y),
                0), 
            Quaternion.identity
        );
        fishClone.name = fish.name;
        StartCoroutine(SpawnFish(interval,fish));
    }
}
