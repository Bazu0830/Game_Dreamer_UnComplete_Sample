using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownMap : MonoBehaviour
{
    public string MapInfo;
    public GameObject MapClone;
    [SerializeField]
    private Transform spownpoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mine"))
        {
            GameObject map = (GameObject)Resources.Load(MapInfo);
            MapClone =Instantiate(map, spownpoint.position, spownpoint.rotation);
        }
    }
   
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mine"))
        {
            Destroy(MapClone);
        }
    }
}
