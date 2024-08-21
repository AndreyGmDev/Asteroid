using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;

public class Spawner_Script : MonoBehaviour
{
    public GameObject enemyPrefab;

    int spawnerLimit = 4;

    float positionInitialX;
    float positionFinalX;
    float positionInitialY;
    float positionFinalY;

    Vector2 spawnArea; // Posição do Spawn

    public GameObject objectToSpawn; // O prefab do objeto que será spawnado
    public LayerMask collisionLayer; // Layer Conferida
    Collider2D spawnCollider;

    bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnCollider = objectToSpawn.GetComponent<Collider2D>();
        Invoke("Spawn", 1.5f);
    }

    void Update()
    {
        if (Nave_Script.Instance != null)
        {
            if (canSpawn && Nave_Script.Instance.enemys <= 0)
            {
               canSpawn = false;
               Invoke("Spawn", 2f);
            }
        }
    }
    void SpawnerArea()
    {
        positionInitialX = transform.position.x - (transform.localScale.x / 2);
        positionFinalX = transform.position.x + (transform.localScale.x / 2);

        positionInitialY = transform.position.x - (transform.localScale.y / 2);
        positionFinalY = transform.position.x + (transform.localScale.y / 2);

        spawnArea = new(Random.Range(positionInitialX, positionFinalX), Random.Range(positionInitialY, positionFinalY));

    }

    void Spawn()
    {
        for (int i = 0; i < spawnerLimit; i++)
        {
            SpawnerArea();
            
            bool isColision = Physics2D.OverlapBox(spawnArea, spawnCollider.bounds.size, 0f, collisionLayer);
            if (isColision)
            {
                i--;
            }
            else
            {
                Instantiate(enemyPrefab, spawnArea, Quaternion.identity);
            }
        }
        if (Nave_Script.Instance != null)
        {
            Nave_Script.Instance.enemys = spawnerLimit*7;
        }
        spawnerLimit++;
        canSpawn = true;
    }
}
    

