using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int count = 20;
    public Vector2 areaSize = new Vector2(17, 14);
    // defines spawn range

    void Start()
    {
        Color[] colors = { Color.red, Color.green, Color.yellow };

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                1f,
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
            );

            GameObject go = Instantiate(prefab, pos, Quaternion.identity);

            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = colors[Random.Range(0, colors.Length)];
        }
    }
}

