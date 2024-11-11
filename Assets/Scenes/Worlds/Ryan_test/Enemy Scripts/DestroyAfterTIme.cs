using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTIme : MonoBehaviour
{
    // Start is called before the first frame update
    public bool destructable = false;
    public int destroytime = 5;

    float time = 0;

    private void Awake()
    {
        time = 0;
    }


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > destroytime && destructable)
        {
            Destroy(gameObject);
        }
    }
}
