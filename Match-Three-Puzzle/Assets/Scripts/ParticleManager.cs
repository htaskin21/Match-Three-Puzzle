﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject clearFXPrefab;
    public GameObject breakFXPprefab;
    public GameObject doubleBreakFXPrefab;
    public GameObject bombFXPrefab;

    public void ClearPieceFXAt(int x, int y, int z = 0)
    {
        if (clearFXPrefab != null)
        {
            GameObject clearFX = Instantiate(clearFXPrefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            ParticlePlayer particlePlayer = clearFX.GetComponent<ParticlePlayer>();

            if (particlePlayer != null)
            {
                particlePlayer.Play();
            }
        }
    }

    public void BreakTileFXAt(int breakableValue, int x, int y, int z = 0)
    {
        GameObject breakFX = null;
        ParticlePlayer particlePlayer = null;

        if (breakableValue > 1)
        {
            if (doubleBreakFXPrefab != null)
            {
                breakFX = Instantiate(doubleBreakFXPrefab, new Vector3(x, y, z), Quaternion.identity);
            }
        }
        else
        {
            if (breakFXPprefab != null)
            {
                breakFX = Instantiate(breakFXPprefab, new Vector3(x, y, z), Quaternion.identity);
            }
        }

        if (breakFX != null)
        {
            particlePlayer = breakFX.GetComponent<ParticlePlayer>();
            if (particlePlayer != null)
            {
                particlePlayer.Play();
            }
        }
    }

    public void BombFXAt(int x, int y, int z=0)
    {
        if (bombFXPrefab != null)
        {
            GameObject bombFX = Instantiate(bombFXPrefab, new Vector3(x, y, z), Quaternion.identity);
            ParticlePlayer particlePlayer = bombFX.GetComponent<ParticlePlayer>();
            if (particlePlayer!=null)
            {
                particlePlayer.Play();
            }
        }
    }
}
