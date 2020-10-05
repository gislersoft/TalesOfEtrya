using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

//using PDollarGestureRecognizer;

public class TouchInputManager : MonoBehaviour
{

    //public static TouchInputManager instance;
    //public delegate void OnLetterRecognized(string recognizedLetter);
    //public OnLetterRecognized onLetterRecognizedCallback;

    //public Transform gestureOnScreenPrefab;

    //private List<Gesture> trainingSet = new List<Gesture>();

    //private List<Point> points = new List<Point>();
    //private int strokeId = -1;

    //private Vector3 virtualKeyPosition = Vector2.zero;
    //private Rect drawArea;

    //private RuntimePlatform platform;
    //private int vertexCount = 0;

    //private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    //private LineRenderer currentGestureLineRenderer;
    //[SerializeField]
    //private bool isEnabled = false;

    //GUI
    //private string message;
    //private bool recognized;

    //int gestureLinesRendererCount;
    //readonly string[] abc = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


    //private void Awake()
    //{
    //    instance = this;
    //}

    //void Start()
    //{
    //    platform = Application.platform;
    //    drawArea = new Rect(0, 0, Screen.width, Screen.height);

    //    Load pre-made gestures
    //    TextAsset[][] gesturesXml = new TextAsset[26][];
    //    var trainingSetPath = "GestureSet/Training Set/";

    //    for (int i = 0; i < abc.Length; i++)
    //    {
    //        gesturesXml[i] = Resources.LoadAll<TextAsset>(trainingSetPath + abc[i]);
    //    }

    //    for (int i = 0; i < gesturesXml.Length; i++)
    //    {
    //        for (int j = 0; j < gesturesXml[i].Length; j++)
    //        {
    //            trainingSet.Add(GestureIO.ReadGestureFromXML(gesturesXml[i][j].text));
    //        }
    //    }

    //    EvaluationGameManager.instance.onWatchingCountdownFinishedCallback += EnableDrawing;
    //    EvaluationGameManager.instance.onDrawingCountdownFinishedCallback += DisableDrawing;
    //}

    //void Update()
    //{
    //    if (isEnabled)
    //    {
    //        if (platform == RuntimePlatform.Android)
    //        {
    //            if (Input.touchCount > 0)
    //            {
    //                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
    //            }
    //        }
    //        else
    //        {
    //            if (Input.GetMouseButton(0))
    //            {
    //                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    //            }
    //        }

    //        if (drawArea.Contains(virtualKeyPosition))
    //        {

    //            if (Input.GetMouseButtonDown(0))
    //            {

    //                if (recognized)
    //                {

    //                    recognized = false;
    //                    strokeId = -1;

    //                    points.Clear();

    //                    gestureLinesRendererCount = gestureLinesRenderer.Count;
    //                    for (int i = 0; i < gestureLinesRendererCount; i++)
    //                    {
    //                        gestureLinesRenderer[i].positionCount = 0;
    //                        Destroy(gestureLinesRenderer[i].gameObject);
    //                    }

    //                    gestureLinesRenderer.Clear();
    //                }

    //                ++strokeId;

    //                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
    //                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

    //                gestureLinesRenderer.Add(currentGestureLineRenderer);

    //                vertexCount = 0;
    //            }

    //            if (Input.GetMouseButton(0))
    //            {
    //                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

    //                currentGestureLineRenderer.positionCount = ++vertexCount;
    //                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
    //            }
    //        }
    //    }
    //}

    //public void Recognize()
    //{
    //    recognized = true;
    //    Gesture candidate = new Gesture(points.ToArray());
    //    Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

    //    message = gestureResult.GestureClass + " " + gestureResult.Score;

    //    if (onLetterRecognizedCallback != null)
    //    {
    //        onLetterRecognizedCallback.Invoke(gestureResult.GestureClass);
    //    }
    //}

    //private void EnableDrawing()
    //{
    //    isEnabled = true;
    //}

    //private void DisableDrawing()
    //{
    //    isEnabled = false;
    //    strokeId = -1;

    //    points.Clear();

    //    gestureLinesRendererCount = gestureLinesRenderer.Count;
    //    for (int i = 0; i < gestureLinesRendererCount; i++)
    //    {
    //        gestureLinesRenderer[i].positionCount = 0;
    //        Destroy(gestureLinesRenderer[i].gameObject);
    //    }

    //    gestureLinesRenderer.Clear();
    //}

    //public void Test()
    //{
    //    message = "Test";
    //}

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10, Screen.height - 40, 500, 50), message);
    //}
}
