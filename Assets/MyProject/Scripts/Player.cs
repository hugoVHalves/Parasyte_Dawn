using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Transform myCamera;

    [SerializeField] private float velocidadeJogador;
    [SerializeField] private float alturaDoSalto;
    [SerializeField] private float gravidade;
    private Vector3 strafe;
    private Vector3 forward;
    private Vector3 vertical;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myCamera = Camera.main.transform; //recupera a camera princiapal da cena

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        /* ATIVA O CURSOR
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        */
    }

    void Update()
    {
        //gira o personagem para a mesma direçao da camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.eulerAngles.y, transform.eulerAngles.z);

        strafe = Input.GetAxisRaw("Horizontal") * velocidadeJogador * transform.right;
        forward = Input.GetAxisRaw("Vertical") * velocidadeJogador * transform.forward;
        vertical += gravidade * Time.deltaTime * Vector3.up;

        if (characterController.isGrounded)
            vertical = Vector3.down;

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            vertical += alturaDoSalto * Vector3.up;

        if (vertical.y > 0 && (characterController.collisionFlags & CollisionFlags.CollidedAbove) != 0)
            vertical = Vector3.zero;

        characterController.Move((strafe + forward + vertical) * Time.deltaTime);
    }
}