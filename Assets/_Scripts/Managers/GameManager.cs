using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startBetIndex;
    [SerializeField] private float _startMoney;

    private BallManager _ballManager;
    private UIManager _uiManager;
    private Bets _bets;

    private float _money;

    private float _bet;
    private int _betIndex;

    [Inject]
    public void Construct(BallManager ballManager, UIManager uiManager, Bets bets)
    {
        _ballManager = ballManager;
        _uiManager = uiManager;
        _bets = bets;
        _money = _startMoney;
        _betIndex = _startBetIndex;
        _bet = _bets.bets[_betIndex];

        _uiManager.Init(AddBet, SubstractBet, SpawnBall, ResetMoney);
        _uiManager.UpdateMoney(_money);
        _uiManager.UpdateBet(_bet);
    }

    private void ResetMoney()
    {
        _money = _startMoney;
        _uiManager.UpdateMoney(_money);
    }

    public void OnMoneyWin(float amount) 
    { 
        _money += amount;
        _uiManager.UpdateMoney(_money);
    }

    public void OnMoneyBet(float amount)
    {
        _money -= amount;
        _uiManager.UpdateMoney(_money);
    }

    public void AddBet()
    {
        _betIndex++;
        if (_betIndex == _bets.bets.Count)
            _betIndex = _bets.bets.Count-1;
        _bet = _bets.bets[_betIndex];
        _uiManager.UpdateBet(_bet);
    }

    public void SubstractBet()
    {
        _betIndex--;
        if (_betIndex < 0)
            _betIndex = 0;

        _bet = _bets.bets[_betIndex];
        _uiManager.UpdateBet(_bet);
    }

    public void SpawnBall(ColorType color)
    {
        if(_money - _bet < 0)
        {
            _uiManager.ShowLowBalancePanel();
            return;
        }

        OnMoneyBet(_bet);
        _ballManager.SpawnBall(_bet, color);
    }
}
