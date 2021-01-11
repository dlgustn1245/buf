using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.8f); //destroy after 0.8 second
    }
}
