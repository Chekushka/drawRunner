using System;
using TMPro;
using Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarValueSetting : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private LevelProgressing _levelProgressing;
    private bool _isLevelCompleted;

    private void Start()
    {
        levelText.text = "Level " + FindObjectOfType<LastPlayedLevelSaving>().GetSceneNumber();
        _levelProgressing = FindObjectOfType<LevelProgressing>();
        progressBar.value = _levelProgressing.CalculateProgress() / 100;
        LevelFinishing.OnLevelCompleted += SetLevelCompleted;
    }

    private void Update()
    {
        if(!_isLevelCompleted)
            progressBar.value = _levelProgressing.CalculateProgress() / 100;
        else
        {
            progressBar.value = 1;
            enabled = false;
        }
    }

    private void SetLevelCompleted() => _isLevelCompleted = true;




}
