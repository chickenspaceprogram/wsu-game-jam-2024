using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyWaves : MonoBehaviour
{

    float time = 0;

    public GameObject sniperPrefab;
    public GameObject runnerPrefab;
    public GameObject gunnerPrefab;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (time > 2  && GameObject.FindGameObjectsWithTag("Enemy").Length < 10) 
        {
            int ic = Random.Range(1, 3);

            Vector3 pos = new Vector3(Random.Range(-22, 16), 2f, Random.Range(1767, 1821));

            switch (ic)
            {
                case 1:
                    Instantiate(sniperPrefab, pos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(runnerPrefab, pos, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(gunnerPrefab, pos, Quaternion.identity);
                    break;

            }
            time = 0;
        }
        time += Time.deltaTime;
    }
}
