using System;
using Service;
using UnityEngine;

namespace Game
{
    public class ShipView : MonoBehaviour, IDamageable
    {
        public event Action OnGetDamage;
        public event Action OnHitTarget;
        
        [SerializeField] private SpriteRenderer _spriteRenderer; 
        [SerializeField] private float _moveSpeed = 6f;
        [SerializeField] private Gun _gun;
        [SerializeField] private Collider2D _collider;

        private float _minBoundaryX;
        private float _maxBoundaryX;
        private float _minBoundaryY;
        private float _maxBoundaryY;
        
        private bool _isEnable;

        private void Start()
        {
            CalculateScreenBoundaries();
            _gun.OnHit += OnAsteroidHit;
        }

        private void OnDestroy() => _gun.OnHit -= OnAsteroidHit;

        private void CalculateScreenBoundaries()
        {
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            
            _gun.SetScreenBounds(screenBounds);

            _minBoundaryX = -screenBounds.x + _spriteRenderer.bounds.extents.x;
            _maxBoundaryX = screenBounds.x - _spriteRenderer.bounds.extents.x;

            _minBoundaryY = -screenBounds.y + _spriteRenderer.bounds.extents.y;
            _maxBoundaryY = screenBounds.y - _spriteRenderer.bounds.extents.y;
        }

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            _collider.enabled = isEnable;
            _gun.SetEnable(isEnable);
        }

        public void ResetShip() => 
            transform.position = new Vector3(0, 0 - Config.Constants.DEFAULT_SCREEN_BOUND_OFFSET * 2, 0);
        
        public void SetShootingFrequency(float frequency) => _gun.SetShootingFrequency(frequency);

        public void UpdateMovement(Vector2 input)
        {
            if (!_isEnable)
            {
                return;
            }

            Vector3 movement = new Vector3(input.x, input.y, 0) * _moveSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + movement;

            newPosition.x = Mathf.Clamp(newPosition.x, _minBoundaryX, _maxBoundaryX);
            newPosition.y = Mathf.Clamp(newPosition.y, _minBoundaryY, _maxBoundaryY);

            transform.position = newPosition;
        }

        public void TakeDamage(int damage) => OnGetDamage?.Invoke();

        private void OnAsteroidHit() => OnHitTarget?.Invoke();
    }
}