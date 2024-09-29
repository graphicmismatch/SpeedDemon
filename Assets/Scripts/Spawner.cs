using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool active;
    public GameObject[] enemies;
    [SerializeField] Vector2 spawnTimeRange;
    public bool boss;
    float timer;
    float spawnTime;
    public static int MaxCount;
    public static int spawnCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        MaxCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }
        if (timer >= spawnTime && (spawnCount<MaxCount|boss)) {
            timer = 0;
            spawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            Instantiate(enemies[Random.Range(0, enemies.Length)]).transform.position = this.transform.position;
            if (!boss) { spawnCount++; }
        }

        timer += Time.deltaTime;
    }
}
