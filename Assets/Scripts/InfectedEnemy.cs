using UnityEngine;
using UnityEngine.AI;

public class InfectedEnemy : MonoBehaviour
{
    public float hp;
    public float hurtAmount;
    public SkinnedMeshRenderer MeshRenderer;
    public Material def;
    public Material hur;
    public bool isInside;

    private NavMeshAgent agent;
    public Transform target;

    public Animator animator;


    public bool isInRange;
    public bool isTouchingPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MeshRenderer = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        MeshRenderer.material = def;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInRange)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", true);
            transform.LookAt(target.position);
        }
        if (!isTouchingPlayer)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);
        isInRange = agent.remainingDistance < agent.stoppingDistance;



        if (hp < 0)
        {
            Destroy(gameObject);
        }

        if (!isInside && hp != 0)
            MeshRenderer.material = def;

        
        
    }
    private void LateUpdate()
    {
        if (hp == 0)
            hp -= 1;
        isInside = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            hp -= hurtAmount;
            MeshRenderer.material = hur;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
            isInside = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sword") && hp != 0)
            MeshRenderer.material = def;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            isTouchingPlayer = true;
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            isTouchingPlayer = false;
    }
}
