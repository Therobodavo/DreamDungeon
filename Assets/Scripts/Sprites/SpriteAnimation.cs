using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public bool sprite;
    public Texture firstTexture;
    public Texture secondTexture;
    public Sprite firstSprite;
    public Sprite secondSprite;
    [Range(0.01f, 2.0f)]
    public float speed = 1.0f;

    private float counter = 0.0f;
    private bool frame = false;

    void Update()
    {
        

        counter += Time.deltaTime;
        if(counter > speed)
        {
            counter = 0.0f;
            frame = !frame;
            Texture tex;
            Sprite spr;
            tex = (frame) ? firstTexture : secondTexture;
            spr = (frame) ? firstSprite : secondSprite;
            if (sprite)
            {
                this.GetComponent<SpriteRenderer>().sprite = spr;
            }
            else
            {
                this.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", tex);
            }
        }
    }
}
