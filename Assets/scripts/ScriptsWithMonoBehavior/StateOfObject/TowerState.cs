using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerState : MonoBehaviour
{
    // this metod used when tower colision with wave 
    public void DestroyObject()
    {
        // code before destroyed object

        Destroy(transform.gameObject);
    }
}
