using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 15f;
    public bool turnRight, turnLeft, moveUp;
    public LayerMask carsLayer;

    private Rigidbody rb;
    private float originRotationY;
    private float rotateMultRight = 6f;
    private float rotateMultLeft = 4.5f;
    private bool isMovingFast;

    public GameObject signalLeft;
    public GameObject signalRight;

    [NonSerialized]public bool carPass;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originRotationY = transform.eulerAngles.y;

        if (turnRight)
            StartCoroutine(TurnSignals(signalRight));
        else if (turnLeft)
            StartCoroutine(TurnSignals(signalLeft));
    }

    IEnumerator TurnSignals(GameObject turnSignal)
    {
        while (!carPass)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
#if UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#else 
        if (Input.touchCount == 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, carsLayer))
        {
            string carName = hit.transform.gameObject.name;
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name==carName)
            {
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName)
            {
#endif
                speed *= 2;
                isMovingFast = true;
            }
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPass)
        {
            speed = other.GetComponent<CarController>().speed + 5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Trigger Pass"))
        {
            carPass = true;
            Collider[] colliders = GetComponents<BoxCollider>();
            foreach (Collider col in colliders)
                col.enabled = true;
        }
            

        if (other.transform.CompareTag("turnRight") && turnRight)
            rb.rotation = Quaternion.Euler(0, originRotationY + 90f, 0);
        else if (other.transform.CompareTag("turnLeft") && turnLeft)
            rb.rotation = Quaternion.Euler(0, originRotationY - 90f, 0);
        else if (other.transform.CompareTag("RemoveTrigger"))
            Destroy(gameObject);
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
