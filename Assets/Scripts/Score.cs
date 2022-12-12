using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Score : MonoBehaviour
{
    public int score { get; private set; }
    private PhotonView view;
    [SerializeField] private TextMeshProUGUI scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void AddScore() {
        view.RPC(nameof(AddScoreRpc), RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRpc() {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
