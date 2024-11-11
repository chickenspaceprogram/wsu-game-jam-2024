using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Damage_box : MonoBehaviour{
    // Start is called before the first frame update

    public double damage = 1;
    public bool destroyOnHit = true;
    public GameObject parent = null;

    void OnCollisionEnter(Collision collision) { 
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<Health>() != null && collision.gameObject != GameObject.Find("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        if (destroyOnHit)
        {
            if (parent != null)
            {
                if (collision.gameObject != parent || collision.gameObject != GameObject.Find("Player"))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
