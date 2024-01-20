using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject impactEffect;
    
    public string hitTag = "canDamage";

    public float bulletDamage;

    void OnParticleCollision(GameObject other) {
        Debug.Log("Particle was hit!");
        if(other.CompareTag(hitTag)){
            Debug.Log("Hit Tag");
            Health health = other.GetComponent<Health>();
            health.TakeDamage(bulletDamage);
        }
    }
}
