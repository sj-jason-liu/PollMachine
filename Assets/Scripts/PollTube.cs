using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollTube : MonoBehaviour
{
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        _anim.SetTrigger("Poll");
    }
}
