using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    private CharacterControl _parentCharacter;

    private void Start()
    {
        _parentCharacter = GetComponentInParent<CharacterControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _parentCharacter.SetIsOnGround(true);            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _parentCharacter.SetIsOnGround(false);            
        }
    }
}
