using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationSettings {

	public static void SetTargetFrameRate()
    {
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
    }
}
