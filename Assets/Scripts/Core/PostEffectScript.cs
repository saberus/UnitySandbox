using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectScript : MonoBehaviour
{
    public Material mat;


    void Update()
    {
        Debug.Log("AAAAAAAAA");
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //src is the fully rendered scene that you normally send to the monitore but we intercepting it 
        //to do more work before passing it on.

        Graphics.Blit(src, dest, mat);
    }


}
