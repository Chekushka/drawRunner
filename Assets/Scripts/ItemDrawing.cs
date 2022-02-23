using System.Collections.Generic;
using Character;
using MoreMountains.Feedbacks;
using UnityEngine;

public class ItemDrawing : MonoBehaviour
{
    [SerializeField] private LineRenderer linePrefab;
    [SerializeField] private Transform linesParent;
    [SerializeField] private GameObject drawField;
    [SerializeField] private LayerMask drawFieldMask;
    [SerializeField] private List<GameObject> itemsMasks;
    [SerializeField] private MMFeedbacks feedbacks;
    [SerializeField] private bool isWin;

    private CharacterMovement _movement;
    private Item _currentItem;
    private LineRenderer _currentLine;
    private List<LineRenderer> _lines;
    private List<Vector3> _fingerPositions;
    private Camera _camera;
    private CameraChanging _cameraChanging;
    private bool _isLineCreationStarted;

    private void Awake()
    {
        _movement = FindObjectOfType<CharacterMovement>();
        _cameraChanging = FindObjectOfType<CameraChanging>();
        _fingerPositions = new List<Vector3>();
        _lines = new List<LineRenderer>();
        _camera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0);
        
        var ray = _camera.ScreenPointToRay(touch.position);
   
        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, drawFieldMask)) return;
        if (hit.collider == null) return;
        
        switch (touch.phase)
        {
            case TouchPhase.Began:
                CreateLine(hit.point);
                break;
            case TouchPhase.Moved:
                UpdateLine(hit.point);
                break;
            case TouchPhase.Ended:
                DisableDrawing();
                if(CheckForSimilarity())
                    _movement.StartCorrectAction(_currentItem);
                else
                    _movement.StartFailAction(_currentItem);
                break;
        }
    }

    public void EnableDrawing(Item item)
    {
        _cameraChanging.ChangeCamera(CameraType.Draw);
        drawField.transform.localScale = Vector3.zero;
        drawField.SetActive(true);
        feedbacks.PlayFeedbacks();
        itemsMasks[(int)item].SetActive(true);
        _currentItem = item;
    }

    private void CreateLine(Vector3 hitPos)
    {
        _currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, linesParent);
        _lines.Add(_currentLine);
        _fingerPositions.Clear();
        for (var i = 0; i < 2; i++)
        {
            _fingerPositions.Add(hitPos);
            _currentLine.SetPosition(i, _fingerPositions[i]);
        }

        _isLineCreationStarted = true;
    }

    private void UpdateLine(Vector3 newFingerPos)
    {
        if(!_isLineCreationStarted) return;
        if (Vector2.Distance(newFingerPos, _fingerPositions[_fingerPositions.Count - 1]) > 0.03f)
        {
            _fingerPositions.Add(newFingerPos);
            _currentLine.positionCount++;
            _currentLine.SetPosition(_currentLine.positionCount - 1, newFingerPos);
        }
    }

    private bool CheckForSimilarity()
    {
        return isWin;
    }

    private void DisableDrawing()
    {
        _isLineCreationStarted = false;
        _cameraChanging.ChangeCamera(CameraType.Main);
        drawField.SetActive(false);
        foreach (var item in itemsMasks)
            item.SetActive(false);
        foreach (var line in _lines)
            Destroy(line.gameObject);
        _lines.Clear();
    }
}
