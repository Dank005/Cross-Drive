using UnityEngine;

public class MovementStudyCar : MonoBehaviour
{
    public GameObject canvas, secondCar, canvasSecond;
    private bool isFirst;
    private CarController car;

    private void Start()
    {
        car = GetComponent<CarController>();
    }

    private void Update()
    {
        if (transform.position.x < 8f && !isFirst)
        {
            isFirst = true;
            GetComponent<CarController>().speed = 0;
            canvas.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (!isFirst || transform.position.x > 9f) return;

        car.speed = 15f;
        canvas.SetActive(false);
        canvasSecond.SetActive(true);
        secondCar.GetComponent<CarController>().speed = 12f;
    }
}
