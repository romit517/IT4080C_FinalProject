using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour {

    [SerializeField] private Transform[] spawnPointsBeast;
    [SerializeField] private GameObject beast;
    [SerializeField] private GameObject mole;
    [SerializeField] private float startTimeBtwSpawnsBeast;
    [SerializeField] private float startTimeBtwSpawnsMole;
    [SerializeField] private float minY, maxY, minX, maxX;
    private Vector3 moleSpawnPosition;
    float timeBtwSpawnsBeast;
    [SerializeField] private GameObject moleSpawnVFX;
    public bool shouldSpawn { get; private set; }
    [SerializeField]private GameObject gameOver;

    // Start is called before the first frame update
    void Start() {
        timeBtwSpawnsBeast = startTimeBtwSpawnsBeast;
        StartCoroutine(SpawnMole());
    }

    // Update is called once per frame
    void Update() {
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2 || gameOver.activeSelf) { shouldSpawn = false; return;  }
        shouldSpawn = true;
        SpawnBeasts();
    }
    void SpawnBeasts() {
        if (timeBtwSpawnsBeast <= 0) {
            Vector3 spawnPosition = spawnPointsBeast[Random.Range(0, spawnPointsBeast.Length)].position;
            PhotonNetwork.Instantiate(beast.name, spawnPosition, Quaternion.identity);
            timeBtwSpawnsBeast = startTimeBtwSpawnsBeast;
        } else {
            timeBtwSpawnsBeast -= Time.deltaTime;
        }
    }
    IEnumerator SpawnMole() {
        while (true) {
            if (shouldSpawn) {
                yield return new WaitForSeconds(5f);
                moleSpawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                GameObject moleVfx = PhotonNetwork.Instantiate(moleSpawnVFX.name, moleSpawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(1.0f);
                PhotonNetwork.Instantiate(mole.name, moleSpawnPosition, Quaternion.identity);
                PhotonNetwork.Destroy(moleVfx);
            } else {
                yield return null;
            }
        }
    }
}
        
