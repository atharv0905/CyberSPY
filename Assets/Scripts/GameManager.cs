using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float respawnDuration;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void PlayerRespawn()
    {
        StartCoroutine(RespawnPlayer());
    }


    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
