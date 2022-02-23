using System.Collections;
using UnityEngine;

public class EnemyDrawing : MonoBehaviour
{
    [SerializeField] private float minDrawTime = 4;
    [SerializeField] private float maxDrawTime = 10;

    public float GetMinDrawTime() => minDrawTime;
    
    public float GetMaxDrawTime() => maxDrawTime;
    
    public bool GetEnemyDrawResult()
    {
        var randomValue = Random.value;
        var result = randomValue > 0;
        
        return result;
    }

    
}
