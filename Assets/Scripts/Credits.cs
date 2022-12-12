using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private float startTime;
    private float actualTime;
    private MainMenu mainMenu;
    [SerializeField]private AudioSource creditsMusic;
    // Start is called before the first frame update
    void OnEnable()
    {
        startTime = 20.0f;
        actualTime = startTime;
        mainMenu = FindObjectOfType<MainMenu>();
        creditsMusic.UnPause();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime -= Time.deltaTime;
        if(actualTime <= 0) {
            mainMenu.EnableMainMenu();
            creditsMusic.Pause();
        }
    }
}
