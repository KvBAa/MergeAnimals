using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cirlcespowner : MonoBehaviour
{
    public GameObject cicle;
    public Transform spownPoint;
    public void Spawn()
    {
        Instantiate(cicle, spownPoint.position, Quaternion.identity);
    }
}
