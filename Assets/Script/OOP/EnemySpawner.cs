using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("Pengaturan Waktu & Batas")]
    [SerializeField] private float intervalSpawn = 3f;
    [SerializeField] private int maksimalMusuh = 5;

    [Header("Area Spawn (Koordinat X & Y)")]
    [SerializeField] private Vector2 batasSpawnMin = new Vector2(-7f, -4f);
    [SerializeField] private Vector2 batasSpawnMax = new Vector2(7f, 4f);

    private float timer = 0f;
    private bool isSpawning = true;

    void Update()
    {
        if (!isSpawning) return;
        timer += Time.deltaTime;

        if (timer >= intervalSpawn)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy Prefab belum dimasukkan ke EnemySpawner!");
            return;
        }

        int jumlahMusuhSekarang = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (jumlahMusuhSekarang >= maksimalMusuh) return;

        float randomX = Random.Range(batasSpawnMin.x, batasSpawnMax.x);
        float randomY = Random.Range(batasSpawnMin.y, batasSpawnMax.y);
        Vector3 posisiSpawn = new Vector3(randomX, randomY, 0f);

        Instantiate(enemyPrefab, posisiSpawn, Quaternion.identity);
    }
}