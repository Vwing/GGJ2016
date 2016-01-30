/*============================================================================== 
 * Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using System.Collections;
using Vuforia;

public class InitErrorHandler : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES
    public Canvas errorCanvas;
    public UnityEngine.UI.Text errorTitle;
    public UnityEngine.UI.Text errorText;
    #endregion // PUBLIC_MEMBER_VARABLES


    #region MONOBEHAVIOUR_METHODS
    void Awake () 
    {
        VuforiaAbstractBehaviour vuforia = FindObjectOfType<VuforiaAbstractBehaviour>();
        vuforia.RegisterVuforiaInitErrorCallback(OnInitError);
	}
    #endregion // MONOBEHAVIOUR_METHODS


    #region PRIVATE_METHODS
    private void OnInitError(VuforiaUnity.InitError error)
    {
        if (error != VuforiaUnity.InitError.INIT_SUCCESS)
        {
            ShowErrorMessage(error);
        }
    }

    private void ShowErrorMessage(VuforiaUnity.InitError errorCode)
    {
        switch (errorCode)
        {
            case VuforiaUnity.InitError.INIT_EXTERNAL_DEVICE_NOT_DETECTED:
                errorTitle.text = "Initialization error";
                errorText.text =
                    "Failed to initialize Vuforia because this " +
                    "device is not docked with required external hardware.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_MISSING_KEY:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "Vuforia App key is missing. \n" + 
                    "Please recompile the Application with a valid key.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_INVALID_KEY:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "Vuforia App key is invalid. \n" +
                    "Please recompile the Application with a valid key.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_NO_NETWORK_TRANSIENT:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "Unable to contact server. Please try again later.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_NO_NETWORK_PERMANENT:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "No network available. Please make sure you are connected to the internet.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_CANCELED_KEY:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "This App license key has been cancelled " +
                    "and may no longer be used. Please get a new license key.";
                break;
            case VuforiaUnity.InitError.INIT_LICENSE_ERROR_PRODUCT_TYPE_MISMATCH:
                errorTitle.text = "License Key Error";
                errorText.text =
                    "Vuforia App key is not valid for this product. Please get a valid key, " +
                    "by logging into your account at developer.vuforia.com and choosing the " +
                    "right product type during project creation";
                break;
#if (UNITY_IPHONE || UNITY_IOS)
                case VuforiaUnity.InitError.INIT_NO_CAMERA_ACCESS:
                    errorTitle.text = "Initialization error";
                    errorText.text = 
                        "Camera Access was denied to this App. \n" + 
                        "When running on iOS8 devices, \n" + 
                        "users must explicitly allow the App to access the camera.\n" + 
                        "To restore camera access on your device, go to: \n" + 
                        "Settings > Privacy > Camera > [This App Name] and switch it ON.";
                    break;
#endif
            case VuforiaUnity.InitError.INIT_DEVICE_NOT_SUPPORTED:
                errorTitle.text = "Initialization error";
                errorText.text =
                    "Failed to initialize Vuforia because this device is not " +
                    "supported.";
                break;
            case VuforiaUnity.InitError.INIT_ERROR:
                errorTitle.text = "Initialization error";
                errorText.text = "Failed to initialize Vuforia.";
                break;
        }

        Debug.Log(errorText.text);

        if (errorCanvas)
        {
            // Show the error message UI canvas
            errorCanvas.gameObject.SetActive(true);
            errorCanvas.enabled = true;
        }
    }
    #endregion // PRIVATE_METHODS
}
