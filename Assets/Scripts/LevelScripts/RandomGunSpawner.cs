using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RandomGunSpawner : MonoBehaviour
{
    //chabes
    public PhotonView photonView;

    public GameObject[] objectsToSpawn;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            SyncSpawnedObjectIndex(randomIndex);
        }
    }
    private void SyncSpawnedObjectIndex(int index)
    {
        Debug.Log("Spawning");
        int clampedIndex = Mathf.Clamp(index, 0, objectsToSpawn.Length - 1);

        GameObject _gun = PhotonNetwork.Instantiate(objectsToSpawn[clampedIndex].name, transform.position, Quaternion.identity);
        if(_gun != null){
            Debug.Log("Spawned");
        } else {
            Debug.LogError("FUCK");
        }
        //Instantiate(objectsToSpawn[clampedIndex], transform.position, Quaternion.identity);
    }
}
