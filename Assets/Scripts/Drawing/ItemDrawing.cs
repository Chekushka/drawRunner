using System.Collections.Generic;
using Character;
using MoreMountains.Feedbacks;
using Triggers;
using UnityEngine;

namespace Drawing
{
    public class ItemDrawing : MonoBehaviour
    {
        [SerializeField] private LineRenderer linePrefab;
        [SerializeField] private Transform linesParent;
        [SerializeField] private GameObject drawField;
        [SerializeField] private LayerMask drawFieldMask;
        [SerializeField] private List<GameObject> itemsMasks;
        [SerializeField] private List<LineRenderer> itemsStandardLines;
        [SerializeField] private MMFeedbacks feedbacks;

        private CharacterMovement _movement;
        private DrawChecking _drawChecking;
        private Item _currentItem;
        private LineRenderer _currentLine;
        private List<LineRenderer> _lines;
        private List<Vector3> _fingerPositions;
        private Camera _camera;
        private CameraChanging _cameraChanging;
        private StandardLineMoving _standardLineMoving;
        private bool _isLineCreationStarted;

        private void Awake()
        {
            _movement = GetComponentInParent<CharacterMovement>();
            _cameraChanging = FindObjectOfType<CameraChanging>();
            _standardLineMoving = GetComponent<StandardLineMoving>();
            _drawChecking = GetComponent<DrawChecking>();
            _fingerPositions = new List<Vector3>();
            _lines = new List<LineRenderer>();
            _camera = Camera.main;
        }
    
        private void Update()
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
                    if(_drawChecking.CheckLine(_currentLine, itemsStandardLines[(int)_currentItem]))
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
            _standardLineMoving.MoveLineToCurrentPos(itemsStandardLines[(int)_currentItem]);
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
}
