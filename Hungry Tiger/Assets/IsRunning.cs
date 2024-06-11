using UnityEngine;

public class IsRunning : StateMachineBehaviour
{
    private Vector3 lastPosition;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enregistrer la position initiale du personnage
        lastPosition = animator.gameObject.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Vérifier si le personnage court en comparant la position actuelle avec la dernière position enregistrée
        Vector3 currentPosition = animator.gameObject.transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);
        bool isRunning = distance >= 0.1f; // Ajuster cette valeur selon votre échelle de jeu

        // Activer ou désactiver l'animation de course en fonction du mouvement du personnage
        animator.SetBool("IsRunning", isRunning);

        // Mettre à jour la dernière position enregistrée
        lastPosition = currentPosition;
    }
}
