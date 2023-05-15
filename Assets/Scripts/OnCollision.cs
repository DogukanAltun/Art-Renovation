using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Edge")
        {
            transform.parent.GetComponent<NodeManager>().Activate();
        }
    }
}
