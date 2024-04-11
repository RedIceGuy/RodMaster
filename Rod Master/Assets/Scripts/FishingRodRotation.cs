using System;
using UnityEngine;

public class FishingRodRotation : MonoBehaviour
{

    void Update()
    {
        RotateRod();
    }

    void RotateRod()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 rodPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= rodPos.x;
        mousePos.y -= rodPos.y;
        if (mousePos.y < 0) {
            mousePos.y = 0;
        }
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 105;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        Debug.Log(angle);
    }
}
