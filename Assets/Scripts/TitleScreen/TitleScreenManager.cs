using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public string sceneName;
    [Range(0.3f, 2.0f)]
    public float fadeOutSpeed = 5.0f;
    
    private GameObject fadeImage;
    private float fadeValue;
    private bool fading;

    void Start()
    {
        GameObject canvas = GameObject.Find("Title Screen UI");
        fadeImage = canvas.transform.GetChild(canvas.transform.childCount - 1).gameObject;
        fadeValue = 0.0f;
        fading = false;
    }

    void Update()
    {
        if(!fading)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                fading = true;
            }
        }
        else
        {
            fadeValue += Time.deltaTime * fadeOutSpeed;
            fadeImage.GetComponent<Image>().color = new Color(0, 0, 0, fadeValue);
            if(fadeValue >= 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
