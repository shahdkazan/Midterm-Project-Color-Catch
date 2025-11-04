using UnityEngine;

using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int count = 20;
    public GameObject secondPrefab;
    public int secondCount = 4;
    public Vector2 areaSize = new Vector2(17, 14);
    public float minDistance = 6f; // minimum distance between objects

    private List<Vector3> spawnedPositions = new List<Vector3>();

    void Start()
    {
        Color[] colors = { Color.red, Color.green, Color.yellow };

        SpawnPrefab(prefab, count, colors);
        SpawnPrefab(secondPrefab, secondCount, null);
    }

    void SpawnPrefab(GameObject prefabToSpawn, int spawnCount, Color[] optionalColors)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos;
            int attempts = 0;

            do
            {
                pos = new Vector3(
                    Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                    1f,
                    Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
                );
                attempts++;
            } while (!IsPositionValid(pos) && attempts < 100);

            GameObject go = Instantiate(prefabToSpawn, pos, Quaternion.identity);

            if (optionalColors != null)
            {
                var renderer = go.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = optionalColors[Random.Range(0, optionalColors.Length)];
            }

            spawnedPositions.Add(pos); // store position for future checks
        }
    }
    bool IsPositionValid(Vector3 pos)
    {
        foreach (var otherPos in spawnedPositions)
        {
            if (Vector3.Distance(pos, otherPos) < minDistance)
                return false;
        }
        
        return true;
    }

}
