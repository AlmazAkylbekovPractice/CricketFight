using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private BoxCollider2D colliderBox;

    [SerializeField] private bool collided;
    [SerializeField] private bool released;

    private void Awake()
    {
        colliderBox = GetComponent<BoxCollider2D>();
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (collided)
                InputManager.Instance.SwitchToSlider();
        }
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
    }
}
