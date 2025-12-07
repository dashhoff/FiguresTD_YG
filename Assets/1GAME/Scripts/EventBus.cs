/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System;

public static class EventBus
{
    public static event Action OnStartGame;
    public static void InvokeStartGame() => OnStartGame?.Invoke();
    
    public static event Action OnStartPause;
    public static void InvokeStartPause() => OnStartPause?.Invoke();
    
    public static event Action OnStopPause;
    public static void InvokeStopPause() => OnStopPause?.Invoke();
    
    public static event Action OnVictory;
    public static void InvokeVictory() => OnVictory?.Invoke();
    
    public static event Action OnDeath;
    public static void InvokeDeath() => OnDeath?.Invoke();
    
    public static event Action<WaveData> OnStartWave;
    public static void InvokeStartWave(WaveData waveData) => OnStartWave?.Invoke(waveData);
    
    public static event Action OnEndWave;
    public static void InvokeEndWave() => OnEndWave?.Invoke();
    
    public static event Action OnDeathEnemy;
    public static void InvokeDeathEnemy() => OnDeathEnemy?.Invoke();
    
    public static event Action OnDeathBoss;
    public static void InvokeDeathBoss() => OnDeathBoss?.Invoke();
}