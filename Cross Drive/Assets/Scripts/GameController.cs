using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isMainScene;
    public float timeToSpawnFrom = 1f;
    public float timeToSpawnTo = 4f;
    public GameObject[] cars;

    private int countCars;
    private Coroutine bottomCars, leftCars, upCars, rightCars;
    private bool isLoseOnce;

    public GameObject canvas;
    public Text nowScore, topScore, coinsCount;

    private void Start()
    {
        CarController.isLose = false;
        CarController.countCars = 0;

        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());
    }

    private void Update()
    {
        if (CarController.isLose && !isLoseOnce)
        {
            StopCoroutine(bottomCars);
            StopCoroutine(leftCars);
            StopCoroutine(upCars);
            StopCoroutine(rightCars);

            nowScore.text = "<color=#FF0000>Score:</color>" + CarController.countCars;
            if (PlayerPrefs.GetInt("Score") < CarController.countCars)
            {
                PlayerPrefs.SetInt("Score", CarController.countCars);
            }
            topScore.text = "<color=#FF0000>Top:</color>" + PlayerPrefs.GetInt("Score");

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CarController.countCars);
            coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
            canvas.SetActive(true);
            isLoseOnce = true;
        }
    }

    IEnumerator BottomCars()
    {
        while(true)
        {
            SpawnCar(new Vector3(-0.7f, -0.15f, -27.5f), 180f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-83f, -0.15f, 3.2f), 270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(26.6f, -0.15f, 9.86f), 90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-8.1f, -0.15f, 58f), 0f, true);
            float timeToSpawn = Random.Range(timeToSpawnFrom, timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    void SpawnCar(Vector3 position, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0, cars.Length)], position, Quaternion.Euler(0, rotationY, 0));
        newObj.name = "Car - " + ++countCars;
        int random = isMainScene ? 1 : Random.Range(1, 4);
        if (isMainScene)
            newObj.GetComponent<CarController>().speed = 15f;
        switch (random)
        {
            case 1:
                newObj.GetComponent<CarController>().turnRight = true;
                break;

            case 2:
                newObj.GetComponent<CarController>().turnLeft = true;
                if (isMoveFromUp)
                    newObj.GetComponent<CarController>().moveUp = true;
                break;

            case 3:
                //Move forward
                break;

        }
    }
}
