using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StaminaWheel : MonoBehaviour
{
    private float stamina; 
    [SerializeField] private float maxStamina = 100f; 

    [SerializeField] private Image greenWheel;
    [SerializeField] private Image redWheel;

    private bool staminaExhausted; 
    
    void Start()
    {
        stamina = maxStamina;
        
    }   

   
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && !staminaExhausted) 
        {
            if(stamina > 0)
            {
               stamina -= 30 * Time.deltaTime;      
            } else
            {
                greenWheel.enabled = false; 
                staminaExhausted = true; 
            }

            redWheel.fillAmount = (stamina / maxStamina + 0.07f); 
        } else 
        {
            if(stamina < maxStamina) 
            {
                stamina += 30 * Time.deltaTime; 
            } else 
            {
                greenWheel.enabled = true; 
                staminaExhausted = false; 
            }

            redWheel.fillAmount = (stamina / maxStamina);
        }

        greenWheel.fillAmount = (stamina / maxStamina); 
    }
} 
