﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirarParaAcamera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
