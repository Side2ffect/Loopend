using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
    }
}
