using UnityEngine;
using MLAgents;
using UnityEngine.AI;

public class VisitorAgent : Agent
{
    public LASAgent LASAgent;
    public NavMeshAgent NavAgent;
    // Get Observation
    public override void CollectObservations()
    {
        // Observe LED
        for (int i = 0; i < LASAgent.ledList.Count; i += 1)
        {
            //ledList[i].GetComponent<LEDLightIntensity>().SetLedIntensity(vectorAction[i]);
            float intensity = LASAgent.ledList[i].transform.Find("Point Light").GetComponent<Light>().intensity;
            AddVectorObs(intensity);
        }
        for (int i = 0; i < LASAgent.ledList.Count; i += 1)
        {
            //ledList[i].GetComponent<LEDLightIntensity>().SetLedIntensity(vectorAction[i]);
            float x = LASAgent.ledList[i].transform.Find("Point Light").GetComponent<Light>().transform.position.x;
            float y = LASAgent.ledList[i].transform.Find("Point Light").GetComponent<Light>().transform.position.y;
            AddVectorObs(x);
            AddVectorObs(y);
        }
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (NavAgent.remainingDistance == 0)
        {
            Vector3 destination = Vector3.zero;
            destination.x = vectorAction[0];
            destination.z = vectorAction[1];
            NavAgent.SetDestination(destination);
        }
    }
}
