using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Speed of rotation (yaw)
    public float movementSpeed = 10f;   // Speed of forward movement

    void Update()
    {
        // Handle rotation (yaw) based on left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");  // Left/Right arrow keys or A/D keys
        transform.Rotate(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);

        // Handle forward movement based on up arrow key
        float verticalInput = Input.GetAxis("Vertical");  // Up arrow or W key
        if (verticalInput > 0)
        {
            // Move the spaceship forward in the direction it's facing
            transform.Translate(Vector3.forward * verticalInput * movementSpeed * Time.deltaTime);
        }
    }
}
