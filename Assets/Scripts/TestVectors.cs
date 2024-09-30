using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVectors : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 v1;
    [SerializeField] private Vector3 v2;

    public Vector3 v3;

    void Start()
    {
        var point1 = Instantiate(prefab, v1, Quaternion.identity);
        var point2 = Instantiate(prefab, v2, Quaternion.identity);
        var distance = Vector3.Distance(point1.transform.position, point2.transform.position);
        var dotProduct = Vector3.Dot(point1.transform.position, point2.transform.position);
        Debug.Log($"dot = {dotProduct} || distance = {distance}");
        if (distance < 10 && dotProduct < 50)
        {
            Debug.Log($"Target in range, Attack!!!");
        }
        WhatAreVectors();
    }

   private void WhatAreVectors()
   { 
    Vector3 vector1 = new Vector3(1, 3, 5);
    //Debug.Log($"{vector1}");
    Vector3 vector2 = new Vector3(3, -5, 7);
    //Debug.Log($"{vector2}");
    Vector3 v1plusv2 = vector1 + vector2;
    //Debug.Log($"{v1plusv2}");
    Vector3 v1minusv2 = vector1- vector2;
    //Debug.Log($"{v1minusv2}");
    Vector3 v2minusv1 = vector2- vector1;
    //Debug.Log($"{v2minusv1}");
    Vector3 v1ttimes4 = vector1 * 4;
    //Debug.Log($"{v1ttimes4}");


    Debug.Log($"Vector1 Magnitude = {vector1.magnitude}");

   }

}
