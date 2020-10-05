using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class TestModalWindow : MonoBehaviour
{
    InGameUIManagerTimerClue inGameUIManagerTimerClue;
    FinderController finderController;
    private ModalPanel modalPanel;
    private DisplayManager displayManager;

    private UnityAction myYesAction;
    private UnityAction myNoAction;
    private UnityAction myCancelAction;

    public GameObject questionMark;

    private static TestModalWindow testModalWindow;

    public static TestModalWindow Instance()
    {
        if (!testModalWindow)
        {
            testModalWindow = FindObjectOfType(typeof(TestModalWindow)) as TestModalWindow;
            if (!testModalWindow)
                Debug.LogError("There needs to be one active DisplayManager script on a GameObject in your scene.");
        }

        return testModalWindow;
    }

    void Awake()
    {
        modalPanel = ModalPanel.Instance();
        displayManager = DisplayManager.Instance();
        inGameUIManagerTimerClue = InGameUIManagerTimerClue.Instance();
        finderController = FinderController.Instance();


    }

    //  Send to the Modal Panel to set up the Buttons and Functions to call
    public void TestYNC()
    {
        modalPanel.Choice("Could you help me find my stuff? I must have dropped them on my way home for going so fast. This is a list of the things I lost.", TestYesFunction, TestNoFunction, TestCancelFunction);
        //      modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", myYesAction, myNoAction, myCancelAction);
    }

    /*public void TestYNC()
    {
        modalPanel.Choice("Could you help me find my stuff? I must have dropped them on my way home for going so fast. This is a list of the things I lost.", TestYesFunction, TestNoFunction, TestCancelFunction);
        //      modalPanel.Choice ("Would you like a poke in the eye?\nHow about with a sharp stick?", myYesAction, myNoAction, myCancelAction);
    }*/

    //  These are wrapped into UnityActions
    void TestYesFunction()
    {
        Debug.Log(finderController.timeTrial);
        if (!finderController.timeTrial) {
            displayManager.DisplayMessage( "Yes" );
            inGameUIManagerTimerClue.StarClueSearching();
            finderController.CollectablePickedUp();
            questionMark.SetActive( false );
        }
        
    }

    void TestNoFunction()
    {
        displayManager.DisplayMessage("No");
    }

    void TestCancelFunction()
    {
        displayManager.DisplayMessage("Maybe later");
    }
}
