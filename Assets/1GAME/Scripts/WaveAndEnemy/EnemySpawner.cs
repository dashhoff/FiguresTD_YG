/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private float _radius = 25f;
    [SerializeField] private float _spawnDelay = 0.3f;
    
    [Header("Prefabs")]
    [SerializeField] private Enemy _normalPrefab;
    [SerializeField] private Enemy _kamikazePrefab;
    [SerializeField] private Enemy _fastPrefab;
    [SerializeField] private Enemy _tankPrefab;
    [SerializeField] private Enemy _bossPrefab;

    [Header("Other")]
    private bool _isSpawning;
    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        EventBus.OnStartWave += StartWave;
    }

    private void OnDisable()
    {
        EventBus.OnStartWave -= StartWave;
    }
    
    public void StartWave(WaveData waveData)
    {
        _spawnCoroutine = StartCoroutine(SpawnCoRoutine(waveData));
        
        Debug.Log("EnemySpawner: StartWave");
    }
    
    private IEnumerator SpawnCoRoutine(WaveData wave)
    {
        _isSpawning = true;
        
        yield return SpawnType(_kamikazePrefab, wave.NormalEnemyCount);
        yield return SpawnType(_normalPrefab, wave.NormalEnemyCount);
        yield return SpawnType(_fastPrefab, wave.FastEnemyCount);
        yield return SpawnType(_tankPrefab, wave.TankEnemyCount);
        yield return SpawnType(_bossPrefab, wave.BossEnemyCount);

        _isSpawning = false;
        _spawnCoroutine = null;
    }

    private IEnumerator SpawnType(Enemy prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = RandomPosOnCircle();
            Enemy newEnemy = Instantiate(prefab, pos, Quaternion.identity);
            newEnemy.Init();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private Vector3 RandomPosOnCircle()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        return new Vector3(
            Mathf.Cos(angle) * _radius,
            1f,
            Mathf.Sin(angle) * _radius
        );
    }

    public void SetSpawnDelay(float delay)
    {
        _spawnDelay = delay;
    }
}
