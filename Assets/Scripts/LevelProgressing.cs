using Triggers;
using UnityEngine;

public class LevelProgressing : MonoBehaviour
{
    public bool levelHasWinner;
    [SerializeField] private Transform playerCharacter;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _fullLevelDistance;

    private void Awake()
    {
        _startPosition = playerCharacter.position;
        _endPosition = FindObjectOfType<LevelFinishing>().transform.position;
        _fullLevelDistance = Vector3.Distance(_startPosition, _endPosition);
    }

    public float CalculateProgress()
    {
        var completedDistance = Vector3.Distance(_startPosition, playerCharacter.position);
        var progress = completedDistance * 100 / _fullLevelDistance;

        return progress;
    }
}
