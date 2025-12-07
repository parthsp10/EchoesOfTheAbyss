using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.collectedRelics >= GameManager.Instance.totalRelics)
        {
            GameManager.Instance.WinGame();
        }
        else
        {
            Debug.Log("You need more relics before you can escape!");
        }
    }
}

