using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{       
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        //bool shouldChase = false;
         Health health;
         GameObject player;
         Fighter fighter;

         /// <summary>
         /// Start is called on the frame when a script is enabled just before
         /// any of the Update methods is called the first time.
         /// </summary>
         void Start()
         {
             health = GetComponent<Health>();
             player = GameObject.FindWithTag("Player");
             fighter = GetComponent<Fighter>();
         }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(health.IsDead()) return;
            //player = GameObject.FindWithTag("Player");
            if(ShouldChase(player))
            {
                InteractWithCombat();
                //InteractWithMovement();
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool ShouldChase(GameObject player)
        {
            if (player == null) return false;
            return Vector3.Distance(player.transform.position, transform.position) <= chaseDistance;
        }

        private bool InteractWithCombat()
        {
            GameObject target = player;
            if(target != null || fighter.CanAttack(target.gameObject)) 
            {
                fighter.Atack(target.gameObject);
                return true;
            } 
            else 
            {
                return false;
            }            
        }
    }
}
