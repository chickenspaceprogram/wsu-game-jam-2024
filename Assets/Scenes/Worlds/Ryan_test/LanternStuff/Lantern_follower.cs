using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Lantern_follower : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject target;
    [SerializeField] private float degreesPerSecond = 45;
    float time = 0;
    Light light;
    bool flashbang = false;
    public bool canflash = true;

    public float initialIntensity = 2;
    public float initialRange = 10;


    private void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && canflash){
            BroadcastMessage("Flash");
            flashbang = true;
            GameObject.Find("Player").GetComponent<Health>().TakeDamage(2);
            time = 0;
        }

        if (flashbang)
        {
            time += Time.deltaTime;
            if(time > 1)
            {
                flashbang = false;
            }
            Flashbang();
        }
        if (!flashbang)
        {
            light.intensity = initialIntensity;
            light.range = initialRange;
        }

        //transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), degreesPerSecond * Time.deltaTime);

        
    }

    void Flashbang()
    {
        
        light.intensity = 4.32f;
        light.range = 23.65f; 
    }

    
}
