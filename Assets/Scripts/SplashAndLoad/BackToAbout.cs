/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;

public class BackToAbout : MonoBehaviour
{
    #region MONOBEHAVIOUR_METHODS
    void Update()
    {
#if UNITY_ANDROID
        // On Android, the Back button is mapped to the Esc key
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Go back to the About page
            Application.LoadLevel("Vuforia-1-About");
        }
#endif
    }
    #endregion // MONOBEHAVIOUR_METHODS
}
