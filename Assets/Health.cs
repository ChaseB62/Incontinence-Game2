using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth = 100f;

    public bool destroyOnDeath = false;

    public void TakeDamage(float damage){
        startHealth -= damage;
        Debug.Log("took " + damage + " points of health");
    }

    public void Update(){
        if(startHealth <= 0 && destroyOnDeath){
            Destroy(gameObject);
        }
    }
    

}
