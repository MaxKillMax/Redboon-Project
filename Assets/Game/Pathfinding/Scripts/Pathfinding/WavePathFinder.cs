using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class WavePathFinder : IPathFinder
    {
        private Vector2 _start;
        private Vector2 _end;

        private List<Vector3> _oldPoints;
        private List<Vector3> _lastPoints;
        private List<Vector3> _points;
        private List<int> _pointCosts;

        private int _cost;
        private List<Vector2> _path;

        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
        {
            _start = A;
            _end = C;

            InitializePointLists(edges);
            AddFirstVector(_start);

            if (!_points.Contains(_start) || !_points.Contains(_end) || _start == _end)
                return default;

            bool pathFinded = false;
            int attempts = 200;

            while (!pathFinded)
            {
                AddNewPoints();
                pathFinded = IsEndFound();

                attempts--;

                if (attempts <= 0)
                    throw new Exception("PathFinding attempts is out!");
            }

            Debug.Log(_oldPoints.Count);

            _cost = _pointCosts[GetPointIndex(_end, _oldPoints)];
            _path = new(_cost);
            _path.Add(_end);

            bool pathCreated = false;

            while (!pathCreated)
            {
                AddPath();
                pathCreated = _path[^1] == _start;

                _cost--;
            }

            _path.Reverse();
            return _path;
        }

        #region Initialization

        private void InitializePointLists(IEnumerable<Edge> edges)
        {
            Edge[] edgesArray = edges.ToArray();

            _points = new List<Vector3>(edgesArray.Length);

            for (int i = 0; i < edgesArray.Length; i++)
            {
                if (!_points.Contains(edgesArray[i].Start))
                    _points.Add(edgesArray[i].Start);

                if (!_points.Contains(edgesArray[i].End))
                    _points.Add(edgesArray[i].End);
            }

            _oldPoints = new List<Vector3>(_points.Count);
            _lastPoints = new List<Vector3>(_points.Count / 2);
            _pointCosts = new List<int>(_points.Count);
        }

        private void AddFirstVector(Vector3 vector)
        {
            _oldPoints.Add(vector);
            _lastPoints.Add(vector);
            _pointCosts.Add(0);
        }

        #endregion

        #region First cycle

        private void AddNewPoints()
        {
            int pointCost;
            List<Vector3> newPoints;

            for (int i = _lastPoints.Count - 1; i >= 0; i--)
            {
                pointCost = GetPointIndex(_lastPoints[i], _oldPoints);
                newPoints = GetNearestPoints(_lastPoints[i], IsNewPoint);

                _oldPoints.AddRange(newPoints);
                _lastPoints.AddRange(newPoints);

                for (int x = 0; x < newPoints.Count; x++)
                    _pointCosts.Add(pointCost + 1);

                _lastPoints.RemoveAt(i);
            }
        }

        private bool IsEndFound() => _oldPoints.Contains(_end);

        private bool IsNewPoint(int pointIndex) => !_oldPoints.Contains(_points[pointIndex]) && !_lastPoints.Contains(_points[pointIndex]);

        #endregion

        #region Second Cycle

        private void AddPath()
        {
            Vector3 vector = GetNearestPoints(_path[^1], CostEqualToCurrent).FirstOrDefault();

            //if (vector == default)
            //    throw new Exception($"Return path not found. Current cost is {_cost}");

            _path.Add(vector);
        }

        private bool CostEqualToCurrent(int pointIndex) => _oldPoints.Contains(_points[pointIndex]) && _pointCosts[GetPointIndex(_points[pointIndex], _oldPoints)] == _cost;

        #endregion

        private int GetPointIndex(Vector3 point, List<Vector3> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == point)
                    return i;
            }

            throw new Exception($"Point {point} not found!");
        }

        private List<Vector3> GetNearestPoints(Vector3 point, Func<int, bool> condition = null)
        {
            List<Vector3> nearestPoints = new();

            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i] == point)
                    continue;

                if (!IsPointsClose(point, _points[i]))
                    continue;

                if (condition != null && !condition.Invoke(i))
                    continue;

                nearestPoints.Add(_points[i]);
            }

            return nearestPoints;
        }

        private bool IsPointsClose(Vector2 vector1, Vector2 vector2)
{
            if (vector1.x == vector2.x)
            {
                if (vector1.y == vector2.y + 1 ||
                    vector1.y == vector2.y - 1)
                {
                    return true;
                }
            }
            else if (vector1.y == vector2.y)
            {
                if (vector1.x == vector2.x + 1 ||
                    vector1.x == vector2.x - 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
