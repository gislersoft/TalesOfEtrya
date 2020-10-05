using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class FightInput : MonoBehaviour {

    public float minJumpThreshold = 50f;

    [HideInInspector]
    public static float Horizontal {get; private set; }
    [HideInInspector]
    public static bool Jump { get; private set; }

    public GraphicRaycaster graphicRaycaster;

    public LayerMask layerMask;

	void Update () {

		if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            //Touched a UI element

            PointerEventData ped = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            graphicRaycaster.Raycast(ped, results);

            if(results.Count == 0)
            {
                float screenWidth = Screen.width / 2f;

                var relativeTouch = touch.position.x - screenWidth;

                if (0f <= relativeTouch)
                {
                    Horizontal = Mathf.Clamp(relativeTouch, 0f, 1f);
                }
                else if (relativeTouch < 0f)
                {
                    Horizontal = Mathf.Clamp(relativeTouch, -1f, 0f);
                }
                else
                {
                    Horizontal = 0f;
                }
                Jump = (touch.deltaPosition.y > minJumpThreshold) ? true : false;
            }

            
        }
        else
        {
            Horizontal = 0f;
            Jump = false;
        }
    }
}
