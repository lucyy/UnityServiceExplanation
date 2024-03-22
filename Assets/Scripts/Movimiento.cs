using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-Vector3.up*0.2f);
    }
  }
