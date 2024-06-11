using UnityEngine;

public class ActivateAnimationOnMove : StateMachineBehaviour
{
    private bool isWalking = false;
    private Vector3 lastPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enregistrer la position initiale du personnage
        lastPosition = animator.gameObject.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Vérifier si le personnage est en train de marcher en comparant la position actuelle avec la dernière position enregistrée
        Vector3 currentPosition = animator.gameObject.transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);
        isWalking = distance > 0.01f; // Vous pouvez ajuster cette valeur selon votre échelle de jeu

        // Activer ou désactiver l'animation en fonction du mouvement du personnage
        animator.SetBool("IsWalking", isWalking);

        // Mettre à jour la dernière position enregistrée
        lastPosition = currentPosition;
    }
}
