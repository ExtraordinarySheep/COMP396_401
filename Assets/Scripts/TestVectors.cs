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
        Debug.Log($"{dotProduct}");
        if (distance < 3){
            Debug.Log($"Target in range, Attack!!!")
        }
    }

   
}
