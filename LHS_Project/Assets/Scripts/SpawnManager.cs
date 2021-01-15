using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy01Prefab;
    public GameObject Enemy02Prefab;
    public GameObject Enemy03Prefab;
    public GameObject AsteroidPrefab;

    public float xMin, xMax, yPos;

    float randX;

    //Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy01", 4, 3);
        InvokeRepeating("SpawnEnemy02", 5, 5);
        InvokeRepeating("SpawnEnemy03", 6, 7);
        InvokeRepeating("SpawnAsteroid", 8, 10);
    }

    void SpawnEnemy01()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(Enemy01Prefab, new Vector2(randX, yPos), Quaternion.identity);
    }
    void SpawnEnemy02()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(Enemy02Prefab, new Vector2(randX, yPos), Quaternion.identity);
    }
    void SpawnEnemy03()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(Enemy03Prefab, new Vector2(randX, yPos), Quaternion.identity);
    }
    void SpawnAsteroid()
    {
        randX = Random.Range(xMin, xMax);
        Instantiate(AsteroidPrefab, new Vector2(randX, yPos), Quaternion.identity);
    }
}
