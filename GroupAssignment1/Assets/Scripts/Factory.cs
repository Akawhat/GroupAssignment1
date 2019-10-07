using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
  public GameObject Spawn(GameObject obj)
    {
        return Instantiate(obj);
    }
}
