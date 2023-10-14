using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 2.5f);
    }
}
