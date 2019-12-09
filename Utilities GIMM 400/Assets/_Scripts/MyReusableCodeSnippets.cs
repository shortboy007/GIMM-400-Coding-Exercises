using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyReusableCodeSnippets : MonoBehaviour
{
    //MY CODE SNIPPETS

/*FYI some of these are earlier versions of scripts I am currently using. Most of them are my own creation
  by using parts of scripts from other games or tutorials I have completed. Basically, these are not
  the current version of some of these snippets, but they cover the criteria which is set forth in the
  assignment guidlines.


/*1. AI Movement/Follow Script
void Chase()
{
Finds the distance between an AI with this script on it and then sets up 
booleans for specific distances.
float dist = Vector3.Distance(transform.position, player.transform.position);
//Debug.Log(dist);
if (dist <= 10.0f)
{
    closeToPlayer = true;
}
else
{
    closeToPlayer = false;
}

if (dist >= 10.0f)
{
    awayFromPlayer = true;
}
else
{
    awayFromPlayer = false;
}

If the player is too far from the AI, the AI will follow the player. 
if (awayFromPlayer)
{
    walkSpeed = 5;
    runSpeed = 20;
    transform.position = Vector3.MoveTowards(transform.position,
    player.transform.position, Time.deltaTime * walkSpeed);
    Vector3 rotateTowardPlayer = new Vector3(player.transform.position.x,
    transform.position.y, player.transform.position.z);
    transform.LookAt(rotateTowardPlayer);
}

If the AI is too close to the player, the AI will stop until the player moves far
enough from the AI. 
if (closeToPlayer)
{
    walkSpeed = 0;
    runSpeed = 0;
    Vector3 rotateTowardPlayer = new Vector3(player.transform.position.x,
    transform.position.y, player.transform.position.z);
    transform.LookAt(rotateTowardPlayer);
}
}
*/

/*2. AI Collision with a Player's Weapon

    This Checks to see whether the AI with this script on it is colliding with an item with a specific tag. 
    It does a predetermined amount of damage which it calls from another script. The damage is a static int
    so that it can be referenced from any other script.
    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "WSword" || collidedWith.tag == "WDagger")
        {
            enemyHealth = enemyHealth - PlayerStatHandler.woodWeaponDamage;
        }
    If the enemy's health is below 0, it is set to 0 so that the enemy doesn't get negative health.
    The enemy is destroyed when the health is below or equal to 0;
    if(enemyHealth <= 0)
    {
        enemyHealth = 0;
        Destroy(gameObject, 0.1f);
    }
    */

/*3. Player Movement Using Direct Transform

    This code sets up the rotation, forward, backward, and jumpheight of the player.
    It uses the input presets which can be modified in the project settings.
    This makes it easier to implement controller based games as well as keyboard controlled games.
    The variable runForward is not called in this space because it only occurs if a player is pressing
    a run button which is controlled in another part of the script.
    var rotate = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
    var walkForward = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed;
    var runForward = Input.GetAxis("Vertical") * Time.deltaTime * runSpeed;
    var jump = Input.GetAxis("Jump") * Time.deltaTime * jumpHeight;

    transform.Translate(walkForward, 0, 0);
    transform.Rotate(0, rotate, 0);
    transform.Translate(0, jump, 0);

    */

/*4. Sets Up A State Machine Using Booleans With One Script

    This script has booleans as well as methods set up to determine when an AI should do an action. 
      void Update()
    {       
        //Debug.Log(count);

        Checks distance between AI and player and sets up boolean based on specific distances.
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Player" + distToPlayer);
        if (distToPlayer <= 20)
        {
            closeToPlayer = true;
        }
        else
        {
            closeToPlayer = false;
        }

        if(distToPlayer <= 10)
        {
            withinAttackDistance = true;
        }
        else
        {
            withinAttackDistance = false;
        }
        If the AI is close to the player, it will follow them
        if (closeToPlayer)
        {
            ChaseState();
        }
        If the AI is close enough to the player, it will attack them.
        else if (withinAttackDistance)
        {
            AttackState();
        }
        If neither of the above conditions are true, another method is called which 
        causes the AI to idle or wander.
        else
        {
            //IdleState();
            stateTimer();
        }

    }
    The stateTimer method checks which state the AI is in and whether or not they are still in that
    state when the count reaches 100. It then changes between Idle or WanderState depending on the
    current state.
    void stateTimer()
    {
        count++;

        if (count >= 100 && wanderState)
        {

            IdleState();
            idleState = true;
            wanderState = false;
        }
        else if (count >= 100 && idleState)
        {

            WanderState();
            idleState = false;
            wanderState = true;
        }
    }

    void IdleState()
    {
        count = 0;
        walkSpeed = 0;
        runSpeed = 0;

        //Debug.Log("IdleState");
    }

    In this method, the AI moves and rotates toward the player
    void ChaseState()
    {
        count = 0;
        walkSpeed = 5;
        runSpeed = 20;

        transform.position = Vector3.MoveTowards(transform.position,
        player.transform.position, Time.deltaTime * walkSpeed);
        Vector3 rotateTowardPlayer = new Vector3(player.transform.position.x,
        transform.position.y, player.transform.position.z);
        transform.LookAt(rotateTowardPlayer);

        //Debug.Log("ChaseState");

    }
    In this method, the AI moves faster towards the player
    void AttackState()
    {
        walkSpeed = 5;
        runSpeed = 20;
        transform.position = Vector3.MoveTowards(transform.position,
        player.transform.position, Time.deltaTime * runSpeed);
        Vector3 rotateTowardPlayer = new Vector3(player.transform.position.x,
        transform.position.y, player.transform.position.z);
        transform.LookAt(rotateTowardPlayer);

        //Debug.Log("AttackState");
    }
    In this method, the counter goes up to one hundred and once it reaches 100, the AI chooses a direction based
    on the number which is assigned to direction to turn. The AI has four objects on it, which are the directions set up
    for the AI to move towards. There is an empty game object on the front, back, and both sides of the AI Gameobject.
    void WanderState()
    {
        count++;

        if(count >= 100)
        {
            directionToTurn = Random.Range(0, 4);
            CountReset();
        }

        if (directionToTurn == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            wanderObjectForward.transform.position, Time.deltaTime * walkSpeed);
            transform.rotation = wanderObjectForward.transform.rotation;
            //Debug.Log("Forward");
        }
        else if (directionToTurn == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            wanderObjectBackward.transform.position, Time.deltaTime * walkSpeed);
            transform.rotation = wanderObjectBackward.transform.rotation;
            //Debug.Log("Backward");
        }
        else if (directionToTurn == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            wanderObjectRight.transform.position, Time.deltaTime * walkSpeed);
            transform.rotation = wanderObjectRight.transform.rotation;
            //Debug.Log("Right");
        }
        else if (directionToTurn == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            wanderObjectLeft.transform.position, Time.deltaTime * walkSpeed);
            transform.rotation = wanderObjectLeft.transform.rotation;
            //Debug.Log("Left");
        }
        }
        This Sets the count back to 0 each time it reaches 100 so that the AI is constantly
        choosing a new direction to go.
        void CountReset()
        {
            count = 0;
        }
        */

/*5. Player Death and Respawning

If a player collides with a monster, the PlayerHealth value subtracts the totalMonsterDamage value from it. 
     void OnCollisionEnter(Collision coll)
{
    GameObject collidedWith = coll.gameObject;
    if (collidedWith.tag == "Monster")
    {
        PlayerHealth = PlayerHealth - totalMonsterDamage;

        healthText = healthTextBox.GetComponent<Text>();
        healthText.text = "Health: " + PlayerHealth;
        }
      }
If the PlayerHealth variable is less than or equal to 0, the player's health is set to 0 and
is updated on the healthText of a main screen overlay canvas is shown as 0. 
The player is then teleported back to the starting position which is a vector equal to the 
transform position at the start of the game.
The PlayerHealth variable is set back to 100 and the player can begin playing again.
    void Update()
    {
        if(PlayerHealth <= 0)
       {
        PlayerHealth = 0;

        healthText = healthTextBox.GetComponent<Text>();
        healthText.text = "Health: " + PlayerHealth;

        player.transform.position = startPos.transform.position;

        PlayerHealth = 100;
    }
    }
    This is an alternate version of how to handle a player's death. It uses a menu
    which comes up if a player runs out of health. The menu has buttons which give
    the option to quit or restart the game. Quit would take the player to the main menu,
    while the restart option would reload the current scene.
            /*if (PlayerHealth <= 0)
        {               
            deathMenuCam.GetComponent<Camera>().enabled = true;
            deathMenuCam.GetComponent<AudioListener>().enabled = true;
            deathMenuCanvas.GetComponent<Canvas>().enabled = true;
        }
        */

/*6. Sound Manager and Calling a sound
 
  This code makes use of the SoundManager from the Bobblehead wars Unity tutorial.
  The soundmanager is instanced as a static script so that it can be used throughout the project.
  The sounds are instantiated and then an array of the sounds is set up in the start method.
  The PlayOneShot method tells the manager to get a specific sound clip and play it. This
  method is public so that it can be reached by other scripts.
public static SoundManager Instance = null;
private AudioSource soundEffectAudio;
public AudioClip throwSpear;
public AudioClip hit;
public AudioClip hurtPlayer;
public AudioClip hurtEnemy;
public AudioClip playerPickupMeat;
public AudioClip playerDeath;
public AudioClip enemyDeath;
public AudioClip tyrannosaurusRoar;
public AudioClip pterodactylRoar;
public AudioClip pterodactylWingFlap;


// Use this for initialization
void Start()
{
    if (Instance == null)
    {
        Instance = this;
    }
    else if (Instance != this)
    {
        Destroy(gameObject);
    }
    AudioSource[] sources = GetComponents<AudioSource>();
    foreach (AudioSource source in sources)
    {
        if (source.clip == null)
        {
            soundEffectAudio = source;
        }
    }
}

public void PlayOneShot(AudioClip clip)
{
    soundEffectAudio.PlayOneShot(clip);
}

DIFFERENT SCRIPT

If the player presses the spacebar, a throwSpear method is called.
         if (Input.GetKeyDown(KeyCode.Space))
        {
            throwSpear();
        }
                    }               
   
    This method instantiates a new gameobject, spearPrefab, at the position of an empty gameobject called spearSpawn.
    It also makes use of the SoundManager script to call the PlayOneShot method to play a sound.
    void throwSpear()
    {

        var spear = (GameObject)Instantiate(
            spearPrefab,
            spearSpawn.position,
            spearSpawn.rotation);

        spear.GetComponent<Rigidbody>().velocity = spear.transform.forward * 100.0f;

        Destroy(spear, 10.0f);

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.throwSpear);
    }
    */

/*7. Animation Controller Script
 * 
 * This is the player movement setup that was in reusable script number 3.
 * The difference with this however, is that it also has animations set up for when
 * a player using the keyboard presses a button, an animation will play.
 *  var rotate = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
    var walkForward = Input.GetAxis("Vertical") * Time.deltaTime * walkSpeed;
    var runForward = Input.GetAxis("Vertical") * Time.deltaTime * runSpeed;
    var jump = Input.GetAxis("Jump") * Time.deltaTime * jumpHeight;

    transform.Translate(walkForward, 0, 0);
    transform.Rotate(0, rotate, 0);
    transform.Translate(0, jump, 0);

    If the player presses any of the buttons below, the corresponding animation will play.
    The GetKey conditionals will play as long as the player holds the button. The else part
    sets the animation bool to false if no button is pressed. The GetKey conditionals are 
    used for movement, while the GetKeyDown conditionals are only used for animations which
    only play once.
    if (Input.GetKey(KeyCode.W))
    {
        playerAnims.SetBool("isWalkingForward", true);
    }
    else
    {
        playerAnims.SetBool("isWalkingForward", false);
    }
    if (Input.GetKey(KeyCode.S))
    {
        playerAnims.SetBool("isWalkingBackward", true);
    }
    else
    {
        playerAnims.SetBool("isWalkingBackward", false);
    }

    If the player presses left or right shift, they are able to use the runForward
    variable set up above. It does the same as the walkForward variable, except
    that it makes the player move faster.
    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
    {
        playerAnims.SetBool("isRunningForward", true);
        transform.Translate(runForward, 0, 0);
    }
    else
    {
        playerAnims.SetBool("isRunningForward", false);
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
        playerAnims.SetBool("isJumping", true);
    }
    else
    {
        playerAnims.SetBool("isJumping", false);
    }
}
