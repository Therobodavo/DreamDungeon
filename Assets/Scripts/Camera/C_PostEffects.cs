using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PostEffects : MonoBehaviour
{
    [Header("Shader Materials")]
    public Material invertMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(true && invertMaterial != null)
        {
            Graphics.Blit(source, destination, invertMaterial);
        }
    }
}
