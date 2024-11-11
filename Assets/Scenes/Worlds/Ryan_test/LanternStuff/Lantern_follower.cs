using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern_follower : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject target;
    [SerializeField] private float degreesPerSecond = 45;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), degreesPerSecond * Time.deltaTime);
    }
}
