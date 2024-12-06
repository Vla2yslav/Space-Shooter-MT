using System;
using Lean.Pool;
using UnityEngine;

namespace Game
{
    public class Gun : MonoBehaviour
    {
        public event Action OnHit; 
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private LeanGameObjectPool _bulletPool;
        
        private bool _isEnable;
        private float _nextShootTime;
        private float _shootingFrequency = 0.5f;
        private Vector2 _screenBounds;

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;
            
            if (!isEnable)
            {
                _bulletPool.DespawnAll();
            }
        }
        public void SetScreenBounds(Vector2 screenBoundaries) => _screenBounds = screenBoundaries;
        
        public void SetShootingFrequency(float frequency) => _shootingFrequency = frequency;

        private void Start() => _bulletPool.Prefab = _bulletPrefab.gameObject;

        private void Update()
        {
            if (_isEnable && Time.time >= _nextShootTime)
            {
                Shoot();
                _nextShootTime = Time.time + _shootingFrequency;
            }
        }

        private void Shoot()
        {
            GameObject bulletPrefab =_bulletPool.Spawn(_bulletPool.transform);
            bulletPrefab.transform.position = transform.position;
            bulletPrefab.transform.rotation = transform.rotation;

            Bullet bullet = bulletPrefab.GetComponent<Bullet>();
            bullet.Initialize(_screenBounds.y + 1f, _bulletPool, OnHitAsteroid);
        }

        private void OnHitAsteroid() => OnHit?.Invoke();
    }
}