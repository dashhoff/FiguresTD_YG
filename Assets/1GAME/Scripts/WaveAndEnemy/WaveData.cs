/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System;

[Serializable]
public class WaveData
{
    public int KamikazeEnemyCount;
    public int NormalEnemyCount;
    public int FastEnemyCount;
    public int TankEnemyCount;
    public int BossEnemyCount;
    
    public float SpawnDelay;
    
    public WaveData(int kamikaze, int normal, int fast, int tank, int boss, float spawnDelay)
    {
        KamikazeEnemyCount = kamikaze;
        NormalEnemyCount = normal;
        FastEnemyCount = fast;
        TankEnemyCount = tank;
        BossEnemyCount = boss;
        
        SpawnDelay = spawnDelay;
    }
}
