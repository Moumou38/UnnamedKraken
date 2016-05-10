using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPhysics : MonoBehaviour
{
    public bool jump;
    public bool idle = false;
    public bool currentRotate = true;
    public float forw;
    public float lat;
    public const float jumpPower = 3f;

    private float currentJump;
    private float velocity = 1f;
    private float smooth = 15f;
    private List<Current> m_currentList = new List<Current>(); 

    public void UpdateCurrentList(Current iCurrent, bool iIn)
    {
        if (m_currentList != null)
        {
            if (iIn) // add current to list
            {
                if (!m_currentList.Contains(iCurrent))
                {
                    m_currentList.Add(iCurrent);
                }
            }
            else
            {
                if (m_currentList.Contains(iCurrent))
                {
                    m_currentList.Remove(iCurrent); 
                }
            }
        }
    }


    public void changeDepth(Vector3 position, bool ilock)
    {
        if (ilock)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;  
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;  
        }
    }

    void BlowMe()
    {
        Rigidbody rigid = GetComponent<Rigidbody>(); 
        if (m_currentList != null)
        {
            foreach (Current c in m_currentList)
            {
                Vector3 v = c.direction - c.origin;
                v.Normalize();
                if (c.reverse == true)
                    v = -v; 
                //v = v - v * (Vector3.Distance(transform.position, c.origin) / Vector3.Distance(c.direction, c.origin));
                //Debug.Log(v); 
                rigid.AddForce(v * c.strength, ForceMode.VelocityChange); 
            }
        }
    }

    public bool IsGrounded()
    {

        float dist = GetComponent<Collider>().bounds.extents.y;
        Vector3 dir = new Vector3(0, -1, 0);
        //edit: to draw ray also//
        Debug.DrawRay(transform.position, dir * dist, Color.green);
        Vector3 origin = transform.position;
        origin.y += 1; 
        RaycastHit hit = new RaycastHit();
        //end edit//
        return Physics.Raycast(origin, dir, out hit, dist + 0.1f);
    }
        

    GameObject controlCameraObject; 
    void Start()
    {
        jump = false;
        controlCameraObject = GameObject.Find("Main Camera");
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;  
    }


    // Update is called once per frame
    void LateUpdate()
    {
        forw = 0f;
        lat = 0f;

        Vector3 mov = Vector3.zero;


        //récupération des input clavier

        float runFactor = 1f;

        //direction
        forw = velocity * runFactor * Input.GetAxisRaw("Vertical");
        lat = velocity * runFactor * Input.GetAxisRaw("Horizontal");

        //input manettes
        if (Input.GetAxis("Left Analog Vertical") > 0.2f || Input.GetAxis("Left Analog Vertical") < -0.2f)
            forw = Input.GetAxis("Left Analog Vertical") * velocity;

        if (Input.GetAxis("Left Analog Horizontal") > 0.2f || Input.GetAxis("Left Analog Horizontal") < -0.2f)
        {
            lat = Input.GetAxis("Left Analog Horizontal") * velocity;
        }



        if (lat != 0)
        {
            currentRotate = true;
        }
        else
            currentRotate = false;
 
        var inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //deplacement
        if (forw != 0 || lat != 0)
        {

            Vector3 lateral = controlCameraObject.transform.right;
            Vector3 forward = controlCameraObject.transform.forward;

            lateral.y = 0;
            lateral.Normalize();
            lateral *= lat;

            forward.y = 0;
            forward.Normalize();
            forward *= forw;

            Vector3 movLat = lateral + forward;
            movLat.Normalize();
            movLat *= 5 * Time.deltaTime / 3f;


           if(!IsGrounded()) // air control a little harder than normal swimming
               GetComponent<Rigidbody>().AddForce(0.5f * inputVector, ForceMode.VelocityChange);
           else
                GetComponent<Rigidbody>().AddForce(0.8f*inputVector, ForceMode.VelocityChange);

          
           SmoothLookAt(movLat);


        }

        if (Input.GetKey(KeyCode.E) && !IsGrounded() && m_currentList.Count == 0)
        {
            getDown();
        }

        BlowMe(); 
        jumping(inputVector); 
        idle = false;
  
    }

    void getDown()
    {
        GetComponent<Rigidbody>().AddForce(-Vector3.up.normalized, ForceMode.VelocityChange);

    }

    void jumping(Vector3 inputVector)
    {
        if (IsGrounded())
        {
            jump = false;

        }
        if ((Input.GetAxis("Jump") > 0) && currentJump > 0.25f)
        {
            //Debug.Log("currentjump : " + currentJump); 
            jump = true; 
            GetComponent<Rigidbody>().velocity = (new Vector3(inputVector.x * 5, Mathf.Sqrt(jumpPower * 2f * Mathf.Abs(Physics.gravity.y)), inputVector.z));

        }
        if (!(Input.GetAxis("Jump") > 0))
        {
            jump = false;

        }

        if (isJumping != null)
        {
            isJumping(jump); 
        }
    }

    public delegate void IsJumping(bool iJumping);
    public event IsJumping isJumping; 

    public void setJumpEnergy(float iEnergy)
    {
        currentJump = iEnergy;
    }

    void SmoothLookAt(Vector3 target)
    {
        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(target, Vector3.up);

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }
}