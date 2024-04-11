using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlossaryLogic : MonoBehaviour
{
    [SerializeField] GameObject[] fishesToDisplay;
    private Image fish_image;
    private TMPro.TextMeshProUGUI fish_name;
    private TMPro.TextMeshProUGUI fish_value;
    private TMPro.TextMeshProUGUI fish_caught;
    void Update()
    {
        foreach (GameObject fishObject in fishesToDisplay) {
            Fish fish = fishObject.transform.GetChild(0).gameObject.GetComponent<Fish>();
            fish_image = fishObject.transform.GetChild(1).gameObject.GetComponent<Image>();
            fish_name = fishObject.transform.GetChild(2).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            fish_value = fishObject.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            fish_caught = fishObject.transform.GetChild(4).gameObject.GetComponent<TMPro.TextMeshProUGUI>();

            fish_name.text = fish.name;
            fish_value.text = "$" + fish.value.ToString();
            fish_caught.text = "Caught: " + PlayerPrefs.GetInt(fish.name, 0).ToString();

            int quantity = PlayerPrefs.GetInt(fish.name, 0);
            if (quantity == 0) {
                if (fish_image != null)
                {
                    fish_image.color = Color.black;
                }
            }
        }
    }
}
