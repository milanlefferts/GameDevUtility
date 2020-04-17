using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{

    void Start()
    {
        transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
    }

}
