using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringWater : MonoBehaviour
{

    private float velocity = 0;
    private float force = 0;
    //current height
    private float height = 0;
    // normal height
    private float target_height = 0;

    [SerializeField]
    private float springStiffness = 0.1f;
    [SerializeField]
    private float dampening = 0.03f;
    [SerializeField]
    private List<SpringWater> springs = new();

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        // maximum extension
        var x = height - target_height;
        var loss = -dampening * velocity;

        force = - springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y+velocity, transform.localPosition.z);
    }

    void FixedUpdate(){
        foreach(SpringWater springWaterComponent in springs){
            springWaterComponent.WaveSpringUpdate(springStiffness, dampening);
        }
    }
}
