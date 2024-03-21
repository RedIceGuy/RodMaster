using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    bool fishingMode = true;

    public int currency = 0;
    public GameObject equippedRod;

    public static GameManager Instance {
        get {
            if(_instance == null) {
                Debug.Log("GameManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            ToggleFishingMode();
            Debug.Log(fishingMode ? "Fishing!" : "Moving!");
        }
    }

    public bool GetFishingMode() {
        return fishingMode;
    }

    void ToggleFishingMode() {
        fishingMode = !fishingMode;
    }
}
