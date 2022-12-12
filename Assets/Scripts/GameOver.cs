using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject waitingText;
    private PhotonView view;
    private bool shouldPause;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if(PhotonNetwork.IsMasterClient == false) {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }
    public void OnClickRestart() {
        view.RPC(nameof(Restart), RpcTarget.All);
    }

    [PunRPC]
    void Restart() {
        PhotonNetwork.LoadLevel("Game Level");
    }
}
