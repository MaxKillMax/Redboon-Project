using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace RedboonTestProject.Pathfinding
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        [SerializeField] private DotFactory _dotFactory;
        [SerializeField] private LineRenderer _line;
        [SerializeField] private EdgeHandler[] _edgeHandlers;

        private List<Edge> _edges = new List<Edge>();

        private readonly PathFinder _pathFinder = new PathFinder();

        private Vector2 _startPoint;
        private Vector2 _endPoint;

        private bool _startSetted = false;
        private bool _endSetted = false;

        private Dot[] _dots;

        private void Start()
        {
            _edges.Capacity = _edgeHandlers.Length;

            for (int i = 0; i < _edgeHandlers.Length; i++)
                _edges.Add(_edgeHandlers[i].HandableObject);
        }

        private void Update()
        {
            CheckInput();
        }

        public void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
                TrySetDot();
        }

        private void TrySetDot()
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!_startSetted)
            {
                _startSetted = true;
                _startPoint = mouseWorldPosition;
            }
            else if (!_endSetted)
            {
                _endSetted = true;
                _endPoint = mouseWorldPosition;
            }
            else
            {
                _startSetted = false;
                _endSetted = false;
                DestroyView();
            }

            if (_startSetted && _endSetted)
                SetPath();
        }

        private Dot CreateDot(Vector3 position, bool deactivate)
        {
            Dot dot = _dotFactory.CreateObject();
            dot.transform.position = position;
            dot.gameObject.SetActive(!deactivate);
            return dot;
        }

        private void SetPath()
        {
            Vector2[] path = _pathFinder.GetPath(_startPoint, _endPoint, _edges).ToArray();
            CreateView(path);
        }

        private void CreateView(Vector2[] path)
        {
            _line.positionCount = path.Length;
            _dots = new Dot[path.Length];

            for (int i = 0; i < path.Length; i++)
            {
                _line.SetPosition(i, path[i]);
                _dots[i] = CreateDot(path[i], false);
            }
        }

        private void DestroyView()
        {
            _line.positionCount = 0;

            for (int i = 0; i < _dots.Length; i++)
                Destroy(_dots[i].gameObject);
        }
    }
}
