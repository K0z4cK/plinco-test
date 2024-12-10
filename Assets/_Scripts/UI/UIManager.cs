using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Main GUI")]
    [SerializeField] private TMP_Text _moneyTMP;
    [SerializeField] private TMP_InputField _betField;
    [SerializeField] private Button _addBetBtn;
    [SerializeField] private Button _substractBetBtn;
    [SerializeField] private Button _greenBtn;
    [SerializeField] private Button _yellowBtn;
    [SerializeField] private Button _redBtn;

    [Header("Low Balance Panel")]
    [SerializeField] private GameObject _lowBalancePanel;
    [SerializeField] private Button _confirmReplenishBtn;
    [SerializeField] private Button _cancelReplenishBtn;

    public void Init(UnityAction onAddBet, UnityAction onSubstractBet, UnityAction<ColorType> onSpawnBall, UnityAction onReplenish)
    {
        _addBetBtn.onClick.AddListener(onAddBet);
        _substractBetBtn.onClick.AddListener(onSubstractBet);
        _greenBtn.onClick.AddListener(delegate { onSpawnBall?.Invoke(ColorType.Green); });
        _yellowBtn.onClick.AddListener(delegate { onSpawnBall?.Invoke(ColorType.Yellow); });
        _redBtn.onClick.AddListener(delegate { onSpawnBall?.Invoke(ColorType.Red); });
        _confirmReplenishBtn.onClick.AddListener(delegate { _lowBalancePanel.SetActive(false); onReplenish?.Invoke(); });
        _cancelReplenishBtn.onClick?.AddListener(delegate { _lowBalancePanel.SetActive(false); });
    }

    public void ShowLowBalancePanel() => _lowBalancePanel.SetActive(true);

    public void UpdateMoney(float money) => _moneyTMP.text = money + "$";

    public void UpdateBet(float bet) => _betField.text = bet + "$";
}
