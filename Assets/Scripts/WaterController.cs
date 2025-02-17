using UnityEngine;

public class WaterController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D waterRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWaterUp();
    }

    private void MoveWaterUp() {
        Vector2 velocity = waterRigidbody.linearVelocity;
        velocity.y = speed;
        waterRigidbody.linearVelocity = velocity;
    }
}
