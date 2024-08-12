using UnityEngine;

public class BounceObject : MonoBehaviour
{
    public float bounceHeight = 2.0f;  // Adjust the height of the bounce.
    public float bounceSpeed = 2.0f;  // Adjust the speed of the bounce.

    private Vector3 initialPosition;
    private bool goingUp = true;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Move the object up and down to simulate a bounce.
        if (goingUp)
        {
            transform.Translate(Vector3.up * bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y >= initialPosition.y + bounceHeight)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y <= initialPosition.y)
            {
                goingUp = true;
            }
        }
    }
}
