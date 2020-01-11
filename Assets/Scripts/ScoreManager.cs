using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject playerToRespawn;
    public Transform spawnPoint;

    public void StartRespawn()
    {
        Invoke("Respawn", 3.0f);
    }

    public void Respawn()
    {
        GameObject newPlayer = Instantiate(playerToRespawn, spawnPoint.position, spawnPoint.rotation);
        Player playerScript = newPlayer.GetComponent<Player>();
        playerScript.StartBlink();
    }
}
