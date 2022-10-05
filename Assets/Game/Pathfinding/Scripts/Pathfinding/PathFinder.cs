using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedboonTestProject.Pathfinding
{
    public class PathFinder : IPathFinder
    {
        private Vector2 _startPoint;
        private Vector2 _endPoint;

        private Rectangle _startRectangle;
        private Rectangle _endRectangle;

        private List<Vector2> _path;

        private List<Edge> _allEdges;
        private List<int> _markedEdgesCosts;

        private List<Rectangle> _markedRectangles;
        private List<Edge> _markedEdges;

        private List<Edge> _currentEdges;
        private List<Edge> _edgePath;

        private Rectangle _lastRectangle;

        public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
        {
            InitializeLists(edges);

            if (!SetStartEdges(A, C))
                return new List<Vector2>();

            if (StartAndEndInOneRectangle() || StartAndEndInNearbyRectangle())
                return _path;

            bool edgesMarked = false;

            while (!edgesMarked)
            {
                AddEdges();

                if (_markedRectangles.Contains(_endRectangle))
                    edgesMarked = true;
                else if (_currentEdges.Count == 0)
                    return new List<Vector2>();
            }

            AddEndEdge();

            List<Edge> _startEdges = GetRectangleEdges(_startRectangle);

            bool edgePathCreated = false;

            while (!edgePathCreated)
            {
                AddEdgePath();

                if (_startEdges.Contains(_edgePath[_edgePath.Count - 1]))
                    edgePathCreated = true;
            }

            _edgePath.Reverse();

            _path.Add(_startPoint);
            _lastRectangle = _startRectangle;

            bool pathCreated = false;

            while (!pathCreated)
            {
                AddPath();

                if (_path.Contains(_endPoint))
                    pathCreated = true;
            }

            return _path;
        }

        #region Initialization

        private void InitializeLists(IEnumerable<Edge> edges)
        {
            _allEdges = edges.ToList();

            _markedEdgesCosts = new List<int>(_allEdges.Count);
            _markedRectangles = new List<Rectangle>(_allEdges.Count);
            _markedEdges = new List<Edge>(_allEdges.Count);

            _currentEdges = new List<Edge>(_allEdges.Count / 2);

            _edgePath = new List<Edge>(_allEdges.Count / 2);
            _path = new List<Vector2>(_allEdges.Count / 2);
        }

        private bool SetStartEdges(Vector2 start, Vector2 end)
        {
            if (!TryGetVectorRectangle(start, out Rectangle startRectangle) ||
                !TryGetVectorRectangle(end, out Rectangle endRectangle))
            {
                return false;
            }

            List<Edge> edges = GetRectangleEdges(startRectangle);

            _markedRectangles.Add(startRectangle);
            _markedEdges.AddRange(edges);

            for (int i = 0; i < edges.Count; i++)
                _markedEdgesCosts.Add(0);

            _currentEdges.AddRange(edges);

            _startPoint = start;
            _startRectangle = startRectangle;

            _endPoint = end;
            _endRectangle = endRectangle;

            return true;
        }

        private bool StartAndEndInOneRectangle()
        {
            if (!IsRectanglesEquals(_startRectangle, _endRectangle))
                return false;

            _path.Add(_startPoint);
            _path.Add(_endPoint);
            return true;
        }

        private bool StartAndEndInNearbyRectangle()
        {
            for (int i = 0; i < _markedEdges.Count; i++)
            {
                if ((IsRectanglesEquals(_markedEdges[i].First, _startRectangle) || IsRectanglesEquals(_markedEdges[i].First, _endRectangle)) &&
                    (IsRectanglesEquals(_markedEdges[i].Second, _startRectangle) || IsRectanglesEquals(_markedEdges[i].Second, _endRectangle)))
                {
                    _path.Add(_startPoint);
                    _path.Add(_markedEdges[i].Start + ((_markedEdges[i].End - _markedEdges[i].Start) / 2));
                    _path.Add(_endPoint);
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Mark edges

        private void AddEdges()
        {
            for (int i = _currentEdges.Count - 1; i >= 0; i--)
            {
                int cost = _markedEdgesCosts[GetEdgeIndex(_currentEdges[i], _markedEdges)];

                TryMarkNewRectangle(_currentEdges[i].First, cost);
                TryMarkNewRectangle(_currentEdges[i].Second, cost);

                _currentEdges.RemoveAt(i);
            }
        }

        private void TryMarkNewRectangle(Rectangle rectangle, int cost)
        {
            if (!_markedRectangles.Contains(rectangle))
                MarkNewRectangle(rectangle, cost);
        }

        private void MarkNewRectangle(Rectangle rectangle, int cost)
        {
            _markedRectangles.Add(rectangle);

            List<Edge> edges = GetRectangleEdges(rectangle);

            for (int i = 0; i < edges.Count; i++)
            {
                if (!_markedEdges.Contains(edges[i]))
                {
                    _markedEdges.Add(edges[i]);
                    _currentEdges.Add(edges[i]);
                    _markedEdgesCosts.Add(cost + 1);
                }
            }
        }

        #endregion

        #region Create edge path

        public void AddEndEdge()
        {
            List<Edge> edges = GetRectangleEdges(_endRectangle);

            if (edges.Count == 0)
                throw new System.Exception("Edges count is 0!");

            int minCost = int.MaxValue;
            int index = 0;

            for (int i = 0; i < edges.Count; i++)
            {
                if (!_markedEdges.Contains(edges[i]))
                    continue;

                int cost = _markedEdgesCosts[GetEdgeIndex(edges[i], _markedEdges)];

                if (cost < minCost)
                {
                    minCost = cost;
                    index = i;
                }
            }

            _edgePath.Add(edges[index]);
            _lastRectangle = _endRectangle;
        }

        public void AddEdgePath()
        {
            Edge lastEdge = _edgePath[_edgePath.Count - 1];
            Rectangle newRectangle = IsRectanglesEquals(lastEdge.Second, _lastRectangle) ? lastEdge.First : lastEdge.Second;

            List<Edge> edges = GetRectangleEdges(newRectangle);
            edges.Remove(lastEdge);

            int minCost = int.MaxValue;
            int index = 0;

            for (int i = 0; i < edges.Count; i++)
            {
                if (_markedEdges.Contains(edges[i]))
                {
                    int markIndex = GetEdgeIndex(edges[i], _markedEdges);

                    if (_markedEdgesCosts[markIndex] <= minCost)
                    {
                        minCost = _markedEdgesCosts[markIndex];
                        index = i;
                    }
                }
            }

            _edgePath.Add(edges[index]);
            _lastRectangle = newRectangle;
        }

        #endregion

        #region Create path

        public void AddPath()
        {
            Vector2 point = _path[_path.Count - 1];
            _path.Add(GetNextPoint(point, _edgePath[_path.Count - 1]));

            if (IsRectanglesEquals(_lastRectangle, _endRectangle))
                _path.Add(_endPoint);
        }

        private Vector2 GetNextPoint(Vector2 currentPoint, Edge currentEdge)
        {
            _lastRectangle = IsRectanglesEquals(_lastRectangle, currentEdge.First) ? currentEdge.Second : currentEdge.First;

            Vector2 halfDifferenceOfEdge = currentEdge.Start + ((currentEdge.End - currentEdge.Start) / 2);
            Vector2 rectangleSize = _lastRectangle.Max - _lastRectangle.Min;

            Vector2 heading = halfDifferenceOfEdge - currentPoint;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance;

            float startDistance = rectangleSize.x * rectangleSize.y;
            float distanceAddition = startDistance / 20;

            Vector2 nextPoint = halfDifferenceOfEdge + direction * startDistance;
            bool isPointSetted = false;

            int attempts = 50;

            while (!isPointSetted)
            {
                if (RectangleContainsVector(_lastRectangle, nextPoint))
                    return nextPoint;

                nextPoint -= direction * distanceAddition;

                attempts--;

                if (attempts <= 0)
                    throw new System.Exception("Attempts is out!");
            }

            return default;
        }

        #endregion

        private List<Edge> GetRectangleEdges(Rectangle rectangle)
        {
            List<Edge> edges = new List<Edge>(1);

            for (int i = 0; i < _allEdges.Count; i++)
            {
                if (IsRectanglesEquals(_allEdges[i].First, rectangle) || IsRectanglesEquals(_allEdges[i].Second, rectangle))
                {
                    edges.Add(_allEdges[i]);
                }
            }

            return edges;
        }

        private bool TryGetVectorRectangle(Vector2 vector, out Rectangle rectangle)
        {
            for (int i = 0; i < _allEdges.Count; i++)
            {
                if (RectangleContainsVector(_allEdges[i].First, vector))
                {
                    rectangle = _allEdges[i].First;
                    return true;
                }
                else if (RectangleContainsVector(_allEdges[i].Second, vector))
                {
                    rectangle = _allEdges[i].Second;
                    return true;
                }
            }

            rectangle = default;
            return false;
        }

        private int GetEdgeIndex(Edge edge, List<Edge> edges)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (IsEdgesEquals(edges[i], edge))
                    return i;
            }

            return -1;
        }

        private bool IsEdgesEquals(Edge edge1, Edge edge2) => IsRectanglesEquals(edge1.First, edge2.First) && IsRectanglesEquals(edge1.Second, edge2.Second) && edge1.Start == edge2.Start && edge1.End == edge2.End;

        private bool IsRectanglesEquals(Rectangle rectangle1, Rectangle rectangle2) => rectangle1.Min == rectangle2.Min && rectangle1.Max == rectangle2.Max;

        private bool RectangleContainsVector(Rectangle rectangle, Vector2 vector) => vector.x >= rectangle.Min.x && vector.y >= rectangle.Min.y && vector.x <= rectangle.Max.x && vector.y <= rectangle.Max.y;
    }
}
