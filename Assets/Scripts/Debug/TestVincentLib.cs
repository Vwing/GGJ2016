using UnityEngine;
using System.Collections;
using VincentLib;

public class TestVincentLib : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        Debug.Log("10 divided by 2 is " + TestFunctions.TestDivide(10, 2));
        Debug.Log("10 times 2 is " + TestFunctions.TestMultiply(10, 2));
        Debug.Log("str: " + TestFunctions.GetString());
    }
}
