using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    // prefabs and their count
    public GameObject prefab;
    private int count = 100;
    public GameObject secondPrefab;
    private int secondCount = 6;

    private Vector3 areaSize = new Vector3(90, 1, 90);    // Size of the spawning area
    private float minDistance = 3f;                 // Minimum distance between spawned objects
    public LayerMask Layer;                  // Layer mask to detect obstacles
    private List<Vector3> spawnedPositions = new List<Vector3>(); // Stores positions of spawned objects


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
            int attempts = 0;

            while (attempts < 100) //each object gets 100 attempts to find a valid postion and spawn to avoid infinate loops 
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                    areaSize.y,
                    Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
                );

                if (IsPositionValid(spawnPos))
                {
                    // Spawn immediately at a valid position
                    GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

                    //set a random color for spawned collectibles
                    if (optionalColors != null)
                    {
                        var renderer = obj.GetComponent<Renderer>();
                        if (renderer != null)
                            renderer.material.color = optionalColors[Random.Range(0, optionalColors.Length)];
                    }

                    spawnedPositions.Add(spawnPos); // Save position
                    break; // Stop trying once spawned
                }

                attempts++;
            }
        }
    }

    
    //checks if position is valid 
    bool IsPositionValid(Vector3 pos)
    {
        //loop to check the ditance between given positions and positions where objects are already spawned is less mindistance 
        foreach (var otherPos in spawnedPositions)
        {
            if (Vector3.Distance(pos, otherPos) < minDistance)
                return false;
        }

        // Detect all colliders within a sphere at 'pos' in obstacleMask'
        Collider[] obstacles = Physics.OverlapSphere(pos, minDistance, Layer);
        if (obstacles.Length > 0)
            return false;

        //If the position is far enough from all spawned objects and doesn’t collide with obstacles, the function returns true
        return true;
    }
   


}
