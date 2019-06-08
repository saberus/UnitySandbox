using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
     [RequireComponent(typeof(Fighter))]
    public class PlayerController : MonoBehaviour
    {
        Health health;
        Fighter fighter;
        void Start() {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
        } 

        // Update is called once per frame
        void Update()
        {
            if(health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null || !fighter.CanAttack(target.gameObject)) continue;
                if (Input.GetMouseButtonDown(1))
                {
                    fighter.Atack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if(Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
                {
                    GetComponent<Move>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
