using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpeedPickupLogic : MonoBehaviour
{
    private PhotonView view;
    private AudioSource speedSound;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject speedPickupAnimation;
    [SerializeField] private float duration;
    [SerializeField] private float multiplier;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        speedSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.IncreaseSpeed(duration, multiplier);
            view.RPC(nameof(PlaySound), RpcTarget.All);
            view.RPC(nameof(DestroyObject), RpcTarget.All);
            PhotonNetwork.Instantiate(speedPickupAnimation.name, transform.position, Quaternion.identity);
        }
    }
    [PunRPC]
    void DestroyObject() {
        if (PhotonNetwork.IsMasterClient == true) {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    [PunRPC]
    void PlaySound() {
        speedSound.PlayOneShot(clip);
    }
}
