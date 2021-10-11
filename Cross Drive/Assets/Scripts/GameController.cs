using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isMainScene;
    public float timeToSpawnFrom = 1f;
    public float timeToSpawnTo = 4f;
    public GameObject[] cars;

    private int countCars;

    private void Start()
    {
        StartCoroutine(BottomCars());
        StartCoroutine(LeftCars());
        StartCoroutine(RightCars());
        StartCoroutine(UpCars());
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
