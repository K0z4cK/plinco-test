using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Slot : MonoBehaviour
{
    private const string BallLayerName = "Ball";

    public event UnityAction<Ball> onBallEnter;
    public event UnityAction<float> onWinRecieve;

    private SpriteRenderer _sprite;
    [SerializeField] private TMP_Text _multiplierTMP;

    private ColorType _color;
    private float _multiplier;
    private string _ballTag;

    public void Init(ColorType color, float multiplier)
    {
        _sprite = GetComponent<SpriteRenderer>();
        _color = color;
        _multiplier = multiplier;

        _sprite.color = ColorUtility.GetColor(_color);
        _multiplierTMP.text = _multiplier.ToString();
        gameObject.layer = LayerMask.NameToLayer(ColorUtility.GetLayerName(_color));
        _ballTag = ColorUtility.GetLayerName(_color);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_ballTag) && other.gameObject.layer == LayerMask.NameToLayer(BallLayerName))
        {
            Debug.Log($"Ball of color {_color} entered. Multiplier: {_multiplier}");

            Ball ball = other.GetComponent<Ball>();

            if (!ball.IsInUse)
                return;

            onBallEnter?.Invoke(ball);
            onWinRecieve?.Invoke(ball.Bet * _multiplier);
        }
    }

    private void OnDisable()
    {
        onBallEnter = null;
        onWinRecieve = null;
    }
}
