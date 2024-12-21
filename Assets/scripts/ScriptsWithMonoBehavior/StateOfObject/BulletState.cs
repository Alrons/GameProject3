using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : MonoBehaviour
{
    // this metod used when bullet get her target position
    public void DestroyObject()
    {
        // code before destroyed object

        Destroy(transform.gameObject);
    }

}
