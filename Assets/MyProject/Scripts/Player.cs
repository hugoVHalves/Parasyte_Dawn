using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 entradasJogador;
    private CharacterController characterController;
    [SerializeField] private float velocidadeJogador;
    private Transform myCamera;

    //Movimentaçao pulo
    private bool estaNoChao;
    [SerializeField] private Transform verificadorChao;
    [SerializeField] private LayerMask cenarioMask;
    [SerializeField] private float alturaDoSalto = 1f;
    private float gravidade = -9.81f;
    private float velocidadeVertical;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myCamera = Camera.main.transform; //recupera a camera princiapal da cena
    }



    // Update is called once per frame
    void Update()
    {
        //gira o personagem para a mesma direçao da camera
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.eulerAngles.y, transform.eulerAngles.z);

        entradasJogador = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        entradasJogador = transform.TransformDirection(entradasJogador);// converte o vetor para trabalhar no mesmo eixo do transform
        characterController.Move(entradasJogador * Time.deltaTime);

        estaNoChao = Physics.CheckSphere(verificadorChao.position, 0.3f, cenarioMask);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            Debug.Log("pulando");
            velocidadeVertical = Mathf.Sqrt(alturaDoSalto * -2f * gravidade);
        }

        if (estaNoChao && velocidadeVertical < 0)
        {
            velocidadeVertical = -1f;
        }
        velocidadeVertical += gravidade * Time.deltaTime;

        characterController.Move(new Vector3(0, velocidadeVertical, 0) * Time.deltaTime);
    }
}



