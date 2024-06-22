using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatEngine : MonoBehaviour
{
    [Header("Engine Settings")]
    public float maxEngineForce = 1000f;
    public float maxTurnForce = 300f;
    public float maxSpeed = 10f;
    public float dragInWater = 2f;
    public float angularDragInWater = 1f;
    

    private Rigidbody rb;
    private float throttleInput;
    private float turnInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Configure Rigidbody
        rb.useGravity = false;
        rb.drag = dragInWater;
        rb.angularDrag = angularDragInWater;
    }

    void Update()
    {
        // Get input
        throttleInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Calculate engine force
        Vector3 engineForce = transform.forward * throttleInput * maxEngineForce;
        rb.AddForce(engineForce);

        // Clamp the boat's speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        // Calculate turning torque
        float turnTorque = turnInput * maxTurnForce;
        rb.AddTorque(Vector3.up * turnTorque);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle collision with water or other objects
        if (collision.gameObject.CompareTag("Water"))
        {
            rb.drag = dragInWater;
            rb.angularDrag = angularDragInWater;
        }
        else
        {
            rb.drag = 0.5f;
            rb.angularDrag = 0.5f;
        }
    }
}


