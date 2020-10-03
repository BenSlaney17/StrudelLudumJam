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
    public Sprite hand_open, hand_grabbing, rotateSprite;
    public float raycastDistance;

    private bool interacting;
    private bool rotatingObject;

    private float rotationSpeed = 500f;

    private Vector3 object_originalPos = Vector3.zero;
    private Quaternion object_originalRot = Quaternion.identity;

    private GameObject lastSeenObject;

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

        RotateObject();

        object_desiredPos = playerCamera.transform.position + playerCamera.transform.forward * 2.0f;

        if (currentObject.transform.position != object_desiredPos)
        {
            MoveObjectToPoint();
        }
           
        if (Input.GetMouseButtonDown(1))
        {
            ReleaseObject();
        }

    }

    void MoveObjectToPoint()
    {
        currentObject.transform.position = Vector3.SmoothDamp(currentObject.transform.position, object_desiredPos, ref velocity, 0.2f);
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
            lastSeenObject = hit.transform.gameObject;

            OutlineObject(lastSeenObject);

            SetCursorSprite(hand_open);

            if (Input.GetMouseButtonDown(0))
            {
                currentObject = hit.transform.gameObject;

                
            }
        }
        else
        {
            if(lastSeenObject != null) RemoveOutline(lastSeenObject);

            if (!interacting) interactImage.enabled = false;
        }
    }

    void OutlineObject(GameObject obj)
    {
        if (obj.GetComponent<Outline>() != null) return;

        var outline = obj.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 10.0f;
    }

    void RemoveOutline(GameObject obj)
    {
        if (obj.GetComponent<Outline>() == null) return;
        Destroy(obj.GetComponent<Outline>());
    }


    void RotateObject()
    {
        interactImage.transform.position = (Input.mousePosition);

        // If the object is currently being dragged by the mouse, execute contents
        if (rotatingObject)
        {
            if (Input.GetMouseButton(0))
            {
                currentObject.transform.Rotate((Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime), (-Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);
            }
            else
            {
                interactImage.enabled = false;
                rotatingObject = false;
            }
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if ((hit.transform.gameObject == currentObject || hit.transform.IsChildOf(currentObject.transform)))
            {
                SetCursorSprite(rotateSprite);

                if (Input.GetMouseButtonDown(0)) rotatingObject = true;
            }
            else
            {
                if (!rotatingObject) interactImage.enabled = false;
            }
        }
    }

    void SetCursorSprite(Sprite sprite)
    {
        interactImage.enabled = true;
        interactImage.sprite = sprite;
    }


    void HoldObject()
    {
        // Store the original position of the object in a transform
        object_originalPos = currentObject.transform.position;
        object_originalRot = currentObject.transform.rotation;

        interacting = true;

        playerMovement.SetCanMove(false);
        playerMovement.UnlockCursor();

        RemoveOutline(currentObject);
    }

    void ReleaseObject()
    {
        interacting = false;

        playerMovement.SetCanMove(true);
        playerMovement.LockCursor();

        interactImage.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
}
