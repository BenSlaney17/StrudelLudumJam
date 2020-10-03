using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Camera playerCamera;
    private PlayerMovement playerMovement;
    private GameObject currentObject;

    [Header("UI")]
    public Image interactImage;
    public Sprite hand_open, hand_grabbing;
    public float raycastDistance;

    private bool interacting;

    private Vector3 object_originalPos = Vector3.zero;
    private Quaternion object_originalRot = Quaternion.identity;

    private Vector3 object_desiredPos = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

   

    private void Awake()
    {
        playerCamera = this.GetComponentInChildren<Camera>();
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!interacting)
        {
            if (currentObject != null)
            {
                MoveObjectToOriginalPosition();
            }

            FireRaycast();

            if (Input.GetMouseButtonDown(0) && currentObject)
            {
                HoldObject();
            }

            return;
        }

        if (currentObject == null)
            return;

        currentObject.transform.Rotate(new Vector3(0.0f, 0.25f, 0.0f));

        object_desiredPos = playerCamera.transform.position + playerCamera.transform.forward * 2.0f;

        if (currentObject.transform.position != object_desiredPos)
        {
            MoveObjectToPoint();
        }
           
        if (Input.GetKey(KeyCode.E))
        {
            ReleaseObject();
        }

    }

    void MoveObjectToPoint()
    {
        currentObject.transform.position = Vector3.SmoothDamp(currentObject.transform.position, object_desiredPos, ref velocity, 0.2f);
        //currentObject.transform.Rotate(new Vector3(0.1f, 0.1f, 0.0f));
    }

    void MoveObjectToOriginalPosition()
    {
        if (Vector3.Distance(currentObject.transform.position, object_originalPos) < 0.1f)
        {

            currentObject.transform.position = object_originalPos;
            currentObject.transform.rotation = object_originalRot;
            currentObject = null;
            return;
        }

        currentObject.transform.position = Vector3.SmoothDamp(currentObject.transform.position, object_originalPos, ref velocity, 75f * Time.deltaTime);
        currentObject.transform.rotation = Quaternion.Lerp(currentObject.transform.rotation, object_originalRot, 10.0f * Time.deltaTime);
    }


    void FireRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, raycastDistance) && hit.transform.CompareTag("Interactable"))
        {
            interactImage.enabled = true;
            interactImage.sprite = hand_open;

            if (Input.GetMouseButtonDown(0))
            {
                //objectDistance = Vector3.Distance(hit.transform.position, transform.position);
                currentObject = hit.transform.gameObject;
            }
        }
        else
        {
            if (!interacting)
                interactImage.enabled = false;
        }
    }

    void HoldObject()
    {
        // Store the original position of the object in a transform
        object_originalPos = currentObject.transform.position;
        object_originalRot = currentObject.transform.rotation;

        interacting = true;

        playerMovement.SetCanMove(false);
        playerMovement.UnlockCursor();

        interactImage.enabled = false;
    }

    void ReleaseObject()
    {
        interacting = false;

        //currentObject.transform.position = object_originalPos;
        //currentObject.transform.rotation = object_originalRot;

        playerMovement.SetCanMove(true);
        playerMovement.LockCursor();
        
        //currentObject = null;
    }
}
