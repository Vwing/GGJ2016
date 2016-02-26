using UnityEngine;
using System.Collections;

public class ResizeChildren : MonoBehaviour {
    public float ScaleMultiplier = 2f;
    [ContextMenu("Resize all children by scale multiplier")]
    void ResizeAll()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        int len = children.Length;
        for (int i = 1; i < len; ++i) //starts at 1 to skip parent.
            children[i].localScale *= ScaleMultiplier;
    }
}
