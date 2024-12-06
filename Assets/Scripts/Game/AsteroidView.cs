using Lean.Pool;
using Service;
using UnityEngine;

namespace Game
{
    public class AsteroidView : MonoBehaviour, IDamageable, IPoolable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private Collider2D _collider;
        
        private int _health = Config.Constants.DEFAULT_ASTEROID_HEALTH;
        
        private LeanGameObjectPool _asteroidPool;
        private float _bottomBoundary; 
        private float _currentRotationSpeed;

        public void Initialize(Sprite sprite, Vector2 spawnPosition, float speed, float bottomBoundary, LeanGameObjectPool asteroidPool)
        {
            _spriteRenderer.sprite = sprite;
            _asteroidPool = asteroidPool;
            transform.position = spawnPosition;
            _moveSpeed = speed;
            _rotationSpeed = speed;
            _bottomBoundary = bottomBoundary;
            
            _currentRotationSpeed = Random.Range(0, 2) == 0 ? -_rotationSpeed : _rotationSpeed;
        }

        private void Update()
        {
            transform.position += Vector3.down * _moveSpeed * Time.deltaTime;
            transform.Rotate(0, 0, _currentRotationSpeed * Time.deltaTime);
            
            if (transform.position.y < _bottomBoundary)
            {
                _asteroidPool.Despawn(gameObject);
            }
        }

        public void OnSpawn()
        {
            _health = Config.Constants.DEFAULT_ASTEROID_HEALTH;
            _collider.enabled = true;
        }

        public void OnDespawn() => _collider.enabled = false;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _asteroidPool.Despawn(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IDamageable ship = other.GetComponent<IDamageable>();
                if (ship != null)
                {
                    _asteroidPool.Despawn(gameObject);
                    ship.TakeDamage(Config.Constants.DEFAULT_DAMAGE);
                }
            }
        }
    }
}