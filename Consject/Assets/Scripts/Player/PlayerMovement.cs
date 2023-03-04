using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 4f;
    public float gravity = -9.81f;

    public Vector3 drag = new Vector3(5, 1, 5);

    Vector3 velocity;

    public float speed = 0f;

    // Update is called once per frame
    void Update()
    {
        //Get Axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        speed = walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        //applique la gravité et le dash
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //drag
        velocity.x /= 1 + drag.x * Time.deltaTime;
        velocity.z /= 1 + drag.z * Time.deltaTime;
    }
}
