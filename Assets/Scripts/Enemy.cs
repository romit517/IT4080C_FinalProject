using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public PlayerController[] players;
    [HideInInspector] public PlayerController nearestPlayer;
    public float speed;
    [HideInInspector]public Animator anim;
    private Score score;
    [HideInInspector] public float distanceOne, distanceTwo;

    [SerializeField] private GameObject deathFX;
    private PhotonView view;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        view = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        anim = GetComponent<Animator>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    protected virtual void Update() {
         distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
         distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        CheckDistance();

    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (PhotonNetwork.IsMasterClient) {
            if (collision.CompareTag("GoldenRay")) {
                score.AddScore();
                view.RPC(nameof(SpawnParticle), RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    public void SpawnParticle() {
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }
    public Vector3 MoveTowards(Transform trans) {
        trans.position = Vector2.MoveTowards(trans.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        return trans.position;
    }
    public void LookTowards(Transform trans) {
        if (nearestPlayer.transform.position.x > trans.position.x) {
            anim.SetBool("shouldWalkLeft", false);
        } else {
            anim.SetBool("shouldWalkLeft", true);
        }
    }
    public void CheckDistance() {
        if (distanceOne < distanceTwo) {
            nearestPlayer = players[0];
        } else {
            nearestPlayer = players[1];
        }
    }
}

