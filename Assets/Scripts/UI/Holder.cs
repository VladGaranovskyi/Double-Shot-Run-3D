using UnityEngine;
using UnityEngine.EventSystems;
using States;

public class Holder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static Holder instance;

    private Vector2 origin; 

    public bool Hold { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 CurrentPosition { get; private set; }

    private void Awake()
    {
        Direction = Vector2.zero;
        instance = this;
        Hold = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData) 
    { 
        origin = eventData.position;
        CurrentPosition = eventData.position;
        Hold = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        CurrentPosition = eventData.position;
        Direction = CurrentPosition - origin;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) 
    { 
        Direction = Vector2.zero;
        Hold = false;
    }
}
