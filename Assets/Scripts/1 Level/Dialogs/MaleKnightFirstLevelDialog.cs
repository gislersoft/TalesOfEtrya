using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleKnightFirstLevelDialog : Interactable {
    private GenericDialog dialog;
    int random;


    void Awake() {
        dialog = GenericDialog.Instance();
    }

    public override void Interact() {
        base.Interact();
        random = Random.Range( 1, 2 );
        switch (random) {
            case 1:
                SetDialog1();
                break;
            case 2:
                SetDialog2();
                break;
            case 3:
                SetDialog2();
                break;
            default:
                break;
        }
        dialog.Show();
    }

    public void SetDialog1() {
        dialog.SetTitle( "Hello! are you lost?" );
        dialog.SetMessage( "The hospital is up the street on my left" );
        dialog.SetOnAccept( "Now follow  your destiny", () => { // define what happens when user clicks Yes:
            dialog.Hide();
        } );
        dialog.SetOnDecline( "Okay, I see you are fine", () => dialog.Hide() );
    }

    public void SetDialog2() {
        dialog.SetTitle( "Hello! are you lost?" );
        dialog.SetMessage( "The fire station is just to my right" );
        dialog.SetOnAccept( "Now follow  your destiny", () => { // define what happens when user clicks Yes:
            dialog.Hide();
        } );
        dialog.SetOnDecline( "Okay, I see you are fine", () => dialog.Hide() );
    }

    public void SetDialog3() {
        dialog.SetTitle( "Hello! are you lost?" );
        dialog.SetMessage( "I saw a necklace  a few minutes ago" );
        dialog.SetOnAccept( "Now follow  your destiny", () => { // define what happens when user clicks Yes:
            dialog.Hide();
        } );
        dialog.SetOnDecline( "Okay, I see you are fine", () => dialog.Hide() );
    }
}
