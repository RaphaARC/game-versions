using UnityEngine;

public class MovimentoJogador : MonoBehaviour
{
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;

    [SerializeField] private AudioSource stepsSource;
    [SerializeField] private AudioClip[] stepsClip;

    [Header("Configuração de Velocidade")]
    [SerializeField] private float slowWalkSpeed = 2f;  // Caminhada lenta
    [SerializeField] private float walkSpeed = 5f;      // Andar normal
    [SerializeField] private float runSpeed = 9f;       // Correr

    private bool isWalkingSlowToggle = false; // Estado de caminhada lenta

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Alterna o modo de caminhada lenta ao apertar LeftControl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isWalkingSlowToggle = !isWalkingSlowToggle;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0;

        // Detecta corrida (só corre se não estiver em modo lento)
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && movimento != Vector3.zero && !isWalkingSlowToggle;

        // Escolhe a velocidade com base no estado
        float currentSpeed;
        if (isWalkingSlowToggle && movimento != Vector3.zero)
            currentSpeed = slowWalkSpeed;
        else if (isRunning)
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;

        // Movimento
        controller.Move(movimento * Time.deltaTime * currentSpeed);

        // Gravidade
        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime);

        // Rotação
        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        // Animações
        animator.SetBool("Mover", movimento != Vector3.zero);
        animator.SetBool("Correr", isRunning);
        animator.SetBool("AndarLento", isWalkingSlowToggle && movimento != Vector3.zero);
    }

    private void Passos()
    {
        stepsSource.PlayOneShot(stepsClip[Random.Range(0, stepsClip.Length)]);
    }
}