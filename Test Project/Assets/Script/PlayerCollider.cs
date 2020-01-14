using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("bag(Clone)"))
        {
            Destroy(collision.gameObject);
            GameLogic.knowledge++;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("bag(Clone)"))
        {
            Destroy(collision.gameObject);
            GameLogic.knowledge++;
        }
    }
}
