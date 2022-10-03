using UnityEngine;
using System.Linq;

namespace RedboonTestProject.Pathfinding
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        [SerializeField] private DotFactory _dotFactory;
        [SerializeField] private Edge[] _edges;

        private readonly WavePathFinder _wavePathFinder = new();

        private Dot _startDot;
        private Dot _endDot;
        private Dot[] _dots;

        private void Update()
        {
            CheckInput();
        }

        public void CheckInput()
        {
            if (Input.GetMouseButtonDown(0))
                SetDot();
        }

        private void SetDot()
        {
            if (_startDot == null)
            {
                _startDot = CreateDot(Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
            }
            else if (_endDot == null)
            {
                _endDot = CreateDot(Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
                SetPath();
            }
            else
            {
                Destroy(_startDot.gameObject);
                Destroy(_endDot.gameObject);

                for (int i = 0; i < _dots.Length; i++)
                    Destroy(_dots[i].gameObject);
            }
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
            Vector2[] dotPositions = _wavePathFinder.GetPath(_startDot.transform.position, _endDot.transform.position, _edges.AsEnumerable()).ToArray();
            _dots = new Dot[dotPositions.Length];

            for (int i = 0; i < _dots.Length; i++)
                _dots[i] = CreateDot(dotPositions[i], false);

            _startDot.gameObject.SetActive(true);
            _endDot.gameObject.SetActive(true);
        }
    }
}
