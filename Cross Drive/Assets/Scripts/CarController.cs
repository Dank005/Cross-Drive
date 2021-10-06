using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 15f;
    public bool turnRight, turnLeft, moveUp;

    private Rigidbody rb;
    private float originRotationY;
    private float rotateMultRight = 6f;
    private float rotateMultLeft = 4.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originRotationY = transform.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("turnRight") && turnRight)
        {
            RotateCar(rotateMultRight);
        }
        else if (other.transform.CompareTag("turnLeft") && turnLeft)
        {
            RotateCar(rotateMultLeft, -1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("turnRight") && turnRight)
            rb.rotation = Quaternion.Euler(0, originRotationY + 90f, 0);
        else if (other.transform.CompareTag("turnLeft") && turnLeft)
            rb.rotation = Quaternion.Euler(0, originRotationY - 90f, 0);
    }

    private void RotateCar(float speedRotate, int direction = 1)
    {
        if (direction == -1 && transform.localRotation.eulerAngles.y < originRotationY - 90f)
            return;

        if (direction == -1 && moveUp && transform.localRotation.eulerAngles.y > 250f && transform.localRotation.eulerAngles.y < 270f)
            return;

        float rotateSpeed = speed * speedRotate * direction;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotateSpeed, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
