using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCongratulationController : MonoBehaviour {
    public Animator AnimatorProperty;
    // Use this for initialization
    private static AnimatedCongratulationController animatedCongratulationController;
    #region Singleton
    public static AnimatedCongratulationController instance;
    private void Awake() {
        instance = this;
    }
    #endregion



    public static AnimatedCongratulationController Instance() {
        if (!animatedCongratulationController) {
            animatedCongratulationController = FindObjectOfType( typeof( AnimatedCongratulationController ) ) as AnimatedCongratulationController;
            if (!animatedCongratulationController)
                Debug.LogError( "There needs to be one active ObjectiveManager script on a GameObject in your scene." );
        }

        return animatedCongratulationController;
    }
    public void Animate () {
        AnimatorProperty.SetTrigger( "ActiveAnimation" );
    }
	
}
