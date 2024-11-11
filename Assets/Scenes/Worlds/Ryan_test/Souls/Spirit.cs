using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    float time = 0;

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Light>().intensity = Mathf.Sin(time) + 1; 

        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(string.Equals(other.gameObject.name, "Player"))
        {
            Character_controller cc = other.gameObject.GetComponentInChildren<Character_controller>();
            cc.MAX_STAMINA += 1;
            cc.stamina = cc.MAX_STAMINA;
            other.gameObject.GetComponent<Health>().Heal(100);

            Destroy(gameObject);
        }
        
    }
}
