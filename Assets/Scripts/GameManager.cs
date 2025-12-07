using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Relics")]
    public int totalRelics = 3;
    public int collectedRelics = 0;
    public TMP_Text relicText;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject deathPanel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (deathPanel != null) deathPanel.SetActive(false);
        UpdateRelicUI();
        Time.timeScale = 1f;
    }

    public void CollectRelic()
    {
        collectedRelics++;
        UpdateRelicUI();
    }

    void UpdateRelicUI()
    {
        if (relicText != null)
        {
            relicText.text = "Relics: " + collectedRelics + " / " + totalRelics;
        }
    }

    public void WinGame()
    {
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayerDied()
    {
        if (deathPanel != null) deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
