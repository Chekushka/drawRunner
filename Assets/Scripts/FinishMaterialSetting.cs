using System.Collections.Generic;
using UnityEngine;

public class FinishMaterialSetting : MonoBehaviour
{
    [SerializeField] private List<GameObject> finishPlatforms;

    public void SetPlatform(int index)
    {
        foreach (var finishPlatform in finishPlatforms)
            finishPlatform.SetActive(false);
        
        finishPlatforms[index].SetActive(true);
    }
}
