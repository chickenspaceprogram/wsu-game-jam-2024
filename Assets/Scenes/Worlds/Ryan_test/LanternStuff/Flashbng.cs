using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashbng : MonoBehaviour
{
    bool flashing = false;


    public void Flash() 
    { 
        flashing = true;
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (flashing)
        {
            if (other.gameObject.GetComponent<Gunner_enemy>() != null)
            {
                Debug.Log("hi");
                other.gameObject.GetComponent<Gunner_enemy>().flashed = true;
            }
            if (other.gameObject.GetComponent<Seeker_controller>() != null)
            {
                Debug.Log("hi");
                other.gameObject.GetComponent<Seeker_controller>().flashed = true;
            }
        }
    }


}
