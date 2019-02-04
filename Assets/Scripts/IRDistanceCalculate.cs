using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRDistanceCalculate : MonoBehaviour
{
    public float irReading = 0;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(string.Format("Visitor {0} enters IR {1}", other.gameObject.name, transform.parent.name));

        // Use (distance - maximum)/(minimum - maximum) to map maximum to 1 and minimum to 0
        float distance = Vector3.Distance(other.transform.position, transform.parent.transform.position);
        irReading = ((float)((distance - 0.8128)/(0.1 - 0.8128)));  
    }

    private void OnTriggerStay(Collider other)
    {
        // Use (distance - maximum)/(minimum - maximum) to map maximum to 1 and minimum to 0
        float distance = Vector3.Distance(other.transform.position, transform.parent.transform.position);
        irReading = ((float)((distance - 0.8128)/(0.1 - 0.8128)));

        //Debug.Log(string.Format("Visitor {0} stays in IR {1}, and distance is {2}", other.gameObject.name, transform.parent.name, distance));
    }

    private void OnTriggerExit(Collider other)
    {
        irReading = 0;
        Debug.Log(string.Format("Visitor {0} leaves IR {1}", other.gameObject.name, transform.parent.name));
    }

    public float GetIRReading()
    {
        return irReading;
    }
}
