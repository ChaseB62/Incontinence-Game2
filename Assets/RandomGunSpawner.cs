using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RandomGunSpawner : MonoBehaviour
{
    public PhotonView photonView; // Store a reference to the PhotonView

    public GameObject[] objectsToSpawn; // Array of GameObjects to choose from

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // If it's the local player, choose a random object and sync the index
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            SyncSpawnedObjectIndex(randomIndex);
        }
    }
    private void SyncSpawnedObjectIndex(int index)
    {
        Debug.Log("Spawning");
        // Set the synchronized index for all players
        int clampedIndex = Mathf.Clamp(index, 0, objectsToSpawn.Length - 1);

        GameObject _gun = PhotonNetwork.Instantiate(objectsToSpawn[clampedIndex].name, transform.position, Quaternion.identity);
        // Instantiate the chosen object for remote players
        //Instantiate(objectsToSpawn[clampedIndex], transform.position, Quaternion.identity);
    }
}
