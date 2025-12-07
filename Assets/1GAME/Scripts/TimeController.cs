/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using UnityEngine;

public class TimeController : MonoBehaviour
{
    
    
    public void NormalTime()
    {
        Time.timeScale = 1;
        
        Debug.Log("Normal time");
    }
    
    public void StopTime()
    {
        Time.timeScale = 0;
        
        Debug.Log("Stop time");
    }
    
    public void OtherTime(float newTime)
    {
        Time.timeScale = newTime;
        
        Debug.Log("Time: " + newTime);
    }
}
