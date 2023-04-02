using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoloversManager : MonoBehaviour
{
    public GameObject foloverObject;
    public int beatsOfset;
    public float OfsetX;
    int beatdCatched = 0;
    public void BeatdCatched()
    {
        beatdCatched++;

        if(beatdCatched >= beatsOfset) {
            beatdCatched = 0;
            SpawnFolover();
        }
    }

    void SpawnFolover()
    {
        Vector3 offset = Random.insideUnitSphere;

        Vector3 position;
        position.x = transform.position.x + offset.x;
        position.y = transform.position.y;
        position.z = transform.position.z + (offset.z * OfsetX);

        Instantiate(foloverObject, position, Quaternion.identity, transform);
        LivesController.instance.IncrementLives();
    }

    public void FoloverGone()
    {
        beatdCatched = 0;

        int count = transform.childCount;

        if (count <= 0) {
            return;
        }

        int index = Random.Range(0, count);
        Transform item = transform.GetChild(index);
        item.GetComponent<FoloverItem>().Gone();
    }
}
