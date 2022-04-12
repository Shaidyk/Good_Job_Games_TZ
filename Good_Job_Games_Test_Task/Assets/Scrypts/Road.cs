using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject player;
    GameObject road;
    void Start()
    {
        road = this.gameObject;
        Debug.Log(road.transform.localScale.z);
    }

    
    void Update()
    {
        road.transform.localScale = new Vector3(road.transform.localScale.x, road.transform.localScale.y, player.transform.localScale.z);
    }
}
