/*
 * Creator: Juan David Suaza
 * Purpose: Control all interaction of the main character, but movement
 *          Movement is done by StandardAssets Scripts
*/

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractionController : MonoBehaviour{

    [Range(10, 500)]
    public int maxCheckDistance = 100;

    protected Camera cam;
    //Interactable focus;
    protected Vector3 clickPosFar;
    protected Vector3 clickPosNear;

    public LayerMask layerMask;

    protected Vector3 worldPointFar;
    protected Vector3 worldPointNear;

    protected RaycastHit hitObject;

    protected Vector3 inputMousePos;
    Interactable hitInteractable;

    protected void Start()
    {
        cam = Camera.main;
        clickPosFar = new Vector3(
                0f,
                0f,
                cam.farClipPlane
                );

        clickPosNear = new Vector3(
            0f,
            0f,
            cam.nearClipPlane
            );

        inputMousePos = new Vector3(0f, 0f, 0f);
    }
    protected void Update () {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            inputMousePos = Input.mousePosition;
            clickPosFar.x = inputMousePos.x;
            clickPosFar.y = inputMousePos.y;

            clickPosNear.x = inputMousePos.x;
            clickPosNear.y = inputMousePos.y;

            worldPointFar = cam.ScreenToWorldPoint(clickPosFar);
            worldPointNear = cam.ScreenToWorldPoint(clickPosNear);

            Debug.DrawRay(worldPointFar, worldPointFar - worldPointNear, Color.green);

            if (Physics.Raycast(worldPointNear, worldPointFar - worldPointNear, out hitObject, maxCheckDistance, layerMask))
            {
                hitInteractable = hitObject.collider.GetComponent<Interactable>();
                if (hitInteractable != null)
                {
                    if((transform.position - hitInteractable.transform.position).magnitude < hitInteractable.interactionRadius)
                    {
                        hitInteractable.Interact();
                        hitInteractable = null;
                    }
                }
            }
        }
    }

    //void SetFocus(Interactable newFocus)
    //{
    //    if(newFocus != this.focus)
    //    {
    //        if(focus != null)
    //            focus.OnDefocused();
    //        this.focus = newFocus;
    //    }

    //    this.focus.OnFocused(transform);
    //}

    //void RemoveFocus()
    //{
    //    if (focus != null)
    //        focus.OnDefocused();
    //    this.focus = null;
    //}
}
