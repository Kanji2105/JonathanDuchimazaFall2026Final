
using UnityEngine;
using UnityEngine.AI;

public class ScissormanAI : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 15f;
    public float runRange = 7f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool playerDetected = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || !agent.isOnNavMesh)
            return;

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distance <= chaseRange)
        {
            if (!playerDetected)
            {
                playerDetected = true;
                animator.SetTrigger("Seeplayer");
            }

            agent.SetDestination(player.position);

            if (distance <= runRange)
            {
                agent.speed = 5f;
                animator.SetBool("IsRunning", true);
            }
            else
            {
                agent.speed = 2.5f;
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            agent.ResetPath();
            agent.speed = 2.5f;
            animator.SetBool("IsRunning", false);
            playerDetected = false;
        }
    }
}
