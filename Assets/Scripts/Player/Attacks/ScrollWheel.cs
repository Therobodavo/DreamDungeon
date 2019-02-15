using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWheel : MonoBehaviour {

    //Array of possible attacks, will change
  public  string[] attackList;
    public string currentAttack;
   public float index;
    public const float orthographicSizeMin = 0.15f;
    public const float orthographicSizeMax = 100;
    public float scrollSpeed;
    private float zoomPos = .5f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //changing zoomposition with scroll wheel
        zoomPos -= scrollSpeed * (Input.GetAxis("Mouse ScrollWheel"));

        //clamping zoomposition
        zoomPos = Mathf.Clamp(zoomPos, orthographicSizeMin, 1f);

        //changing camera size 
       index = Mathf.Lerp(index, orthographicSizeMax * zoomPos, .07f);


        if(index > attackList.Length)
        {
            index = 0;
        }

        currentAttack = attackList[Mathf.RoundToInt(index)];
     
    }
}
