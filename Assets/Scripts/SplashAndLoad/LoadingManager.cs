/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingManager : MonoBehaviour 
{
    #region PRIVATE_MEMBER_VARIABLES
    private bool mChangeLevel = true;
    private RawImage mUISpinner;
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start () 
    {
        mUISpinner = FindSpinnerImage();
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        mChangeLevel = true;
	}

	void Update () 
    {
        mUISpinner.rectTransform.Rotate(Vector3.forward, 90.0f * Time.deltaTime);

        if (mChangeLevel)
        {
            LoadARSceneAsync();
            mChangeLevel = false;
        }
	}
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void LoadARSceneAsync()
    {
        Application.LoadLevelAsync("Vuforia-3-AR-VR");
    }

    private RawImage FindSpinnerImage()
    {
        RawImage[] images = FindObjectsOfType<RawImage>();
        foreach (var img in images)
        {
            if (img.name.Contains("Spinner"))
            {
                return img;
            }
        }
        return null;
    }
    #endregion // PRIVATE_METHODS
}
