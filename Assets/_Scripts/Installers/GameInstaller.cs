using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("GameManager Properties")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Bets _bets;

    [Header("BallManager Properties")]
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _ballSpawnPosition;
    [SerializeField] private float _spawnRadius;

    [Header("Slots Properties")]
    [SerializeField] private Slot _slotPrefab;

    [SerializeField] private SlotMultipliers _greenMultipliers;
    [SerializeField] private SlotMultipliers _yellowMultipliers;
    [SerializeField] private SlotMultipliers _redMultipliers;

    [SerializeField] Transform _slotsSpawnPosition;
    [SerializeField] float _slotsSpawnOffsetX;
    [SerializeField] float _slotsSpawnOffsetY;

    [Header("UIManager Properties")]
    [SerializeField] private UIManager _uiManager;

    private List<Slot> _slots = new List<Slot>();

    public override void InstallBindings()
    {
        Container.Bind<Bets>().FromInstance(_bets).AsSingle();
        Container.Bind<BallManager>().AsSingle().WithArguments(_ballPrefab, _ballSpawnPosition, _spawnRadius).NonLazy();
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();

        SpawnSlots(ColorType.Green, _greenMultipliers.multipliers, _slotsSpawnOffsetY * 2);
        SpawnSlots(ColorType.Yellow, _yellowMultipliers.multipliers, _slotsSpawnOffsetY);
        SpawnSlots(ColorType.Red, _redMultipliers.multipliers, 0);
    }

    private void SpawnSlots(ColorType color, List<float> multipliers, float slotsSpawnOffsetY)
    {
        for (int i = 0; i < multipliers.Count; i++)
        {
            if (i == 0)
            {
                Vector2 spawnPosition = new Vector2(_slotsSpawnPosition.position.x, _slotsSpawnPosition.position.y + slotsSpawnOffsetY);
                var newSlot = Container.InstantiatePrefabForComponent<Slot>(_slotPrefab, _slotsSpawnPosition);
                Container.BindInterfacesAndSelfTo<Slot>().FromInstance(newSlot).AsTransient().NonLazy();

                newSlot.transform.position = spawnPosition;
                newSlot.Init(color, multipliers[i]);
                _slots.Add(newSlot);
            }
            else
            {
                Vector2 spawnPosition = new Vector2(_slotsSpawnPosition.position.x - _slotsSpawnOffsetX * i, _slotsSpawnPosition.position.y + slotsSpawnOffsetY);
                var newSlot = Container.InstantiatePrefabForComponent<Slot>(_slotPrefab, _slotsSpawnPosition);
                Container.BindInterfacesAndSelfTo<Slot>().FromInstance(newSlot).AsTransient().NonLazy();

                newSlot.transform.position = spawnPosition;
                newSlot.Init(color, multipliers[i]);
                _slots.Add(newSlot);

                spawnPosition = new Vector2(_slotsSpawnPosition.position.x + _slotsSpawnOffsetX * i, _slotsSpawnPosition.position.y + slotsSpawnOffsetY);
                newSlot = Container.InstantiatePrefabForComponent<Slot>(_slotPrefab, _slotsSpawnPosition);
                Container.BindInterfacesAndSelfTo<Slot>().FromInstance(newSlot).AsTransient().NonLazy();

                newSlot.transform.position = spawnPosition;
                newSlot.Init(color, multipliers[i]);
                _slots.Add(newSlot);
            }
        }
    }

    public override void Start()
    {
        GameManager gameManager = Container.Resolve<GameManager>();
        BallManager ballManager = Container.Resolve<BallManager>();

        foreach (var slot in _slots)
        {
            slot.onBallEnter += ballManager.OnBallReturned;
            slot.onWinRecieve += gameManager.OnMoneyWin;
        }

    }

}