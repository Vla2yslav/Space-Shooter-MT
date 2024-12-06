using System;
using Lean.Pool;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _moveSpeed = 10f;

        private float _topBoundary;
        private LeanGameObjectPool _bulletsPool;
        private Action _onHitCallback;

        public void Initialize(float topBoundary, LeanGameObjectPool pool, Action onHitCallback)
        {
            _topBoundary = topBoundary;
            _bulletsPool = pool;
            _onHitCallback = onHitCallback;
        }

        public void OnSpawn() { }

        public void OnDespawn() { }

        private void Update()
        {
            transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);

            if (transform.position.y > _topBoundary)
            {
                _bulletsPool.Despawn(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Asteroid"))
            {
                IDamageable damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    _bulletsPool.Despawn(gameObject);
                    _onHitCallback?.Invoke();
                    damageable.TakeDamage(1);
                }
            }
        }
    }
}