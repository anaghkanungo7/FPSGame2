using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;



public class ClassPlayerController : MonoBehaviour
{

    public float health = 200f;



    int moveSpeed = 5; // how fast the player moves
    float lookSpeedX = 6; // left/right mouse sensitivity
    float lookSpeedY = 3; // up/down mouse sensitivity
    int jumpForce = 250; // ammount of force applied to create a jump

    


    // Enemy code 
    public int enemyCount = 0;
    public int collectablesFound = 0;
    public int collectablesGoal = 10;
    public int enemyGoal = 10;


    // TextMeshes
    
    public TextMeshProUGUI gunObjectiveText;
    public TextMeshProUGUI enemyObjectiveText;
    public TextMeshProUGUI energyObjectiveText;

    public Transform camTrans; // a reference to the camera transform
    float xRotation;
    float yRotation;
    Rigidbody _rigidbody;

    //The physics layers you want the player to be able to jump off of. Just dont include the layer the palyer is on.
    public LayerMask groundLayer;

    public Transform feetTrans; //Position of where the players feet touch the ground
    float groundCheckDist = .5f; //How far down to check for the ground. The radius of Physics.CheckSphere
    public bool grounded = false; //Is the player on the ground

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        lookSpeedX *= .65f; //WebGL has a bug where the mouse has higher sensitibity. This compensates for the change. 
        lookSpeedY *= .65f; //.65 is a rough guess based on testing in firefox.
#endif
        _rigidbody = GetComponent<Rigidbody>(); // Using GetComponent is expensive. Always do it in start and chache it when you can.
        Cursor.lockState = CursorLockMode.Locked; // Hides the mouse and locks it to the center of the screen.
    }

    void FixedUpdate()
    {
        //Creates a movement vector local to the direction the player is facing.
        Vector3 moveDir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxis("Horizontal"); // Use GetAxisRaw for snappier but non-analogue  movement
        moveDir *= moveSpeed;
        moveDir.y = _rigidbody.velocity.y; // We dont want y so we replace y with that the _rigidbody.velocity.y already is.
        _rigidbody.velocity = moveDir; // Set the velocity to our movement vector

        //The sphere check draws a sphere like a ray cast and returns true if any collider is withing its radius.
        //grounded is set to true if a sphere at feetTrans.position with a radius of groundCheckDist detects any objects on groundLayer within it
        grounded = Physics.CheckSphere(feetTrans.position, groundCheckDist, groundLayer);
    }

    void Update()
    {
        // Updating text
        enemyObjectiveText.SetText("Kill " + enemyCount + "/" + enemyGoal + " enemies");
        energyObjectiveText.SetText("Collect " + collectablesFound + "/" + collectablesGoal + " energy cells");

        bool completedEnemy = (enemyCount == enemyGoal);
        bool collectedEnergy = (collectablesFound == collectablesGoal);

        if (completedEnemy)
            enemyObjectiveText.fontStyle = FontStyles.Strikethrough;

        if (collectedEnergy)
            energyObjectiveText.fontStyle = FontStyles.Strikethrough;

        if (completedEnemy && collectedEnergy)
            SceneManager.LoadScene("Ending");

        if (health < 0) {
            SceneManager.LoadScene("RetryPage");
        }


        

        
        yRotation += Input.GetAxis("Mouse X") * lookSpeedX;
        xRotation += Input.GetAxis("Mouse Y") * -1 * lookSpeedY; //inverted
        xRotation = Mathf.Clamp(xRotation, -90, 90); //Keeps up/down head rotation realistic
        camTrans.localEulerAngles = new Vector3(xRotation, 0, 0);
        transform.eulerAngles = new Vector3(0, yRotation, 0);

        if (grounded && Input.GetButtonDown("Jump")) //if the player is on the ground and press Spacebar
        {
            _rigidbody.AddForce(new Vector3(0, jumpForce, 0)); // Add a force jumpForce in the Y direction
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            collectablesFound++;
        }
    }
}