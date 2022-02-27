using Tabtale.TTPlugins;
using UnityEngine;

public class ClickInitialization : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        TTPCore.Setup();
    }
}
