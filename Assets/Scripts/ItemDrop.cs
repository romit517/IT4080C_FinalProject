using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject hearthDrop;
    [SerializeField] private GameObject speedDrop;
    private int chanceToDropHeart;
    private int chanceToDropSpeed;
    [SerializeField] private int dropPercentageHeart;
    [SerializeField] private int dropPercentageSpeed;
    [SerializeField] private bool shouldDropHeart, shouldDropSpeed;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("GoldenRay") && shouldDropHeart){
                chanceToDropHeart = Random.Range(1, 100);
                if(chanceToDropHeart <= dropPercentageHeart) {
                    PhotonNetwork.InstantiateRoomObject(hearthDrop.name, transform.position, Quaternion.identity);
                }
            }
        if(collision.CompareTag("GoldenRay") && shouldDropSpeed) {
            chanceToDropSpeed = Random.Range(1, 100);
            if(chanceToDropSpeed <= dropPercentageSpeed) {
                PhotonNetwork.InstantiateRoomObject(speedDrop.name, transform.position, Quaternion.identity);
            }
        }
        }
        
    }

