using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Battle.UI;
using RPG.Character.Equipment;
using RPG.Battle.Event;
using UnityEngine.Events;

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

        // 캐릭터의 현재 상태
        public CombatState currentState;
        public DebuffType currentDebuff;
        public bool isActunableDebuff; // 현재 행동 불가 디버프 상태인가?

        // Event
        PerSecondEvent perSecEvent;
        TakeDamageEvent takeDamageEvent;

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

        public void AddTakeDamageAction(UnityAction action)
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

            takeDamageEvent.Invoke();

            switch (type)
            {
                case DamagedType.Normal:
                    CurrentHp -= damage;
                    characterUI.TakeDamageText(damage.ToString(), type);
                    break;
                case DamagedType.Ciritical:
                    CurrentHp -= damage;
                    characterUI.TakeDamageText(damage.ToString() + "!!", type);
                    break;
                case DamagedType.MISS:
                    characterUI.TakeDamageText("MISS~", type);
                    break;
            }
        }

        public void Dead()
        {
            Debug.Log(name + "은 죽었다");
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
                    StartCoroutine(TakeStern(duration));
                    break;
                case DebuffType.Fear:
                    if (isActunableDebuff) return;
                    StartCoroutine(TakeFear(duration));
                    break;
                case DebuffType.Temptation:
                    if (isActunableDebuff) return;
                    StartCoroutine(TakeTemptation(duration));
                    break;
                case DebuffType.Paralysis:
                    StartCoroutine(TakeParalysis(duration));
                    break;
                case DebuffType.Bloody:
                    StartCoroutine(TakeBloody(duration));
                    break;
                case DebuffType.Curse:
                    StartCoroutine(TakeCurse(duration));
                    break;
            }
        }

        public IEnumerator TakeStern(float duration)
        // 기절할 수 있다.
        // 시간만큼 행동 불가
        // 중첩불가
        {
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Stern;
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
        }


        public IEnumerator TakeFear(float duration)
        // 공포 당할 수 있다.
        // 시간만큼 천천히 대상에게서 멀어짐
        // 중첩 불가
        {
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Fear;
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
        }
        public IEnumerator TakeTemptation(float duration)
        // 유혹 당할 수 있다.
        // 시간만큼 천천히 걸어옴 (공격X)
        // 중첩 불가
        {
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Temptation;
            yield return new WaitForSeconds(duration);
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
        }

        public IEnumerator TakeParalysis(float duration)
        // 마비할 수 있다.
        // 시간만큼 이동 불가
        // 중첩 가능
        {
            float defaultMovementSpeed = status.MovementSpeed;
            status.MovementSpeed = 0;
            yield return new WaitForSeconds(duration);
            status.MovementSpeed = defaultMovementSpeed;
        }

        public IEnumerator TakeBloody(float duration)
        // 출혈할 수 있다.
        // 초당 체력 2% 감소
        // 중첩 가능
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


        public IEnumerator TakeCurse(float duration)
        // 저주 당할 수 있다.
        // 시간만큼 현재 공격력 감소(15%씩)
        // 중첩 가능
        {
            int decreseDamage = (int)(status.AttackDamage * 0.15f);
            status.AttackDamage -= decreseDamage;
            yield return new WaitForSeconds(duration);
            status.AttackDamage += decreseDamage;
        }
        #endregion
    }
}