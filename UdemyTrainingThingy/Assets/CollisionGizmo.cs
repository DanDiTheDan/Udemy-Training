﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGizmo : MonoBehaviour
{

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}
