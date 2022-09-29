using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollWheelTuring : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }
}
