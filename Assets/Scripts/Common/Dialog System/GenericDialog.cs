using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent( typeof( CanvasGroup ) )]
public class GenericDialog : MonoBehaviour {

    public Text title;
    public Text message;
    public Text accept, decline;
    public Button acceptButton, declineButton;
    public int hidenTime = 1;

    private CanvasGroup cg;

    void Awake() {
        cg = GetComponent<CanvasGroup>();
    }

    public GenericDialog OnAccept(string text, UnityAction action) {
        accept.text = text;
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener( action );
        return this;
    }



    public GenericDialog OnDecline(string text, UnityAction action) {
        decline.text = text;
        declineButton.onClick.RemoveAllListeners();
        declineButton.onClick.AddListener( action );
        return this;
    }

    public GenericDialog Title(string title) {
        this.title.text = title;
        return this;
    }

    public GenericDialog Message(string message) {
        this.message.text = message;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show() {
        this.transform.SetAsLastSibling();
        cg.interactable = true;
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void Hide() {
        cg.interactable = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds( hidenTime );
        cg.alpha = 0f;
        cg.blocksRaycasts = true;
    }

    private static GenericDialog instance;
    public static GenericDialog Instance() {
        if (!instance) {
            instance = FindObjectOfType( typeof( GenericDialog ) ) as GenericDialog;
            if (!instance)
                Debug.Log( "There need to be at least one active GenericDialog on the scene" );
        }

        return instance;
    }

    public void SetTitle(string text) {
        this.title.text = text;
    }

    public void SetMessage(string text) {
        this.message.text = text;
    }

    public void SetHidenTime(int number) {
        this.hidenTime = number;
    }

    public void SetOnAccept(string text, UnityAction action) {
        this.acceptButton.onClick.RemoveAllListeners();
        this.acceptButton.onClick.AddListener( () => {
            this.decline.text = text;
            action();
        } );
        this.decline.text = "";
    }

    public void SetOnDecline(string text, UnityAction action) {
        this.declineButton.onClick.RemoveAllListeners();
        this.declineButton.onClick.AddListener( () => {
            this.decline.text = text;
            action();
        } );
        this.accept.text = "";
    }
}
