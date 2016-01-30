/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashManager : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    private RawImage mSplashImage;
    private Texture2D[] mSplashTextures = new Texture2D[4];
    #endregion // PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        mSplashTextures[0] = Resources.Load("SplashScreen/SplashLandscape1") as Texture2D;
        mSplashTextures[1] = Resources.Load("SplashScreen/SplashLandscape2") as Texture2D;
        mSplashTextures[2] = Resources.Load("SplashScreen/SplashLandscape3") as Texture2D;
        mSplashTextures[3] = Resources.Load("SplashScreen/SplashLandscape4") as Texture2D;

        mSplashImage = FindObjectOfType<RawImage>();
        mSplashImage.texture = PickImageWithBestAspect(mSplashTextures);

        StartCoroutine(LoadNextSceneAfter(5));
    }
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private IEnumerator LoadNextSceneAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Application.LoadLevel(Application.loadedLevel+1);

        // Unload splash images 
        foreach (var tex in mSplashTextures)
        {
            Resources.UnloadAsset(tex);
        }
    }

    private Texture2D PickImageWithBestAspect(Texture2D[] splashImages)
    {
        Texture2D bestImage = splashImages[0];
        float minAspectDiff = 2; //Maximum value
        foreach (Texture2D image in splashImages)
        {
            float imageAspect = image.width / (float)image.height;
            float screenAspect = Screen.width / (float)Screen.height;
            if (Mathf.Abs(imageAspect - screenAspect) < minAspectDiff)
            {
                minAspectDiff = Mathf.Abs(imageAspect - screenAspect);
                bestImage = image;
            }
        }
        return bestImage;
    }
    #endregion // PRIVATE_METHODS
}