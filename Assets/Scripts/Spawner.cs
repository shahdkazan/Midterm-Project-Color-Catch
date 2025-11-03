using UnityEngine;



public class Spawner : MonoBehaviour
{
    public GameObject prefab;          // first prefab
    public int count = 20;             // number of first prefab
    public GameObject secondPrefab;    // second prefab
    public int secondCount = 4;        // number of second prefab
    public Vector2 areaSize = new Vector2(17, 14);

    void Start()
    {
        Color[] colors = { Color.red, Color.green, Color.yellow };

        // Spawn first prefab
        SpawnPrefab(prefab, count, colors);

        // Spawn second prefab 
        SpawnPrefab(secondPrefab, secondCount, null);
    }

   
    void SpawnPrefab(GameObject prefabToSpawn, int spawnCount, Color[] optionalColors)
    {
        float minDistance = 6f; // Minimum distance between spawned objects

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos;
            int attempts = 0;

            // Try to find a valid spawn position
            do
            {
                pos = new Vector3(
                    Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                    1f,
                    Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
                );
                attempts++;
            } while (!IsPositionValid(pos, minDistance) && attempts < 100); // 100 attempts max

            GameObject go = Instantiate(prefabToSpawn, pos, Quaternion.identity);

            // Only assign random color if colors are provided
            if (optionalColors != null)
            {
                var renderer = go.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = optionalColors[Random.Range(0, optionalColors.Length)];
            }
        }
    }

    // Checks if the spawn position is far enough from other objects
    bool IsPositionValid(Vector3 pos, float minDistance)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, minDistance);
        return hitColliders.Length == 0;
    }
}
