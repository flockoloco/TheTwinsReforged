﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector2 spawnP;

    private void Awake()
    {
        spawnP = transform.position;
    }
}
