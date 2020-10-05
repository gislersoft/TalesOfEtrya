using System.Collections.Generic;
using UnityEngine;

public class MultiTargetCameraController : MonoBehaviour
{
    private Camera camera;
    [SerializeField]
    private List<Transform> listOfTargets = new List<Transform>();
    private Vector3 centerPoint;
    public Vector3 multiTargetOffset;
    private Vector3 velocity;
    public float smoothTime = 0.5f;

    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomLimiter = 50f;

    private void Start()
    {
        camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (listOfTargets.Count == 0)
            return;

        Move();

        Zoom();
        
        
    }

    public void Move()
    {
        centerPoint = GetCenterPoint() + multiTargetOffset;

        transform.position = Vector3.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetGreatestDistance() / zoomLimiter);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        int listOfTargetCount = listOfTargets.Count;

        var bounds = new Bounds(listOfTargets[0].position, Vector3.zero);

        for (int i = 0; i < listOfTargetCount; i++)
        {
            bounds.Encapsulate(listOfTargets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        int listOfTargetCount = listOfTargets.Count;

        if (listOfTargetCount == 1)
            return listOfTargets[0].position;

        var bounds = new Bounds(listOfTargets[0].position, Vector3.zero);

        for (int i = 0; i < listOfTargetCount; i++)
        {
            bounds.Encapsulate(listOfTargets[i].position);
        }

        return bounds.center;
    }


}
