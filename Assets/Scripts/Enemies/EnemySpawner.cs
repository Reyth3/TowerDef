using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemies;
    public Vector3 relativeSpawnPoint = new Vector3(1,0,0), startingDirection = new Vector3(1, 0, 0);
    public int waves = 1, enemiesPerWave = 5, enemiesIncrease = 0;
    public float enemiesDelay = 1;
    private int wave = 0;
    private SpriteRenderer sr;
    private bool isSpawning = false;
    // Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        Wave();
	}
	
    public void Wave()
    {
        if(wave < waves && !isSpawning)
            StartCoroutine(_wave());
    }

    IEnumerator _wave()
    {
        var count = enemiesPerWave + (wave * enemiesIncrease);
        isSpawning = true;
        for (int i =0; i < count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemiesDelay);
        }
        isSpawning = false;
        yield return new WaitForSeconds(3);
        Wave();
    }

    void SpawnEnemy()
    {
        var index = Random.Range(0, enemies.Length);
        var go = Instantiate(enemies[index], transform.position + relativeSpawnPoint, Quaternion.identity) as GameObject;
        var e = go.GetComponent<Enemy>();
        e.direction = startingDirection;
        e.color = sr.color;
    }
}
