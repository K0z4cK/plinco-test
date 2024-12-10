using UnityEngine;
using Zenject;

public class Ball : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private float _bet;
    public float Bet => _bet;

    public bool IsInUse { get; private set; }

    public void Init(float bet, ColorType color)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _bet = bet;
        _spriteRenderer.color = ColorUtility.GetColor(color);
        gameObject.tag = ColorUtility.GetLayerName(color);
        IsInUse = true;
    }

    public void Disable()
    {
        IsInUse = false;
        gameObject.SetActive(false);
    }
}
