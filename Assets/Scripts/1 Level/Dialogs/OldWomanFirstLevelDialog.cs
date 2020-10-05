using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OldWomanFirstLevelDialog : Interactable {

    FinderController finderController;
    GenericDialog dialog;
    InGameUIManagerTimerClue inGameUIManagerTimerClue;
    bool GameStarted = false;
    bool GameFinished = false;
    private List<string> dialogs = new List<string>();//[ "Some horrible creatures made me run through the city...", "in the process I lost some objects, can you look for these...", "can you look for these, I will give you this shiny rock I found"];
    public List<Item> items;
    int page = 0;
    //private DisplayManager displayManager;

    void Awake() {
        finderController = FinderController.Instance();
        dialog = GenericDialog.Instance();
        dialogs.Add( "Some horrible creatures made me run through the city..." );
        dialogs.Add( "in the process I lost some objects, can you look for these?..." );
        dialogs.Add( "I will give you this shiny rock I found" );
        //displayManager = DisplayManager.Instance();
        inGameUIManagerTimerClue = InGameUIManagerTimerClue.Instance();
    }

    public override void Interact() {
        //dialog.SetHidenTime( 2 );
        if (!finderController.GetGameStatus()) {
            if (!GameStarted) {
                /*base.Interact();
                dialog.SetTitle( "Could you help me?" );
                dialog.SetMessage( "Some horrible creatures made me run through the city, in the process I lost some objects, can you look for these, I will give you this shiny rock I found" );
                dialog.SetOnAccept( "Thank you so much, take this list", () => {
                    dialog.Hide();
                    YesFunction();
                    //dialog.SetHidenTime(5);
                    GameStarted = true;
                } );
                dialog.SetOnDecline( "Nowadays youngers", () => dialog.Hide() );
                dialog.Show();*/
                NextPage();
            } else {
                base.Interact();
                dialog.SetTitle( "Could you help me?" );
                dialog.SetMessage( "Did you find my stuff?" );
                if (!finderController.GetGameStatus()) {
                    dialog.SetOnAccept( "Go to find my stuff, you lazy", () => {
                        dialog.Hide();
                        //dialog.SetHidenTime(5);
                    } );
                } else {
                    dialog.SetOnAccept( "Thank you so much, take this shiny rock", () => {
                        dialog.Hide();
                        //dialog.SetHidenTime(5);
                        inGameUIManagerTimerClue.StopClueSearching();
                        GameStarted = true;
                    } );
                }
                dialog.SetOnDecline( "Okay, I'm still waiting", () => dialog.Hide() );
                dialog.Show();
            }
        } else {
            base.Interact();
            dialog.SetTitle( "Could you help me?" );
            dialog.SetMessage( "Thank you so much for finding my stuff" );
            dialog.SetOnAccept( " ", () => {
                dialog.Hide();
            } );
            dialog.SetOnDecline( " ", () => dialog.Hide() );
            dialog.Show();
        }
        
        
        
    }

    void NextPage() {
        Debug.Log(dialogs.Count);
        Debug.Log( page );
        if (page < (dialogs.Count - 1)) {
            base.Interact();
            dialog.SetTitle( "Could you help me?" );
            dialog.SetMessage( dialogs[ page ] );
            dialog.SetOnAccept( "Thank you so much, take this", () => {
                page++;
                NextPage();
                //dialog.SetHidenTime(5);
                GameStarted = true;
            } );
            dialog.SetOnDecline( "Nowadays youngers", () => dialog.Hide() );
            dialog.Show();
        } else {
            base.Interact();
            dialog.SetTitle( "Could you help me?" );
            dialog.SetMessage( dialogs[ page ] );
            dialog.SetOnAccept( "", () => {
                YesFunction();
                dialog.Hide();
                //dialog.SetHidenTime(5);
                GameStarted = true;
            } );
            dialog.SetOnDecline( "Nowadays youngers", () => dialog.Hide() );
            dialog.Show();
        }
        
    }

    void GiveItem() {

        foreach (Item item in items) {
            bool wasPickesUp = Inventory.instance.Add( item );
            if (wasPickesUp) {
                //Destroy( gameObject );
                Debug.Log( "Entregado Item!" );
            }
        }

    }

    void YesFunction() {
        //dialog.SetHidenTime( 2 );
        Debug.Log( finderController.timeTrial );
        if (!finderController.timeTrial) {
            //displayManager.DisplayMessage( "Yes" );
            GiveItem();
            inGameUIManagerTimerClue.StarClueSearching();
            finderController.CollectablePickedUp();
        }

    }
}
