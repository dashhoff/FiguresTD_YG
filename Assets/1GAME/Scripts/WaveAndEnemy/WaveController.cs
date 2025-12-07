/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using NaughtyAttributes;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Main")]
    public EnemyCoefficient EnemyCoefficient;
    public WaveData CurrentWaveData;
    public WaveData LastWaveData;
    public int CurrentWave;
    
    [Header("WavePower")]
    public float WavePower = 1f;
    public float WavePowerAddition = 1.01f;
    
    [Header("EnemyCount")]
    public int EnemyCount;
    public int CurrentEnemyCount;

    private void OnEnable()
    {
        EventBus.OnDeathEnemy += SubEnemyCount;
    }

    private void OnDisable()
    {
        EventBus.OnDeathEnemy -= SubEnemyCount;
    }

    public void Init()
    {
        CurrentWave = 1;

        CurrentWaveData = GenerateNewWaveData();
        
        Debug.Log("WaveController: Init");
    }

    public void SubEnemyCount()
    {
        CurrentEnemyCount--;

        if (CurrentEnemyCount == 0)
        {        
            Debug.Log("WaveController: WaveEndet");
            
            EventBus.InvokeEndWave();
        }
    }
    
    public WaveData GenerateNewWaveData()
    {
        float newSpawnDelay = LastWaveData.SpawnDelay * 0.99f;
        
        int kamikaze = Mathf.RoundToInt(LastWaveData.KamikazeEnemyCount * EnemyCoefficient.Kamikaze * WavePower);
        int normal = Mathf.RoundToInt(LastWaveData.NormalEnemyCount * EnemyCoefficient.Normal * WavePower);
        int fast   = Mathf.RoundToInt(LastWaveData.FastEnemyCount * EnemyCoefficient.Fast * WavePower);
        int tank   = Mathf.RoundToInt(LastWaveData.TankEnemyCount * EnemyCoefficient.Tank * WavePower);
        int boss   = Mathf.RoundToInt(LastWaveData.BossEnemyCount * EnemyCoefficient.Boss * WavePower);

        if (CurrentWave % 10 == 0)
        {
            kamikaze++;
            normal++;
            fast++;
            tank++;
        }
        
        if (CurrentWave % 25 == 0)
            boss++;
        
        EnemyCount = kamikaze + normal + fast + tank + boss;
        
        return new WaveData(kamikaze, normal, fast, tank, boss, newSpawnDelay);
    }
    
    [Button]
    public void NextWave()
    {
        LastWaveData = CurrentWaveData;
        
        CurrentWave++;
        WavePower *= WavePowerAddition;
        
        CurrentWaveData = GenerateNewWaveData();
       
        EventBus.InvokeStartWave(CurrentWaveData);
        
        Debug.Log("WaveController: NextWave");
    }
}
