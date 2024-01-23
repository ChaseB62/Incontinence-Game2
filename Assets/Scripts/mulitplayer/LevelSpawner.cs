using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] mapPrefabs; // Array of map prefabs
    public GameObject lobby;
    private GameObject currentMap;   // Reference to the currently spawned map
    private int currentIndex = 0;   // Index of the currently spawned map

    public float timeToNextMap = 300f;

    public GameObject music;

    public LayerMask gunLayer;

    public LayerMask playerLayer;

    // Collider2D variable to detect interactions
    public Collider2D interactionCollider;

    public PhotonView photonView;

    // Function to spawn the next map
    [PunRPC]
    public void NextMap()
    {
        // If a map is already spawned, destroy it
        if (currentMap != null)
        {
            Destroy(currentMap);
            PhotonNetwork.RemoveBufferedRPCs();
        }

        if(lobby != null){
            Destroy(lobby);
            interactionCollider.enabled = false;
        }

        // Find all objects on the specified layer
        Collider2D[] objectsOnLayer = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(1000, 1000), 0, gunLayer);
        Collider2D[] playersOnLayer = Physics2D.OverlapBoxAll(Vector2.zero, new Vector2(1000, 1000), 0, playerLayer);

        // Destroy each object on the layer
        foreach (Collider2D obj in objectsOnLayer)
        {
            Destroy(obj.gameObject);
        }

        foreach (Collider2D obj in playersOnLayer)
        {
            obj.transform.position = new Vector3(0, 0, 0);
        }

        // Spawn the next map prefab
        currentMap = Instantiate(mapPrefabs[currentIndex], Vector3.zero, Quaternion.identity);

        // Increment the index for the next map (looping back to the start if necessary)
        currentIndex = (currentIndex + 1) % mapPrefabs.Length;
    
        StartCoroutine(Timer());
    }

    // OnTriggerEnter2D is called when the Collider2D enters a trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the interacting object has the tag "Player"
        if (other.CompareTag("canEffect"))
        {
            Debug.Log("Touched");
            music.SetActive(true);
            // Call the NextMap() function
            photonView.RPC("NextMap", RpcTarget.AllBuffered);
        }
    }

    IEnumerator Timer(){
        yield return new WaitForSeconds(timeToNextMap);
        
        photonView.RPC("NextMap", RpcTarget.AllBuffered);
    }
}
