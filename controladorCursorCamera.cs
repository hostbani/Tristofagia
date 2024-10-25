using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorCursorCamera : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 1.0f;
    private float gravityValue = -9.81f;
    private Animator animator;
    private Transform myCamera;

    private void Start()
    {
        // Oculta o cursor
        Cursor.visible = false;

        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;

        animator = GetComponent<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
    }

    void Update()
{
    // Rotaciona o personagem para seguir a direção da câmera
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.eulerAngles.y, transform.eulerAngles.z);

    groundedPlayer = controller.isGrounded;
    if (groundedPlayer && playerVelocity.y < 0)
    {
        playerVelocity.y = 0f;
    }

    // Recebe o movimento dos eixos horizontais e verticais
    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

    // Transforma o vetor de movimento para alinhar com a câmera (eixo global)
    move = myCamera.TransformDirection(move);
    move.y = 0; // Ignora a inclinação vertical da câmera

    // Move o controlador de personagem
    controller.Move(move * Time.deltaTime * playerSpeed);

    // Aplica a direção ao personagem
    if (move != Vector3.zero)
    {
        gameObject.transform.forward = move;
        animator.SetBool("isWalk", true);
    }
    else {
        animator.SetBool("isWalk", false);
    }

    playerVelocity.y += gravityValue * Time.deltaTime;
    controller.Move(playerVelocity * Time.deltaTime);
}

}