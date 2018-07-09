using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Img : MonoBehaviour {
    [SerializeField]
    Material mat;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
