using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum AgentState { Passive, Aggresive };

public class AiPlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent ai;
    [SerializeField] private float aiLookRadius = 5f;
    [SerializeField] private float aiAttackRadius = 3f;
    [SerializeField] private GameObject[] destinationPoints;
    [SerializeField] private int aiDamage;
    [SerializeField] private float aiDamageRate = 1f;

    public Text playerStateText;

    private int currentIndexDestination;
    private Vector3 currentDestination;
    private Vector3 myPosition;

    public AgentState currentAiState;

    private Transform target;
    private HealthManager targetHealth;

    private float distance;

    void Start()
    {
        target = TargetManager.instance.target.transform;
        GetNewDestinationForAi();
        currentAiState = AgentState.Passive;
    }

    void Update()
    {
        myPosition = ai.transform.position;
        playerStateText.text = $"Current State: {currentAiState}";

        if (target != null)
            distance = Vector3.Distance(target.position, myPosition);

        if (distance <= aiLookRadius)
            currentAiState = AgentState.Aggresive;

        switch (currentAiState)
        {
            case AgentState.Passive:
                playerStateText.color = Color.Lerp(Color.green, Color.yellow, 0.5f);
                PassiveState();
                break;

            case AgentState.Aggresive:
                playerStateText.color = Color.red;
                AggresiveState();
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(myPosition, aiLookRadius);
    }

    private void PassiveState()
    {
        if (myPosition.x == currentDestination.x && myPosition.z == currentDestination.z)
            GetNewDestinationForAi();
        else
            ai.SetDestination(currentDestination);
    }

    private void AggresiveState()
    {
        if (target == null)
        {
            currentAiState = AgentState.Passive;
            ai.SetDestination(currentDestination);
            playerStateText.color = Color.Lerp(Color.green, Color.yellow, 0.5f);
        }
        else
            ai.SetDestination(target.position);

        if (distance <= aiAttackRadius && target != null)
            Invoke("AttackTarget", aiDamageRate);
        else if(target == null)
        {
            currentAiState = AgentState.Passive;
            ai.SetDestination(currentDestination);
            playerStateText.color = Color.Lerp(Color.green, Color.yellow, 0.5f);
        }
    }

    private void AttackTarget()
    {
        if(target != null)
        {
            targetHealth = target.gameObject.GetComponent<HealthManager>();
            targetHealth.AddDamage(aiDamage);
        }
    }

    private void GetNewDestinationForAi()
    {
        currentIndexDestination = Random.Range(0, destinationPoints.Length - 1);
        currentDestination = destinationPoints[currentIndexDestination].transform.position;
    }
}