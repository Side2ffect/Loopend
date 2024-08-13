using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Skybox : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1.2f;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
