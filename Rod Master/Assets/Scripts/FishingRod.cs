using System;
using UnityEngine;

public class FishingRod : MonoBehaviour
{

    void Update()
    {
        RotateRod();
    }

    void RotateRod() {
        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = 5.23f;
        Vector3 rodPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= rodPos.x;
        mousePos.y -= rodPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
