using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startPage;
    public GameObject optionsPage;
    public GameObject howToPlayPage;
    public GameObject creditsPage;
    private bool muted = false;
    public TextMeshProUGUI muteButton;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showOptions()
    {
        startPage.SetActive(false);
        howToPlayPage.SetActive(false);
        creditsPage.SetActive(false);
        optionsPage.SetActive(true);
    }
    public void showStart()
    {
        startPage.SetActive(true);
        howToPlayPage.SetActive(false);
        creditsPage.SetActive(false);
        optionsPage.SetActive(false);
    }

    public void showHowToPlay()
    {
        startPage.SetActive(false);
        howToPlayPage.SetActive(true);
        creditsPage.SetActive(false);
        optionsPage.SetActive(false);
    }
    public void showCredits()
    {
        startPage.SetActive(false);
        howToPlayPage.SetActive(false);
        creditsPage.SetActive(true);
        optionsPage.SetActive(false);
    }

    public void toggleAudio()
    {

        muted = !muted;
        if (muted)
            muteButton.text = "Unmute Sound";
        else
        {
            muteButton.text = "Mute Sound";
        }
        AudioListener.pause = muted;
    }
}
