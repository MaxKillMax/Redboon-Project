using UnityEngine;
using System.Linq;

namespace RedboonTestProject.Pathfinding
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        [SerializeField] private DotFactory _dotFactory;
        [SerializeField] private GridData _gridData;

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
                TrySetDot();
        }

        private void TrySetDot()
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 intPosition = RoundVector3(mouseWorldPosition);

            if (_startDot == null)
                _startDot = CreateDot(intPosition, true);
            else if (_endDot == null)
                _endDot = CreateDot(intPosition, true);
            else
                DestroyDots();

            if (_startDot && _endDot)
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
            Debug.Log(_wavePathFinder.GetPath(_startDot.transform.position, _endDot.transform.position, _gridData.Edges.AsEnumerable()).ToArray());
            Vector2[] dotPositions = _wavePathFinder.GetPath(_startDot.transform.position, _endDot.transform.position, _gridData.Edges.AsEnumerable()).ToArray();
            _dots = new Dot[dotPositions.Length];

            for (int i = 0; i < _dots.Length; i++)
                _dots[i] = CreateDot(RoundVector3(dotPositions[i]), false);

            _startDot.gameObject.SetActive(true);
            _endDot.gameObject.SetActive(true);
        }

        private void DestroyDots()
        {
            Destroy(_startDot.gameObject);
            Destroy(_endDot.gameObject);

            for (int i = 0; i < _dots.Length; i++)
                Destroy(_dots[i].gameObject);
        }

        private Vector3 RoundVector3(Vector3 vector) => new(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
}
