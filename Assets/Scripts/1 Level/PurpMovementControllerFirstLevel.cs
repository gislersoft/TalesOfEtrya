using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PurpMovementControllerFirstLevel : MonoBehaviour {

    public Animator animator;
    public GameObject Player;
    public float atackDistance = 4.0f;
    public float EnemyDistanceDetection = 15.0f;
    public bool freezed;
    public float damping;
    

    Rigidbody theRigidBody;
    NavMeshAgent _agent;

    //Renderer myRender;

    int random;

    // Use this for initialization
    void Start () {
        //animator.SetBool("Fly Die",true);
        //myRender = GetComponent<Renderer>();
        theRigidBody = GetComponent<Rigidbody>();
        random = Random.Range(1, 4);

        _agent = GetComponent<NavMeshAgent>();
        _agent.Warp( transform.position );

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        /*fpsTargetDistance = Vector3.Distance(fpsTarget.position,transform.position);
        if (fpsTargetDistance<enemyLookDistance && fpsTargetDistance >= atackDistance) {
            //myRender.material.color = Color.yellow;
            LookAtPlayer();
            MoveToPlayer();
        } else {
            animator.SetBool( "Creepy Run", false );
        }*/

        

        float distance = Vector3.Distance( transform.position, Player.transform.position );
        if (!freezed) {
            //Debug.Log( "Distance: " + distance );
            if (distance < EnemyDistanceDetection && distance >= (atackDistance * 2)) {
                //Vector3 dirToPlayer = transform.position - Player.transform.position;
                Vector3 newPos = Player.transform.position;
                _agent.SetDestination( newPos );
                animator.SetBool( "Creepy Run", true );
                /*try {
                    _agent.isStopped = false;
                    _agent.SetDestination( newPos );
                } catch {

                }*/
            } else {
                animator.SetBool( "Creepy Run", false );
                _agent.SetDestination( transform.position );
            }

            if (distance < atackDistance * 2) {
                //myRender.material.color = Color.yellow;
                AtackPlayer();
            }
        } else {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        

    }

    void LookAtPlayer(){
        float distance = Vector3.Distance( transform.position, Player.transform.position );
        if (!freezed && distance < atackDistance * 2) {
            Quaternion rotation = Quaternion.LookRotation( Player.transform.position - transform.position );
            transform.rotation = Quaternion.Slerp( transform.rotation, rotation, Time.deltaTime * damping );
        }
    }

    /*
    void MoveToPlayer() {
        animator.SetBool( "Creepy Run", true );
        float step = damping * Time.deltaTime;
        //transform.position = Vector3.MoveTowards( transform.position, fpsTarget.position, step );
        
    }*/

    void AtackPlayer()
    {
        random = Random.Range(1, 4);
        //Debug.Log(random);
        if (!animator.GetBool("Melee Right Attack 0" + random.ToString())) {
            animator.SetBool( "Melee Right Attack 0" + random.ToString(), true );
        }
    }

    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, fpsTarget.position);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atackDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemyLookDistance);
    }*/
}
