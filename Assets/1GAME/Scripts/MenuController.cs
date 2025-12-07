/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void Start()
    {
        _waveController.Init();
        _enemySpawner.StartWave(_waveController.GenerateNewWaveData());
    }
}
