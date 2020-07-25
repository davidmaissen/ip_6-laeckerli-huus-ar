using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticlesAfterPlay : MonoBehaviour
{
    private void Start()
     {
         Destroy(gameObject, GetComponent<ParticleSystem>().main.duration); 
     }
}
