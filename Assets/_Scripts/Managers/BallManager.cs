using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class BallManager
{
    private Ball _ballPrefab;
    private Transform _spawnPosition;
    private float _spawnRadius;

    private ObjectPool<Ball> _ballsPool;

    [Inject] private DiContainer _diContainer;

    [Inject]
    private void Construct(Ball ballPrefab, Transform spawnPosition, float spawnRadius)
    {
        _ballPrefab = ballPrefab;
        _spawnPosition = spawnPosition;
        _spawnRadius = spawnRadius;
        _ballsPool = new ObjectPool<Ball>(Create, Get, Release);
        Debug.Log("BallManager constructed");
    }

    public void SpawnBall(float bet, ColorType color)
    {
        Ball ball = _ballsPool.Get();
        ball.Init(bet, color);
        //ball.transform.position = spawnPosition;
    }

    public void OnBallReturned(Ball ball)
    {
        Debug.Log($"Ball returned: {ball.name}");
        _ballsPool.Release(ball);
    }

    private Ball Create()
    {
        var ball = _diContainer.InstantiatePrefabForComponent<Ball>(_ballPrefab, _spawnPosition);
        ball.gameObject.SetActive(false);
        return ball;
    }

    private void Get(Ball ball)
    {
        Vector2 spawnPosition = new Vector3(Random.Range(-_spawnRadius, _spawnRadius), Random.Range(-_spawnRadius, _spawnRadius), 0) + _spawnPosition.position;
        Debug.Log($"SpawnBall: {spawnPosition}");
        ball.transform.position = spawnPosition;
        ball.gameObject.SetActive(true);
    }

    private void Release(Ball ball)
    {
        ball.Disable();
    }
}
