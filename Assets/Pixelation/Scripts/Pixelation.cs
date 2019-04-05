using Assets.Pixelation.Example.Scripts;
using UnityEngine;

namespace Assets.Pixelation.Scripts
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Pixelation")]
    public class Pixelation : ImageEffectBase
    {
        [Range(64.0f, 512.0f)]
        public float BlockCount = 128;
        [Range(0.001f, 1.0f)]
        public float pixelInSpeed = .5f;

        private float blockCountActual = 0.0f;



        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            blockCountActual = Mathf.Lerp(blockCountActual, BlockCount, pixelInSpeed);

            float k = Camera.main.aspect;
            Vector2 count = new Vector2(Mathf.Floor(blockCountActual), Mathf.Floor(blockCountActual) / k);
            Vector2 size = new Vector2(1.0f/count.x, 1.0f/count.y);
            //
            material.SetVector("BlockCount", count);
            material.SetVector("BlockSize", size);
            Graphics.Blit(source, destination, material);
        }
    }
}