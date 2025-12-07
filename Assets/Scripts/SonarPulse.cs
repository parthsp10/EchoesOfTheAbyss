using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPulse : MonoBehaviour
{
    public KeyCode pingKey = KeyCode.Q;
    public GameObject sonarWavePrefab;
    public float verticalOffset = 1f;
    public float cooldown = 1.5f;

    private float nextPingTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(pingKey) && Time.time >= nextPingTime)
        {
            if (sonarWavePrefab != null)
            {
                Vector3 spawnPos = transform.position + Vector3.up * verticalOffset;
                Instantiate(sonarWavePrefab, spawnPos, Quaternion.identity);
            }
            nextPingTime = Time.time + cooldown;
        }
    }
}


