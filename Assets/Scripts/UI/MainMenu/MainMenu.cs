using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text controlsText;  
    [SerializeField] private string mainSceneName = "MainScene";  
    [SerializeField] private Button startButton;  
    [SerializeField] private Button controlsButton;  
    [SerializeField] private Button quitButton;
    private bool controlsActive = false;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        controlsButton.onClick.AddListener(OnControlsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
        controlsText.gameObject.SetActive(false);
        controlsActive = false;
    }

    
    void OnStartClicked()
    {
        SceneManager.LoadScene(mainSceneName); 
    }

    void OnControlsClicked()
    {
        if (controlsActive == false)
        {
            controlsText.gameObject.SetActive(true);
            controlsActive = true;
        }
        else
        {
            controlsActive = false;
            controlsText.gameObject.SetActive(false);
        }
    }

    void OnQuitClicked()
    {
        Application.Quit(); 
    }
}
