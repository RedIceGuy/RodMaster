using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    bool fishingMode = true;

    public int currency;
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
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) {
            Destroy(gameObject);
        }
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
    public void UpgradeFishingRod(GameObject betterRod, int price) {
        currency -= price;
        equippedRod = betterRod;
    }
}
