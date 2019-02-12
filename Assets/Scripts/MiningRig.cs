using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// modified by Heimer, Christoffer Brandt, Robin
/// </summary>

public class MiningRig : MonoBehaviour
{
    [SerializeField] private bool pickedUp;
    [SerializeField] public float timeBetweenBoxes = 2;
    [SerializeField] private GameObject chunk;

    private StoreWindow storeWindow;

    private AudioSource rigCollision;
    private AudioSource drillingLoop;
    private AudioSource deployBox;
    private AudioSource disableRig;
    private AudioManager audioManager;

    private GameObject box;
    private PlayerController player;
    private MeshRenderer rigStatusRend;
    private Animator animator;
    private MiningNode minedNode; //currently minedNode set to null when no minedNode
    private Color baseColor;
    private Rigidbody rb;
    public bool shielded = false;
    public bool coBoxSpawnRunning = false;
    public bool coPickUpRigRunning = false;

    public bool functioning = true;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        box = GameObject.Find("GetableBox");
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>();
        rigStatusRend = transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        rigStatusRend.material.color = Color.red;
        animator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        AudioSource[] audios = GetComponents<AudioSource>();
        rigCollision = audios[0];
        drillingLoop = audios[1];
        deployBox = audios[2];
        disableRig = audios[3];
        storeWindow = FindObjectOfType<StoreWindow>();
    }

    private void OnEnable()
    {
        if (rigStatusRend != null)
        {
            rigStatusRend.material.color = Color.red;
        }
    }

    public void TryPickup()
    {
        if (!functioning)
        {
            Repair();
        }
        else if (minedNode)
        {
            StartCoPickUpRig();
        }
        else if(!coPickUpRigRunning)
        {
            shielded = false;
            player.pickedUpItem = true;
            player.SetEnableHoldingRig(true);
            transform.SetParent(player.transform);
            audioManager.Play("Pickup");
            gameObject.SetActive(false);
        }
    }

    public void StartCoPickUpRig()
    {
        StartCoroutine("CoPickUpRig");
    }

    //Rig is parented to player on pickup, despawns and changes a bool to indicate the player is carrying it
    private IEnumerator CoPickUpRig()
    {
        if (!coPickUpRigRunning)
        {
            coPickUpRigRunning = true;
            player.pickedUpItem = true;
            minedNode = null;

            if (animator.GetBool("OnNodeDeploy") || animator.GetBool("Empty"))
            {
                animator.SetBool("Empty", false);
                animator.SetBool("OnNodeDeploy", false);
                animator.SetBool("OnPickUp", true);
                rigStatusRend.material.color = Color.yellow;
                pickedUp = true;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                animator.SetBool("Empty", false);
                animator.SetBool("OnNodeDeploy", false);
                animator.SetBool("OnPickUp", true);
                rigStatusRend.material.color = Color.red;
                pickedUp = true;
            }

            coBoxSpawnRunning = false;
            coPickUpRigRunning = false;
            rigStatusRend.material.color = Color.red;
        }

        yield return null;
    }  



    //Rig is un-parented, spawns in front of player and changes the bool to false
    public void DropRig()
    {
        animator.SetBool("OnPickUp", false);
        gameObject.transform.SetParent(null);
        gameObject.SetActive(true);
        pickedUp = false;
        transform.position = player.transform.position + player.model.transform.TransformDirection(Vector3.up * 4 + Vector3.forward * 2);
        rb.velocity = Vector3.zero;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}

    public void ThrowRig(float throwStrength)
    {
        DropRig();
        rb.velocity =  player.model.transform.TransformDirection(Vector3.up * 2 + Vector3.forward * 4 * throwStrength + Vector3.forward*player.playerSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rigCollision.Play();
        }
    }

    public void BreakRig()
    {
        if (!shielded)
        {
            if (functioning)
            {
                disableRig.Play();
            }
            functioning = false;
            rigStatusRend.material.color = Color.black;
            coBoxSpawnRunning = false;
        }
    }

    //If the object collides with the "Node" tag AND picked up is false(released), changes color to green and starts spawning boxes
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node" && minedNode == null)
        {
            if (!pickedUp)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                animator.SetBool("OnNodeDeploy", true);
                minedNode = other.GetComponent<MiningNode>();
                transform.position = minedNode.transform.position;

                if (functioning)
                {
                    StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
                }
            }
        }
        
        if (other.tag == "Sanctuary")
        {
            shielded = true;
        }
    }

    //If exiting node collider, change color to red
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("OnPickUp", true);
            minedNode = null;
            rigStatusRend.material.color = Color.red;
        }

        if (other.tag == "Sanctuary")
        {
            shielded = false;
        }
    }

    //Coroutine that uses for loop to create boxes in the rigs proximity within a set interval
    private IEnumerator CoBoxSpawn(int resourseAmount)
    {
        if (!coBoxSpawnRunning)
        {
            coBoxSpawnRunning = true;
            rigStatusRend.material.color = Color.yellow;
            animator.SetBool("OnNodeDeploy", true);
            animator.SetBool("Empty", false);
            animator.SetBool("OnPickUp", false);
            yield return new WaitForSeconds(2.8f);
            drillingLoop.Play();
            timeBetweenBoxes = storeWindow.MiningRigExtractionRate;
            Debug.Log("timeBetweenBoxes is " + timeBetweenBoxes);
            for (int i = 0; i < resourseAmount; i++)
            {
                for (int w = 0; w < timeBetweenBoxes; w++)
                {
                    if (functioning && minedNode)
                    {
                        rigStatusRend.material.color = Color.green;
                    }
                    else if (!functioning)
                    {
                        rigStatusRend.material.color = Color.black;
                        drillingLoop.Stop();
                        break;
                    }
                    else
                    {
                        drillingLoop.Stop();
                    }
                    yield return new WaitForSeconds(1);
                }

                if (functioning && minedNode)
                {
                    rigStatusRend.material.color = Color.green;
                    EjectChunk();
                    if (!minedNode.OnBoxSpawn()) //Do when empty 
                    {
                        minedNode = null;
                        rigStatusRend.material.color = Color.red;
                        animator.SetBool("Empty", true);
                    }
                }
                else if (functioning)
                {
                    rigStatusRend.material.color = Color.red;
                }

                if (pickedUp)
                {
                    yield break;
                }
            }
            drillingLoop.Stop();
            animator.SetBool("OnNodeDeploy", false);
            animator.SetBool("Empty", false);
            animator.SetBool("OnPickUp", true);

            coBoxSpawnRunning = false;
        }
        yield return null;
    }

    public void Repair()
    {
        functioning = true;
        audioManager.Play("Repair");
        rigStatusRend.material.color = Color.red;
        if (minedNode)
        {
            StartCoroutine(CoBoxSpawn(minedNode.resourceValue));
        }
    }

    //Ejects a boc in a random direction from the MiningRig
    public void EjectChunk()
    {
        Chunk newChunk = Instantiate(chunk, transform.position + transform.TransformDirection(Vector3.up * 3), Quaternion.identity).GetComponent<Chunk>();

        if (minedNode.materialType == GameController.MetalVarieties.CINNABAR)
        {
            newChunk.chunkType = GameController.MetalVarieties.CINNABAR;
            newChunk.myRenderer.material = GameController.instance.metalMaterials[0];
        }
        else if (minedNode.materialType == GameController.MetalVarieties.TUNGSTEN)
        {
            newChunk.chunkType = GameController.MetalVarieties.TUNGSTEN;
            newChunk.myRenderer.material = GameController.instance.metalMaterials[1];
        }
        else if (minedNode.materialType == GameController.MetalVarieties.COBALT)
        {
            newChunk.chunkType = GameController.MetalVarieties.COBALT;
            newChunk.myRenderer.material = GameController.instance.metalMaterials[2];
        }
        else
        {
            newChunk.chunkType = RandomChunkType(newChunk);
        }

        newChunk.rb.velocity = transform.TransformDirection(Vector3.up * 15 + Vector3.forward * Random.Range(-10, 10) + Vector3.right * Random.Range(-10, 10));
        deployBox.Play();
    }

    private GameController.MetalVarieties RandomChunkType(Chunk newChunk)
    {
        int random = Random.Range(0, 3);

        newChunk.myRenderer.material = GameController.instance.metalMaterials[random];

        if (random < 1)
        {
            return GameController.MetalVarieties.CINNABAR;
        }
        if (random < 2)
        {
            return GameController.MetalVarieties.TUNGSTEN;
        }
        return GameController.MetalVarieties.COBALT;
    }
}
