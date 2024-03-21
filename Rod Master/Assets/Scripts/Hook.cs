using UnityEngine;

public class Hook : MonoBehaviour
{
    public float Hookspeed = 5f;
    public bool hooked = false;

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = Hookspeed * Time.deltaTime * new Vector2(0f, verticalInput);

        transform.Translate(movement);
    }
}
