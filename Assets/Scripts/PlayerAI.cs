using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerSwitcher playerSwitcher;

    [Header("Detection Radius")]
    [SerializeField] private float playerRadius = 5f;
    [SerializeField] private float enemyRadius = 10f;

    [Header("Detection")]
    public bool playerDetected;
    public bool enemyDetected;
    public NavMeshAgent _agent;


    private void Update()
    {
        // Détection du joueur actuel
        playerDetected = false;

        if (playerSwitcher != null && playerSwitcher._currentPlayer != null)
        {
            if (Vector3.Distance(transform.position, playerSwitcher._currentPlayer.transform.position) <= playerRadius)
            {
                playerDetected = true;
            }
        }

        // Détection des ennemis
        enemyDetected = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= enemyRadius)
            {
                enemyDetected = true;
                break;
            }
        }

        if(!playerDetected)
        {
            _agent.SetDestination(playerSwitcher._currentPlayer.transform.position);
        }
        else
        {
            _agent.SetDestination(this.transform.position);
        }
    }

   
}
