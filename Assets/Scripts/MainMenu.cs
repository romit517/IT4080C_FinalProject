using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject roomCreation;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private AudioSource menuMusic;

    [SerializeField] private TMP_InputField createInput, joinInput, nameInput;


    public void CreateRoom() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel("Game Level");
    }

    public void ChangeName() {
        PhotonNetwork.NickName = nameInput.text;
    }
    public void EnableRoomCreation() {
        mainMenu.SetActive(false);
        roomCreation.SetActive(true);
    }
    public void EnableMainMenu() {
        roomCreation.SetActive(false);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        menuMusic.UnPause();
    }
    public void QuitGame() {
        Application.Quit();
        print("QuitGame");
    }
    public void EnableSettings() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void EnableCredits() {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
        menuMusic.Pause();
    }
}


