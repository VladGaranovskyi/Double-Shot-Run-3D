using UnityEngine;
using UnityEngine.UI;

public class WallModeProgressDisplayer : MonoBehaviour
{
    [SerializeField] private RectTransform _greenSide;
    [SerializeField] private RectTransform _redSide;
    [SerializeField] private RectTransform _greenAnchor;
    [SerializeField] private RectTransform _redAnchor;
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private float _defaultHeight;
    private Vector2 _redSidePos;
    private Vector2 _greenSidePos;
    private Vector2 _redSideSize;
    private Vector2 _greenSideSize;
    private Vector2 _cursorPos;

    private void Awake()
    {
        _redSidePos = _redSide.position;
        _redSideSize = _redSide.sizeDelta;
        _greenSidePos = _greenSide.position;
        _greenSideSize = _greenSide.sizeDelta;
        _cursorPos = _cursor.position;
    }

    public void DisplayProgress(float hp)
    {
        float lambda = (hp < 100 ? (hp / 100f) / ((100 - hp) / 100f) : 2f);
        _cursor.position = new Vector2((_greenAnchor.position.x + (_redAnchor.position.x * lambda)) / (1 + lambda), _cursor.position.y);

        _greenSide.position = new Vector2((_cursor.position.x + _greenAnchor.position.x) / 2f, _greenSide.position.y);
        _greenSide.sizeDelta = new Vector2(Mathf.Abs(_greenAnchor.position.x - _cursor.position.x), _defaultHeight);

        _redSide.position = new Vector2((_cursor.position.x + _redAnchor.position.x) / 2f, _redSide.position.y);
        _redSide.sizeDelta = new Vector2(Mathf.Abs(_redAnchor.position.x - _cursor.position.x), _defaultHeight);
    }

    public void ResetPositions()
    {
        _cursor.position = _cursorPos;

        _greenSide.position = _greenSidePos;
        _greenSide.sizeDelta = _greenSideSize;

        _redSide.position = _redSidePos;
        _redSide.sizeDelta = _redSideSize;
    }
}
