using System.Collections;
using Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastPlayedLevelSaving : MonoBehaviour
{
    private const string LastSceneIndexKey = "LastScene_Index";
    private const string LastSceneNumberKey = "LastScene_Number";
    private const string LastPlaysCountKey = "Plays_Count";
    private const string LastLoopCountKey = "Loop_Count";
    private const int DefaultSceneNumber = 1;
    private const int DefaultPlaysCountNumber = 0;
    private const int DefaultLoopCount = 1;
    private const int AfterLoopStartLevel = 1;

    private int _sceneIndex;
    private int _sceneNumber;
    private int _playsCount;
    private int _loopCount;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(LastSceneNumberKey))
        {
            _sceneNumber = DefaultSceneNumber;
            _playsCount = DefaultPlaysCountNumber;
            _loopCount = DefaultLoopCount;
            
            PlayerPrefs.SetInt(LastSceneNumberKey, _sceneNumber);
            PlayerPrefs.SetInt(LastPlaysCountKey, _playsCount);
            PlayerPrefs.SetInt(LastLoopCountKey, _loopCount);
        }
        else
        {
            _sceneNumber = PlayerPrefs.GetInt(LastSceneNumberKey);
            _playsCount = PlayerPrefs.GetInt(LastPlaysCountKey);
            _loopCount = PlayerPrefs.GetInt(LastLoopCountKey);
        }

        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(LastSceneIndexKey, _sceneIndex);
    }

    private void OnEnable() => LevelFinishing.OnLevelCompleted += OnLevelCompletedSave;
    private void OnDisable() => LevelFinishing.OnLevelCompleted -= OnLevelCompletedSave;

    public int GetSceneNumber() => _sceneNumber;
    public int GetPlaysCount()
    {
        _playsCount = PlayerPrefs.GetInt(LastPlaysCountKey);
        _playsCount++;
        PlayerPrefs.SetInt(LastPlaysCountKey, _playsCount);
        return _playsCount;
    }
    public int GetSceneIndex() => _sceneIndex;
    public int GetLoopCount() => _loopCount;

    private void OnLevelCompletedSave()
    {
        LevelFinishing.OnLevelCompleted -= OnLevelCompletedSave;
        StartCoroutine(WaitAndSave());
    }

    private IEnumerator WaitAndSave()
    {
        yield return new WaitForSeconds(0.3f);
        _sceneNumber++;

        if (_sceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            _sceneIndex = AfterLoopStartLevel;
            _loopCount++;
        }
        else
            _sceneIndex++;
        
        PlayerPrefs.SetInt(LastSceneNumberKey, _sceneNumber);
        PlayerPrefs.SetInt(LastSceneIndexKey, _sceneIndex);
        PlayerPrefs.SetInt(LastPlaysCountKey, _playsCount);
        PlayerPrefs.SetInt(LastLoopCountKey, _loopCount);
    }
}
