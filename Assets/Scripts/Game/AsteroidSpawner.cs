using Lean.Pool;
using Service;
using UnityEngine;

namespace Game
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidView _asteroidPrefab;
        [SerializeField] private Sprite[] _asteroidSprites;
        [SerializeField] private LeanGameObjectPool _asteroidPool;

        private bool _isEnable;
        private float _nextSpawnTime;
        private Vector2 _screenBounds;
        
        private float _asteroidSpeed = 1f;
        private float _spawnFrequency = 2f;

        public void SetEnable(bool isEnable)
        {
            _isEnable = isEnable;

            if (!isEnable)
            {
                _asteroidPool.DespawnAll();
            }
        }
        
        public void SetAsteroidSpeed(float speed) => _asteroidSpeed = speed;
        
        public void SetAsteroidSpawnFrequency(float frequency) => _spawnFrequency = frequency;

        private void Start()
        {
            _asteroidPool.Prefab = _asteroidPrefab.gameObject;
            CalculateSpawnRadius();
        }

        private void CalculateSpawnRadius()
        {
            _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            _screenBounds.x -= Config.Constants.DEFAULT_SCREEN_BOUND_OFFSET;
            _screenBounds.y += Config.Constants.DEFAULT_SCREEN_BOUND_OFFSET;
        }

        private void Update()
        {
            if (_isEnable && Time.time >= _nextSpawnTime)
            {
                SpawnAsteroid();
                _nextSpawnTime = Time.time + _spawnFrequency;
            }
        }

        private void SpawnAsteroid()
        {
            float randomX = Random.Range(-_screenBounds.x, _screenBounds.x);
            Vector3 spawnPosition = new Vector3(randomX, _screenBounds.y, 0);

            GameObject asteroidPrefab = _asteroidPool.Spawn(_asteroidPool.transform);
            AsteroidView asteroid = asteroidPrefab.GetComponent<AsteroidView>();
            asteroid.Initialize(_asteroidSprites[Random.Range(0, _asteroidSprites.Length)], spawnPosition, _asteroidSpeed, -_screenBounds.y, _asteroidPool);
        }
    }
}