using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloatingJoystick : Joystick
{
    //public Image imageHolder;
    //public Image joystickImage;
    private Vector2 joystickCenter;
    //private readonly Vector3 initialPosition = Vector3.zero;
    //[Range( 1, 10 )]
    //public int movementRange = 4;
    public RectTransform additionalImage;

    void Start()
    {
        background.gameObject.SetActive(false);
        if (additionalImage != null)
        {
            additionalImage.gameObject.SetActive(false);
        }

        //imageHolder = GetComponent<Image>();
        //joystickImage = transform.GetChild( 0 ).GetComponent<Image>();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

        /*Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageHolder.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos )) {
            pos.x = pos.x / imageHolder.rectTransform.sizeDelta.x;
            pos.y = pos.y / imageHolder.rectTransform.sizeDelta.y;

            float x = (imageHolder.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (imageHolder.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            inputVector = new Vector3( x, y );
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            joystickImage.rectTransform.anchoredPosition = new Vector2(
                inputVector.x * (imageHolder.rectTransform.sizeDelta.x / movementRange),
                inputVector.y * (imageHolder.rectTransform.sizeDelta.y / movementRange) );
        }*/
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        if(additionalImage != null)
        {
            additionalImage.gameObject.SetActive(true);
            additionalImage.position = eventData.position;
        }
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        if (additionalImage != null)
        {
            additionalImage.gameObject.SetActive(false);
        }
        inputVector = Vector2.zero;
    }
}