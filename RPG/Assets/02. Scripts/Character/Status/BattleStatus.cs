using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Character.Equipment;
using RPG.Battle.UI;
using RPG.Battle.Event;
using RPG.Battle.Control;

namespace RPG.Character.Status
{
    public class BattleStatus : MonoBehaviour
    {
        // Component
        [Header("UI")]
        public CharacterUI characterUI;

        [Header("Battle")]
        public int currentHp = 0;
        public bool isDead = false;

        [Header("Status")]
        public Status status;

        // Coroutine
        public IEnumerator perSecCoroutine;

        // ĳ������ ���� ����
        public CombatState currentState;
        public DebuffType currentDebuff;
        public bool isActunableDebuff; // ���� �ൿ �Ұ� ����� �����ΰ�?
        private bool isCursed;
        public List<IEnumerator> debuffList = new List<IEnumerator>(); // ���� ����� ����Ʈ



        // Event
        public PerSecondEvent perSecEvent;
        public TakeDamageEvent takeDamageEvent;

        // Encapsule
        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, status.MaxHp);
                if (characterUI != null)
                {
                    characterUI.UpdateHPUI(currentHp);
                }

                if (currentHp <= 0)
                {
                    Dead();
                }
            }
        }

        private void Awake()
        {
            perSecEvent = new PerSecondEvent();
            takeDamageEvent = new TakeDamageEvent();
            perSecCoroutine = PerSecEvent();
        }

        private void OnEnable()
        {
        }

        protected virtual void Start()
        {
        }

        protected virtual void LateUpdate()
        {

        }
        #region Initialize

        public virtual void Init()
        {
            currentHp = status.MaxHp;
            isDead = false;
        }

        public virtual void Release()
        {
        }
        #endregion

        #region BattleEvent

        public void AddTakeDamageAction(UnityAction<BattleStatus, BattleStatus> action)
        {
            takeDamageEvent.AddListener(action);
        }

        public void AddPerSecAction(UnityAction<BattleStatus> action)
        {
            perSecEvent.AddListener(action);
        }

        public IEnumerator PerSecEvent()
        {
            while (!isDead)
            {
                yield return new WaitForSeconds(1f);
                perSecEvent.Invoke(this);
            }
        }

        #endregion

        public void TakeDamage(int damage, DamagedType type = DamagedType.Normal)
        {
            if (isDead) return;

            int totalDamage = 0;
            totalDamage += damage;
            if (isCursed)
            {
                totalDamage += (int)(damage * 0.5f);
            }

            switch (type)
            {
                case DamagedType.Normal:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString(), type);
                    break;
                case DamagedType.Ciritical:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString() + "!!", type);
                    break;
                case DamagedType.MISS:
                    characterUI.TakeDamageText("MISS~", type);
                    break;
            }
        }

        public void Dead()
        {
            StopAllDebuff();
            RemoveAllDebuff();
            currentState = CombatState.Dead;
            isDead = true;
        }

        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }

        #region Debuff
        public void TakeDebuff(DebuffType type, float duration)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    if (isActunableDebuff) return;
                    { 
                        IEnumerator debuff = TakeStern(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Fear:
                    if (isActunableDebuff) return;
                    {
                        IEnumerator debuff = TakeFear(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Temptation:
                    if (isActunableDebuff) return;
                    {
                        IEnumerator debuff = TakeTemptation(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Paralysis:
                    {
                        IEnumerator debuff = TakeParalysis(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Bloody:
                    {
                        IEnumerator debuff = TakeBloody(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Curse:
                    {
                        IEnumerator debuff = TakeCurse(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
            }
        }

        public void ReStartAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StartCoroutine(debuff);
            }
        }

        public void StopAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StopCoroutine(debuff);
            }
        }

        public void RemoveAllDebuff()
        {
            debuffList.Clear();
        }

        private IEnumerator TakeStern(float duration)
        // ������ �� �ִ�.
        // �ð���ŭ �ൿ �Ұ�
        // ��ø�Ұ�
        {
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Stern;
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
        }


        private IEnumerator TakeFear(float duration)
        // ���� ���� �� �ִ�.
        // �ð���ŭ õõ�� ��󿡰Լ� �־���
        // ��ø �Ұ�
        {
            float defaultMovementSpeed = status.MovementSpeed;
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Fear;
            UpdateMovementSpeed(status.MovementSpeed * 0.7f);
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
        }
        private IEnumerator TakeTemptation(float duration)
        // ��Ȥ ���� �� �ִ�.
        // �ð���ŭ õõ�� �ɾ�� (����X)
        // ��ø �Ұ�
        {
            float defaultMovementSpeed = status.MovementSpeed;
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Temptation;
            UpdateMovementSpeed(status.MovementSpeed * 0.3f);
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
        }

        private IEnumerator TakeParalysis(float duration)
        // ������ �� �ִ�.
        // �ð���ŭ �̵� �Ұ�
        // ��ø ����
        {
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(0);
            yield return new WaitForSeconds(duration);
            UpdateMovementSpeed(defaultMovementSpeed);
        }

        private IEnumerator TakeBloody(float duration)
        // ������ �� �ִ�.
        // �ʴ� ü�� 2% ����
        // ��ø ����
        {
            float time = 0;
            int bloodyDamage = status.MaxHp / 50;
            while (true)
            {
                TakeDamage(bloodyDamage);
                yield return new WaitForSeconds(1f);
                time += 1;
                if (time > duration)
                {
                    break;
                }
            }
        }


        private IEnumerator TakeCurse(float duration)
        // ���� ���� �� �ִ�.
        // �޴� ������ ����
        // ��ø �Ұ�
        {
            isCursed = true;
            yield return new WaitForSeconds(duration);
            isCursed = false;
        }
        #endregion

        public void UpdateBehaviour()
        {
            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
            nav.stoppingDistance = status.AttackRange;

            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
            controller.attack.attackAnimPoint = controller.attack.attackDelay / 2.8f;
        }

        public void UpdateMovementSpeed(float speed)
        {
            status.MovementSpeed = speed;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
        }

        public void UpdateAttackRange(float range)
        {
            status.AttackRange = range;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.stoppingDistance = status.AttackRange;
        }

        public void UpdateAttackSpeed(float speed)
        {
            status.AttackSpeed = speed;

            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
            controller.attack.attackAnimPoint = controller.attack.attackDelay / 2.8f;
        }
    }
}