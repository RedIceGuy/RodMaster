using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryLogic : MonoBehaviour
{
    [SerializeField] GameObject[] fishesToDisplay;
    private Image fish_image;
    private TMPro.TextMeshProUGUI fish_caught;
    void Update()
    {
        foreach (GameObject fishObject in fishesToDisplay) {
            GameObject fish = fishObject.transform.GetChild(0).gameObject;
            int quantity = PlayerPrefs.GetInt(fish.name, 0);
            fish_caught = fishObject.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            fish_caught.text = "Caught: " + PlayerPrefs.GetInt(fish.name, 0).ToString();
            if (quantity == 0) {
                fish_image = fish.GetComponent<Image>();
                if (fish_image != null)
                {
                    fish_image.color = Color.black;
                }
            }
            Debug.Log(fish.name);
            Debug.Log(quantity);
        }
    }
}
