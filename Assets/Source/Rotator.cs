using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private void Update()
    {
        if (transform.localEulerAngles.y > 360)
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);

            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x,
                transform.localEulerAngles.y + _speed * Time.deltaTime, transform.localEulerAngles.z);
    }
}
