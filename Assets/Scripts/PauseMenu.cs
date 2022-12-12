using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private PhotonView view;
    private Image[] images;
    private TextMeshProUGUI[] texts;
    private bool menuEnabled;

    public bool MenuEnabled {
        get { return menuEnabled; }
        private set { menuEnabled = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        DisableImagesAndTexts();
        print("Disabled");
    }

    public void OnClickRestart() {
        view.RPC(nameof(Restart), RpcTarget.All);
    }
    [PunRPC]
    void Restart() {
        PhotonNetwork.LoadLevel("Game Level");
    }
    public void QuitGame() {
        print("Quit");
        Application.Quit();
    }
    public void MainMenu() {
        view.RPC(nameof(GoToMainMenu), RpcTarget.All);
    }
    [PunRPC]
    void GoToMainMenu() {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public void DisableImagesAndTexts() {
        foreach (Image img in images) {
            img.enabled = false;
        }
        foreach (TextMeshProUGUI text in texts) {
            text.enabled = false;
        }
        menuEnabled = false;
    }
    public void EnableImagesAndTexts() {
        foreach (Image img in images) {
            img.enabled = true;
        }
        foreach (TextMeshProUGUI text in texts) {
            text.enabled = true;
        }
        StartCoroutine(EnableMenuCo());
    }
    IEnumerator EnableMenuCo() {
        yield return new WaitForSeconds(0.1f);
        menuEnabled = true;
    }
}
