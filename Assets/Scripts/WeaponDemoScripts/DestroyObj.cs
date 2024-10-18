using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    //Destroy time in sec.
    [SerializeField] public float destroyAfter = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyAfter);        
    }
}
