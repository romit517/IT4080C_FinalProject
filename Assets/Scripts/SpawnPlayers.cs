using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject player1, player2;
    [SerializeField] private float minX, minY, maxX, maxY;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        view = GetComponent<PhotonView>();
        if (view.IsMine) {
            PhotonNetwork.Instantiate(player1.name, randomPosition, Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate(player2.name, randomPosition, Quaternion.identity);
        }
        view.RPC(nameof(StartGame), RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    void StartGame() {
        Time.timeScale = 1;
    }
}
