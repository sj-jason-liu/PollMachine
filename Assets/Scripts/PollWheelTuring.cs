using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollWheelTuring : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime);
    }
}
