using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public GameObject instance;

    public void InstantiateObject()
    {
        GameObject.Instantiate(instance);
    }
}
