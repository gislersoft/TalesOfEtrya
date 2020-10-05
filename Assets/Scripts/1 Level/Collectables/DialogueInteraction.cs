using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DialogueInteraction : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GenericDialog dialog = GenericDialog.Instance();
        dialog.SetTitle( "Hello player!" );
        dialog.SetMessage( "Do you want to exchange some gems for 100 gold?" );
        dialog.SetOnAccept( "Yes", () => { // define what happens when user clicks Yes:
            dialog.Hide();
        } );

        dialog.SetOnDecline( "No thanks", () => dialog.Hide() );
        dialog.Show();


    }


}
