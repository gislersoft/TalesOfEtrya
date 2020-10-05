using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image imageHolder;
    public Image joystickImage;
    private Vector2 inputVector;
    private readonly Vector3 initialPosition = Vector3.zero;
    [Range(1,10)]
    public int movementRange = 4;

    private void Start()
    {
        imageHolder = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageHolder.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            pos.x = pos.x / imageHolder.rectTransform.sizeDelta.x;
            pos.y = pos.y / imageHolder.rectTransform.sizeDelta.y;

            float x = (imageHolder.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (imageHolder.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            inputVector = new Vector3(x, y);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            joystickImage.rectTransform.anchoredPosition = new Vector2(
                inputVector.x * (imageHolder.rectTransform.sizeDelta.x / movementRange),
                inputVector.y * (imageHolder.rectTransform.sizeDelta.y / movementRange));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = initialPosition;
        joystickImage.rectTransform.anchoredPosition = initialPosition;
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }
}
