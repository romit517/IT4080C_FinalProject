using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private TextMeshProUGUI healthDisplay;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private AudioSource loopingTrack;
    private PhotonView view;
    private void Start() {
        view = GetComponent<PhotonView>();
    }
    public void TakeDamage() {
        view.RPC(nameof(TakeDamageRPC), RpcTarget.All);
    }
    public void AddHealth() {
        view.RPC(nameof(AddHealthRPC), RpcTarget.All);
    }

    [PunRPC]
    void TakeDamageRPC() {
        health--;
        if(health <= 0) {
            gameOver.SetActive(true);
            loopingTrack.Stop();
        }

        healthDisplay.text = health.ToString();
    }
    [PunRPC]
    void AddHealthRPC() {
        health++;
        healthDisplay.text = health.ToString();
    }
}
