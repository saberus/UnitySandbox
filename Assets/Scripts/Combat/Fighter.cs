using UnityEngine;
using System.Collections;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 10f;
        private Move mover;
        float TimeSinceLastAttack = 0;

        Health target;

        private void Start()
        {
            mover = GetComponent<Move>();
        }

        private void Update()
        {
            TimeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else 
            {
                mover.Cancel();
                AttackBehavour();
            }
        }

        private void AttackBehavour()
        {
            transform.LookAt(target.transform);
            if(TimeSinceLastAttack >= timeBetweenAttacks)
            {
                //This will trigger the (AUTOMATIC) Hit() Animation Event
                TriggerAttack();
                TimeSinceLastAttack = Mathf.Infinity;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private void TriggerStopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) <= weaponRange;
        }

        //Animation Event
        void Hit()
        {
            if(target == null) return;
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null || !targetToTest.IsDead();
        }

        public void Atack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
        }
    }
}
