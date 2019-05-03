using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public string nextSceneName;
    public Sprite[] cutsceneImages;
    public float fadeSpeed;
    public float keepSpeed;
    [Range(0.0f, 1.0f)]
    public float resizeSpeed;

    private float fadeAmt;
    private float keepTimer;
    private GameObject fadeReference;
    private GameObject imageReference;
    private int currentImg;
    private CutsceneStates cutsceneState;
    private enum CutsceneStates
    {
        FadingIn,
        Staying,
        FadingOut
    }

    void Start()
    {
        fadeAmt = 1.0f;
        fadeReference = null;
        currentImg = 0;
        GameObject cutsceneCanvas = GameObject.Find("Cutscene Canvas");
        if(cutsceneCanvas)
        {
            fadeReference = cutsceneCanvas.transform.GetChild(cutsceneCanvas.transform.childCount - 1).gameObject;
            imageReference = cutsceneCanvas.transform.GetChild(0).gameObject;
        }
    }

    void Update()
    {
        if(fadeReference)
        {
            switch (cutsceneState)
            {
                case (CutsceneStates.FadingIn):
                    fadeAmt -= fadeSpeed * Time.deltaTime;
                    fadeReference.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, fadeAmt);
                    if (fadeAmt <= 0.0f)
                        cutsceneState = CutsceneStates.Staying;
                break;

                case (CutsceneStates.Staying):
                    keepTimer += Time.deltaTime;
                    if(keepTimer >= keepSpeed)
                    {
                        cutsceneState = CutsceneStates.FadingOut;
                        keepTimer = 0.0f;
                    }
                break;

                case (CutsceneStates.FadingOut):
                    fadeAmt += fadeSpeed * Time.deltaTime;
                    fadeReference.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, fadeAmt);
                    if (fadeAmt >= 1.0f)
                    {
                        cutsceneState = CutsceneStates.FadingIn;
                        currentImg++;
                        if(currentImg < cutsceneImages.Length)
                        {
                            imageReference.GetComponent<Image>().sprite = cutsceneImages[currentImg];
                        }
                        else
                        {
                            if(nextSceneName != "")
                                SceneManager.LoadScene(nextSceneName);
                        }
                    }
                break;
            }
        }
    }
}
