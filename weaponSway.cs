using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class weaponSway : MonoBehaviour
{
    [SerializeField] private FirstPersonController FPSCobject;

    // CREDIT : https://www.youtube.com/watch?v=QIVN-T-1QBE

    [Header("Sway Settings")]
    [SerializeField] private float smooth;
    [SerializeField] private float swayMultiplier;
    private bool moving;
    public GameObject player;
    float xpos;
    float ypos;
    float zpos;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);

        Vector3 playerspeed = FPSCobject.m_CharacterController.velocity;

        if(playerspeed.x > 0) //may need a z axis check
        {
            moving = true;
        }

        if (moving)
        {
            //object moves in y=x^2 at a speed dependant on the speed of the player 
            transform.localPosition = new Vector3(xpos, ypos, zpos);
            xpos = xpos * playerspeed.x;
            ypos = ypos * playerspeed.y;

            xpos = ypos * ypos;
            ypos = xpos * xpos;

        }
    }
}
