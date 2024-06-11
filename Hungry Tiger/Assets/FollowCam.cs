using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    // Référence à l'objet à suivre
    public Transform target;

    // Offset de la caméra par rapport à la cible
    public Vector3 offset;

    // LateUpdate est appelée après toutes les autres mises à jour
    void LateUpdate()
    {
        // Si la cible est assignée, déplacer la caméra
        if (target != null)
        {
            Vector3 newPosition = target.position + offset;
            transform.position = newPosition;
        }
    }

    // Méthode publique pour changer la cible à suivre
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
