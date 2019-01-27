using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class C_LookAt : MonoBehaviour {

    #region Variables
    public GameObject target;
    #endregion

    void Update ()
    {
        //Make the current transform look at the target
        this.transform.LookAt(target.transform);
	}
}
