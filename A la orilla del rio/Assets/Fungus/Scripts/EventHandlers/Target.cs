using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public Vector2 followSpot;
    public float speed = 1f;
    public bool inDialogue;
    public bool cutsceneInProgress;
    private NavMeshAgent agent;

    public Verb verb;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        verb = FindObjectOfType<Verb>();
        Set_Player_Position_To_Spawnpoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialogue == false)
        {
            //posicion del mouse
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0) )
        {
            //actualizar la posicion del target
            followSpot = new Vector2(mousePos.x, mousePos.y);
        }
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));

        // moverse hacia el punto de seguimiento
        //transform.position = Vector2.MoveTowards(transform.position, followSpot, speed * Time.deltaTime);
        }
        
    }

    public void exitDialogue()
    {
        inDialogue = false;
        cutsceneInProgress = false;

        verb.verb = Verb.Actions.Walk;
        verb.UpdateVerbTextBox(null);
        verb.gameObject.SetActive(true);
    }

    public void enterDialogue()
    {
        inDialogue = true;
        cutsceneInProgress = true;

        verb.verb = Verb.Actions.Walk;
        verb.UpdateVerbTextBox(null);
        verb.gameObject.SetActive(false);
    }

    public void SetDestinationTarget()
    {
        agent.SetDestination(transform.position);
    }

    public void dontMove()
    {
        agent.SetDestination(transform.position);
        followSpot = transform.position;
        inDialogue = true;

    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {
    //     followSpot = transform.position;
    // }


    public void Set_Player_Position_To_Spawnpoint(){
        string pointName = Game_Master.instance.lastSpawnpointName;
        print("Point name: " +pointName);
        if(pointName == null || pointName == string.Empty){
            pointName = "Default_Point";
        }
        GameObject point = GameObject.Find("Spawn_Points/" +Game_Master.instance.lastSpawnpointName);
        print(point);
        if(point == null){
            print("ERR: COULD NOT FIND SPAWN POINT OF " +Game_Master.instance.lastSpawnpointName);
            point = GameObject.Find("Spawn_Points/Default_Point");
            this.transform.position = followSpot = point.transform.position;
            return;
        }
        print("Found point at: " +point.transform.position);
        this.transform.position = followSpot = point.transform.position;
    }

}
