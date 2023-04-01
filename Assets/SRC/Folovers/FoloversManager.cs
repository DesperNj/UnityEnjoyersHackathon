using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoloversManager : MonoBehaviour
{
    public GameObject foloverObject;
    public int beatsOfset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int beatsCatched = SoundController.instance.beatsCatched;
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnFolover();
        }
    }
    void SpawnFolover()
    {
        Vector3 offset = Random.insideUnitSphere;

        Vector3 position;
        position.x = transform.position.x + offset.x;
        position.y = transform.position.y;
        position.z = transform.position.z + offset.z;

        Instantiate(foloverObject, position, Quaternion.identity, transform);
    }
}
