using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f; // Vitesse de déplacement du personnage
    public float changeDirectionInterval = 0.5f; // Intervalle de temps pour changer de direction

    private Vector3 targetPosition;
    private float timeSinceLastChange = 0f;

    void Start()
    {
        // Définir une position cible initiale aléatoire
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Déplacer le personnage vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Mettre à jour le temps depuis le dernier changement de direction
        timeSinceLastChange += Time.deltaTime;

        // Si le personnage atteint la position cible ou si l'intervalle de changement de direction est atteint
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f || timeSinceLastChange >= changeDirectionInterval)
        {
            // Définir une nouvelle position cible aléatoire
            SetRandomTargetPosition();
            timeSinceLastChange = 0f;
        }
    }

    void SetRandomTargetPosition()
    {
        // Définir une nouvelle position cible aléatoire dans les limites de la carte
        // Assure-toi de définir les limites de ta carte (par exemple, -10 à 10 pour x et z)
        float mapMinX = -10f;
        float mapMaxX = 10f;
        float mapMinZ = -10f;
        float mapMaxZ = 10f;

        float randomX = Random.Range(mapMinX, mapMaxX);
        float randomZ = Random.Range(mapMinZ, mapMaxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }
}