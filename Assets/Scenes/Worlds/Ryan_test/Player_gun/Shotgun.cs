using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public bool enabled = true;
    
    
    float time = 0;
    public float timeBetweenShots = 0.5f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    int shots = 0;
    public float bullet_speed = 32f;



    public int spread = 20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            time += Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                if (!alreadyAttacked)
                {
                    //Vector3 forward = transform.forward;
                    //Vector3 right = transform.right;

                    if (time > timeBetweenShots)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Rigidbody rb = Instantiate(projectile, GameObject.Find("Lantern").transform.position,
                                Quaternion.identity).GetComponent<Rigidbody>();

                            rb.velocity = PickFiringDirection(rb.transform.forward, spread);
                            rb.AddForce(transform.forward * bullet_speed, ForceMode.Impulse);
                            rb.AddForce(transform.up * 3f, ForceMode.Impulse);
                            rb.GetComponent<DestroyAfterTIme>().destructable = true;
                            rb.GetComponent<Player_Damage_box>().parent = gameObject;
                        }

                        shots++;
                        time = 0;
                    }

                    if (shots > 3)
                    {
                        alreadyAttacked = true;
                        Invoke(nameof(ResetAttack), timeBetweenAttacks);
                    }
                }
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        shots = 0;
    }


    Vector3 PickFiringDirection(Vector3 muzzleForward, float spreadRadius)
    {
        Vector3 candidate = Random.insideUnitSphere * spreadRadius + muzzleForward;
        return candidate.normalized;
    }


}
