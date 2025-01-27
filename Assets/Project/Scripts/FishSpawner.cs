using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    public Fish[] fishPrefabs; // Array de prefabs de peces
    public float minSpawnInterval = 1f; // Intervalo mínimo entre spawns
    public float maxSpawnInterval = 3f; // Intervalo máximo entre spawns
    public float spawnHeightRange = 5f; // Rango de altura para spawnear peces
    public float spawnXOffset = 10f; // Distancia desde el centro para spawnear peces
    public Orca orcaPrefab;
    public Fish pezGPrefab;
    
    private List<Fish> fishList = new List<Fish>();
    
    public void StartSpawning()
    {
        StartCoroutine(WaitAndStart());
    }

    IEnumerator WaitAndStart()
    {
        yield return new WaitUntil(() => GameManager.Instance.gameRunning);
        ScheduleNextSpawn();
        ScheduleOrca();
    }

    private void ScheduleOrca()
    {
        Invoke(nameof(SpawnOrca), GameManager.Instance.roundDuration / 4);
    }

    private void SpawnOrca()
    {
        float xPosition = spawnXOffset;

        // Generar una altura aleatoria
        float yPosition = Random.Range(-spawnHeightRange, spawnHeightRange);

        // Instanciar el pez
        var spawnPosition = new Vector2(xPosition, yPosition);
        var f = Instantiate(orcaPrefab);
        f.Initialize(spawnPosition, xPosition > 0 ? Vector2.left : Vector2.right );
        if (xPosition > 0)
        {
            f.transform.localScale = new Vector3(-f.transform.localScale.x, f.transform.localScale.y, f.transform.localScale.z);
        }
        fishList.Add(f);
    }


    private void SpawnFish()
    {
        var fishPrefab = GameManager.Instance.globalRound switch
        {
            3 => orcaPrefab,
            2 => pezGPrefab,
            _ => fishPrefabs[Random.Range(0, fishPrefabs.Length)]
        };
        
        // Decidir el lado de la pantalla (izquierda o derecha)
        float xPosition = Random.value > 0.5f ? spawnXOffset : -spawnXOffset;

        // Generar una altura aleatoria
        float yPosition = Random.Range(-spawnHeightRange, spawnHeightRange);

        // Instanciar el pez
        var spawnPosition = new Vector2(xPosition, yPosition);
        var f = Instantiate(fishPrefab);
        f.Initialize(spawnPosition, xPosition > 0 ? Vector2.left : Vector2.right );
        // Asegurar que los peces que aparecen desde la derecha se muevan hacia la izquierda
        if (xPosition > 0)
        {
            f.transform.localScale = new Vector3(-f.transform.localScale.x, f.transform.localScale.y, f.transform.localScale.z);
        }

        fishList.Add(f);
        if (!GameManager.Instance.gameRunning) return;
        // Programar el siguiente spawn
        ScheduleNextSpawn();
    }

    public void DeleteAllFish()
    {
        foreach (var fish in fishList.ToList())
        {
            fishList.Remove(fish);
            Destroy(fish.gameObject);
        }
    }
    private void ScheduleNextSpawn()
    {
        var maxInterval = maxSpawnInterval;
        if (GameManager.Instance.globalRound == 3)
        {
            maxInterval *= 2;
        }
        float spawnInterval = Random.Range(minSpawnInterval, maxInterval);
        Invoke(nameof(SpawnFish), spawnInterval);
    }
}