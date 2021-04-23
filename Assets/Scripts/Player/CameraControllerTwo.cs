using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTwo : MonoBehaviour
{
    //follow stuff
    public Transform targetTransform; //what we are following
    public Transform pivotTransform;
    public Transform camTransform;

    private Vector3 cameraFollowVelocity = Vector3.zero;
    private float followSpeed = .1f;

    //rotation stuff
    private InputManager input;

    public float lookSpeed = .03f;
    public float tiltSpeed = .03f;
    public float minPivot = -35;
    public float maxPivot = 35;

    private float lookAngle;
    private float PivotAngle;

    //collision stuff
    public LayerMask ignoreLayers;

    public float targetLength;
    private float defaultLength;

    public float cameraSphereRadius = .2f;
    public float cameraCollisionOffset = .2f;
    public float minCollisionOffset = .2f;

    private Vector3 camPos;


    //Stuff for rotating cam
    [SerializeField]
    private Vector3 offsetPosition;

    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    private void Awake()
    {
        defaultLength = camTransform.localPosition.z;
    }
    void Start()
    {
        input = InputManager.instance2;
    }


    void Update()
    {
        FollowTarget(Time.deltaTime);
        HandleRotation(Time.deltaTime);
        HandleCollisions(Time.deltaTime);
    }

    /// <summary>
    /// Moves the transoform of the camra holder/parent towards the player
    /// </summary>
    /// <param name="delta"></param>
    private void FollowTarget(float delta)
    {
        //create / store a position somewhere between the holder's current position and the target position
        Vector3 goal = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);

        //assign the value of that calculated position to the camera holder position
        transform.position = goal;
    }

    private void HandleRotation(float delta)
    {

        float mouseX = input.look2.x;
        float mouseY = input.look2.y;

        //calculate looking
        lookAngle += (mouseX * lookSpeed) / delta;

        //calculate tilting
        PivotAngle -= (mouseY * tiltSpeed) / delta;

        //clamp the pivot/tilt
        PivotAngle = Mathf.Clamp(PivotAngle, minPivot, maxPivot);

        //THE FOLLOWING SECTION APPLIES SIDE TO SIDE ROTATION:

        //create a vector3 to store the new rotation in
        Vector3 rotation = Vector3.zero;

        //set the y value of that vector3 to the lookAngle
        rotation.y = lookAngle;

        //create a Quaternion using the vector3
        Quaternion targetRotation = Quaternion.Euler(rotation);

        //set the rotation of the cameraHolder/parent to that quaternion.
        transform.rotation = targetRotation;

        //THE FOLOWING SECTION APPLIES UP AND DOWN ROTATION:

        //zero out the rotation vector3
        rotation = Vector3.zero;

        //set the x value of the vector3 to the pivotAngle
        rotation.x = PivotAngle;

        //set the Quaternion we made to a new Quaternion using the updated V3
        targetRotation = Quaternion.Euler(rotation);

        //set the local rotation of the pivotTransform to the Quaternion.
        pivotTransform.localRotation = targetRotation;

    }

    private void HandleCollisions(float delta)
    {
        //targetPos should by default be the defaultPos
        targetLength = defaultLength;

        //see if the pivot can see the camera
        //If not, change the targetPos (length of the pole) to be shorter 
        //so that the camera will not be inside an object

        RaycastHit hit;

        Vector3 direction = camTransform.position - pivotTransform.position;
        direction.Normalize();

        //Where to cast from, ho wbig of a sphere, what direction,
        //where to store the results, how far to cast, what layers?
        if (Physics.SphereCast(pivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetLength), ignoreLayers))
        {
            //find out how far what we hit was from the pivot
            float dis = Vector3.Distance(pivotTransform.position, hit.point);

            //update the targetLength AKA length of the pole with this distance made negative
            targetLength = -(dis - cameraCollisionOffset);

            if (Mathf.Abs(targetLength) < minCollisionOffset)
            {
                targetLength = -minCollisionOffset;
            }
        }


        //move the camera to the target position
        camPos.z = Mathf.Lerp(camTransform.localPosition.z, targetLength, delta / .2f);

        camTransform.localPosition = camPos;
    }
}
