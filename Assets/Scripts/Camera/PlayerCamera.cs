using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public string playerName = "Player";
    public float xOffset;
    public float yOffset;
    public float clippingOffset;
    public float xSensitivity;
    public float ySensitivity;
    public float yMax = 50.0f;
    public float yMin = 0.0f;
    public float distance;
    public float smoothing = .5f;

    private Vector3 lerpPosition;
    private GameObject player;
    private float currentX;
    private float currentY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find(playerName);
        this.transform.position = player.transform.position;
    }

    void Update()
    {
        currentX +=  Input.GetAxis("Mouse X") * xSensitivity;
        currentY += -Input.GetAxis("Mouse Y") * ySensitivity;
        currentY = Mathf.Clamp(currentY, yMin, yMax);

        CheckCursorLock();
    }

    void LateUpdate()
    {
        lerpPosition = this.transform.position;

        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        this.transform.position = player.transform.position + rotation * direction;
        this.transform.LookAt(player.transform.position);
        this.transform.position += (this.transform.right * xOffset) + (this.transform.up * yOffset);
        ClipCorrect();

        this.transform.position = Vector3.Lerp(this.transform.position, lerpPosition, 1.0f - smoothing);
    }

    private void ClipCorrect()
    {
        RaycastHit hit;
        if(Physics.Raycast(player.transform.position, this.transform.position - player.transform.position, out hit))
        {
            if (hit.transform.gameObject.tag == "Enenmy")
            {
                hit.transform.gameObject.GetComponent<BasicEnnemy>().behindPlayer = true;
            }
            else if(hit.transform.gameObject.tag != "Trigger")
            {
                float playerToCameraDist = Vector3.Distance(player.transform.position, this.transform.position);
                float playerToHitDist = Vector3.Distance(player.transform.position, hit.point);
                if (playerToHitDist < playerToCameraDist)
                {
                    Vector3 playerDirection = (player.transform.position - this.transform.position).normalized;
                    this.transform.position = hit.point + (playerDirection * clippingOffset);
                }
            }
            
        }
    }

    private void CheckCursorLock()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            switch(Cursor.lockState)
            {
                case (CursorLockMode.None): Cursor.lockState = CursorLockMode.Locked; break;
                case (CursorLockMode.Locked): Cursor.lockState = CursorLockMode.None; break;
            }
        }
    }
}
