using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 8f;
    public float runSpeed = 12f;
    public float rotationSpeed = 720f; // Vitesse de rotation en degrés par seconde
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public Camera playerCamera; // Référence à la caméra du joueur

    private Vector3 velocity;
    private bool isGrounded;
    private Animator animator; // Référence au composant Animator
    private float speedMultiplier = 1f;

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtenir la référence au composant Animator
        Cursor.lockState = CursorLockMode.Locked; // Verrouiller le curseur au centre de l'écran

        // Assigner la caméra principale si elle n'est pas déjà assignée
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        // Vérification pour s'assurer que la caméra est assignée
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera not assigned in the Inspector and no Main Camera found in the scene.");
        }

        // Vérification pour s'assurer que l'Animator est assigné
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // Si la caméra n'est pas assignée, sortir de la méthode Update
        if (playerCamera == null || animator == null)
        {
            return;
        }

        // Vérifier si le personnage est au sol
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Applique une petite force vers le bas pour coller au sol
        }

        // Obtenir l'entrée du joueur
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Gérer l'accélération du personnage
        if (vertical != 0 || horizontal != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speedMultiplier = 2f;
            }
            else
            {
                speedMultiplier = 1f;
            }
        }
        else
        {
            speedMultiplier = 0f;
        }

        // Vérifier si le personnage doit se déplacer
        if (vertical != 0 || horizontal != 0)
        {
            // Calculer la direction de la caméra
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;

            // Ignorer la composante verticale
            cameraForward.y = 0f;
            cameraRight.y = 0f;

            // Normaliser les directions
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculer le vecteur de mouvement en fonction de la direction de la caméra
            Vector3 moveDir = (cameraForward * vertical + cameraRight * horizontal).normalized;
            float currentSpeed = walkSpeed * speedMultiplier; // Appliquer le multiplicateur de vitesse
            controller.Move(moveDir * currentSpeed * Time.deltaTime);

            // Définir les animations
            animator.SetBool("IsWalking", speedMultiplier == 1f);
            animator.SetBool("IsRunning", speedMultiplier == 2f);
            animator.SetBool("IsIdling", false); // Pas à l'arrêt car il y a du mouvement
            animator.SetBool("IsHitting", false); // Désactiver l'animation de frappe

            // Faire tourner le personnage vers la direction du mouvement de manière fluide
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Désactiver les animations de marche et de course, activer l'animation d'inactivité
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdling", true);
            animator.SetBool("IsHitting", false); // Désactiver l'animation de frappe
        }

        // Appliquer la gravité
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            // Gérer le saut
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Appliquer le mouvement vertical (gravité et saut)
        controller.Move(velocity * Time.deltaTime);

        // Vérifier si le clic gauche de la souris est enfoncé pour activer l'animation de frappe
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsHitting", true);
        }
        else
        {
            // Si le clic de la souris est relâché, désactiver l'animation de frappe
            animator.SetBool("IsHitting", false);
        }
    }
}
