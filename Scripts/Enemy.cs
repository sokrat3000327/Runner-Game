using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    /*
   * there is two state a state waiting to the runner
   * the second state is the running state
   * */
    enum State { Idle, Running }
    // detect how far is the player from the enemy

    [Header(" Settings ")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch(state)
        {
            case State.Idle:
                SearchForTarget();
            break;
            //the enemy running toWards the runner
            case State.Running:
                RunTowardsTarget();
            break;
        }
    }

    // the enemy search for the runner
    private void SearchForTarget()
    {
        //create a sphere around the enemies and it is radius is searchRadius
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
                return;
            }
        }
    }

    // play the run animation
    private void StartRunningTowardsTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget()
    {
        if (targetRunner == null)
            return;

        /*
         * if the runner close to the enemies do 
         */
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position,
            Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < .1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
