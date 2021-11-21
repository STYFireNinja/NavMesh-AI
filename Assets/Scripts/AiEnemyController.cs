using UnityEngine;
using UnityEngine.AI;

public class AiEnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent ai;
    [SerializeField] private GameObject[] destinationPoints;

    private int currentIndexDestination;
    private Vector3 currentDestination;
    private Vector3 myPosition;

    void Start()
    {
        GetNewDestinationForAi();
    }

    void Update()
    {
        myPosition = ai.transform.position;
        PassiveState();
    }

    private void PassiveState()
    {
        if (myPosition.x == currentDestination.x && myPosition.z == currentDestination.z)
            GetNewDestinationForAi();
        else
            ai.SetDestination(currentDestination);
    }

    private void GetNewDestinationForAi()
    {
        currentIndexDestination = Random.Range(0, destinationPoints.Length - 1);
        currentDestination = destinationPoints[currentIndexDestination].transform.position;
    }
}