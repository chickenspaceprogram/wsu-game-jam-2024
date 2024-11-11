using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class port_to_two : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        GameObject.Find("Lantern").GetComponent<Shotgun>().enabled = false;
        GameObject.Find("Lantern_follower").GetComponent<Shotgun>().enabled = true;
        GameObject.Find("RedCamera").GetComponent<Camera>().enabled = false;
        Quaternion qc = GameObject.Find("RedCamera").transform.rotation;
        qc.x = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            GameObject baller = GameObject.Find("Player").gameObject;

            baller.transform.position = new Vector3(0, 2f, 1798f);

            Camera new_cam = GameObject.Find("RedCamera").GetComponent<Camera>();

            Character_controller cc = baller.GetComponent<Character_controller>();

            HealthBar hb = baller.GetComponent<HealthBar>();

            new_cam.enabled = true;

            cc.player_cam = new_cam;
            hb.cam = new_cam;

            GameObject ln = GameObject.Find("Lantern");

            ln.transform.SetParent(new_cam.transform);

            Destroy(baller.transform.GetChild(0).GameObject());

            new_cam.transform.SetParent(baller.transform);

            new_cam.tag = "MainCamera";

            ln.transform.localPosition = new Vector3(-0.53f, -0.25f, 0.82f);
            ln.transform.localRotation = Quaternion.identity;

            GameObject.Find("Lantern").GetComponent<Shotgun>().enabled = true;
            GameObject.Find("Lantern_follower").GetComponent<Shotgun>().enabled = false;

            Destroy(gameObject);
        }
   
    }
}
