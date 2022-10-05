using UnityEngine;
using NaughtyAttributes;
using System;

namespace RedboonTestProject.Pathfinding
{
    public class RectangleHandler : MonoBehaviour, IHandler<Rectangle>
    {
        public event Action OnHandableObjectInitialized;

        [SerializeField] private SpriteRenderer _sprite;

        [SerializeField, ReadOnly] private Rectangle _rectangle;

        public Rectangle HandableObject => _rectangle;

        private void Awake()
        {
            float random = UnityEngine.Random.Range(0.5f, 1.0f);
            _sprite.color = new Color(random, random, random);
            OnHandableObjectInitialized?.Invoke();
        }

        [Button("Update rectangle")]
        public void UpdateRectangle()
        {
            Vector3 position = transform.position;
            Vector3 scale = transform.localScale;

            _rectangle.Min = new Vector2(position.x - (scale.x / 2), position.y - (scale.y / 2));
            _rectangle.Max = new Vector2(position.x + (scale.x / 2), position.y + (scale.y / 2));

        }
    }
}
