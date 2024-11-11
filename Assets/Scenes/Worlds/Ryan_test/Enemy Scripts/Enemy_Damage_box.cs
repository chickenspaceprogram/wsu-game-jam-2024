using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage_box : MonoBehaviour{
    // Start is called before the first frame update

    public double damage = 1;
    public bool destroyOnHit = false;
    public GameObject parent = null;

    void OnCollisionEnter(Collision collision) { 
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<Health>().TakeDamage(damage);
        }
        if (destroyOnHit)
        {
            if (parent != null)
            {
                if (collision.gameObject != parent)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
