using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _money;

    private BallManager _ballManager;

    [Inject]
    public void Construct(BallManager ballManager)
    {
        _ballManager = ballManager;
        print(_ballManager.GetType());
    }

    public void OnMoneyWin(float amount)
    {
        _money += amount;
    }

    public void OnMoneyBet(float amount)
    {
        _money -= amount;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ColorType randomColor = (ColorType)Random.Range(0, 3);
            _ballManager.SpawnBall(0, randomColor);
        }
    }
}
