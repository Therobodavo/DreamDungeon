﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = player.transform.forward;
	}
}