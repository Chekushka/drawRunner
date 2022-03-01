using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestarting : MonoBehaviour
{
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
