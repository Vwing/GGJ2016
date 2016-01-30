/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;

public class BlackMaskBehaviour : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES
    private float mFadeFactor = 0;
    #endregion //PRIVATE_MEMBER_VARIABLES


    #region MONOBEHAVIOUR_METHODS
    void Start()
    {
        mFadeFactor = 0;
    }

    void Update()
    {
        Camera cam = Vuforia.VuforiaBehaviour.Instance.PrimaryCamera;
        if (cam == null)
        {
            Debug.LogError("Vuforia Left eye (Primary) camera not set; please verify your camera rig setup.");
            return;
        }
            
        // Update black mask position at near clip plane
        float near = cam.nearClipPlane;
        float fovX = 2.0f * Mathf.Atan(1.0f / cam.projectionMatrix[0, 0]);
        float fovY = 2.0f * Mathf.Atan(1.0f / cam.projectionMatrix[1, 1]);

        this.transform.localPosition = 1.05f * Vector3.forward * near;
        this.transform.localScale = new Vector3(4.0f * near * Mathf.Tan(fovX / 2), 4.0f * near * Mathf.Tan(fovY / 2), 1);

        // Update black mask transparency
        // black mask becomes fully opaque (black) at half transition (0.5)
        // then, beyond 0.5, the black mask plane gradually becomes transparent again (until 1.0).
        Renderer blackMaskRenderer = this.GetComponent<Renderer>();
        blackMaskRenderer.enabled = (mFadeFactor > 0.01f && mFadeFactor < 0.99f);

        float alpha = (mFadeFactor < 0.5f) ? (2 * mFadeFactor) : (1 - 2 * (mFadeFactor - 0.5f));
        blackMaskRenderer.material.SetFloat("_Alpha", alpha);
    }
    #endregion MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS
    public void SetFadeFactor(float tf)
    {
        mFadeFactor = Mathf.Clamp01(tf);
    }
    #endregion // PUBLIC_METHODS
}