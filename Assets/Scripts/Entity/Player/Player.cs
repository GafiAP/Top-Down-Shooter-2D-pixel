using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : CharacterBase
{
    public static Player instance { get; private set; }
    public event EventHandler<OnBowAttackArrowCountEventArgs> OnBowAttackArrowCount;
    public class OnBowAttackArrowCountEventArgs : EventArgs
    {
        public int arrow;
    }
    public event EventHandler<OnSpellCooldownEventArgs> OnSpellCooldown;
    public class OnSpellCooldownEventArgs : EventArgs
    {
        public float cooldown;
    }
    public event EventHandler<OnWeaponSelectedEventArgs> OnWeaponSelected;
    public class OnWeaponSelectedEventArgs : EventArgs
    {
        public string weaponName;
    }
    public event EventHandler<OnPlayerAttackEventArgs> OnPlayerAttack;
    public class OnPlayerAttackEventArgs : EventArgs
    {
        public WeaponState state;
    }
    public event EventHandler OnPlayerMove;

    public enum WeaponState
    {
        Bow,
        Punch,
        Spell
    }
    public WeaponState state;
    public enum PlayerState
    {
        Move,
        Attack,
        Dead,
        Victory
    }
    public PlayerState playerState;
    public WeaponSO[] weaponSO;
    public Vector2 inputVector;
    public bool AttackMode;
    public int arrow;
    public bool victoryOrDefeat;

    [SerializeField] private int speed;
    [SerializeField] private PlayerSO playerSORef;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject defeatUI;
    [SerializeField] private GameObject battleUI;

    private VictoryOrDefeat victoryOrDefeatUI;
    
    private BackSoundManager backSoundManager;
    private SFX sfx;
    private EnemySpawnPool enemySpawnPool;
    private Vector3 boxsize = new Vector3(0.76f, 0.54f, 0);
    private Vector3 boxsizeOffset = new Vector3(0, 0.62f, 0);
    private Vector3 moveDirection;
    private int score;
    private float spellTimer;
    private bool spellCooldown;

    private void Awake()
    {
        instance = this;
        victoryOrDefeat = false;
    }
    private void Start()
    {
        //set arrow,event,state,bgm,score,attack mode etc
        arrow = 5;
        OnBowAttackArrowCount?.Invoke(this, new OnBowAttackArrowCountEventArgs { arrow = arrow });
        spellCooldown = true;
        score = 0;
        AttackMode = false;
        state = WeaponState.Punch;
        SetProjectileParticlePool(FindFirstObjectByType<SpawnProjectileParticlePool>());
        SetPlayerSO(this.playerSORef);
        SetHealth(GetPlayerSO().health);
        backSoundManager = FindAnyObjectByType<BackSoundManager>();
        backSoundManager.PlayBattleBGM();
        backSoundManager.GetComponent<BackSoundManager>().GetComponent<AudioSource>().loop = true;
        sfx = FindAnyObjectByType<SFX>();
        enemySpawnPool = FindFirstObjectByType<EnemySpawnPool>();
        victoryOrDefeatUI = FindFirstObjectByType<VictoryOrDefeat>();
    }

    void Update()
    {
        //check if enemy = 0 or player health = 0
        CheckVictoryOrDefeat();
        //run spellcooldown
        SpellCooldown();
        //check UI arrow count visual
        CheckArrowVisual();
        //player state
        switch (playerState)
        {
            case PlayerState.Move:
                PlayerMovement();
                SwitchWeapon();
                //weapon state
                switch (state)
                {
                    case WeaponState.Bow:
                        if (GetTarget() != null && arrow > 0)
                        {
                            Attack(WeaponState.Bow);

                        }
                        foreach (var weapon in weaponSO)
                        {
                            if (weapon.weaponName == "Bow")
                            {
                                SetDamage(weapon.weaponDamage);
                                OnWeaponSelected?.Invoke(this, new OnWeaponSelectedEventArgs { weaponName = weapon.weaponName });
                            }
                        }
                        break;
                    case WeaponState.Punch:
                        Attack(WeaponState.Punch);
                        foreach (var weapon in weaponSO)
                        {
                            if (weapon.weaponName == "Punch")
                            {
                                SetDamage(weapon.weaponDamage);
                                OnWeaponSelected?.Invoke(this, new OnWeaponSelectedEventArgs { weaponName = weapon.weaponName });
                            }
                        }
                        break;
                    case WeaponState.Spell:

                        if (GetTarget() != null && !spellCooldown)
                        {
                            Attack(WeaponState.Spell);

                        }
                        foreach (var weapon in weaponSO)
                        {

                            if (weapon.weaponName == "FireBallSpell")
                            {
                                SetDamage(weapon.weaponDamage);
                                OnWeaponSelected?.Invoke(this, new OnWeaponSelectedEventArgs { weaponName = weapon.weaponName });
                            }
                        }
                        break;
                }
                break;
            case PlayerState.Attack:
                AttackMode = true;
                break;
            case PlayerState.Dead:
                victoryOrDefeat = true;
                break;
            case PlayerState.Victory:
                victoryOrDefeat = true;
                break;
        }
        
        
    }
    public int CheckPlayerToEnemyDirection()
    {
        //check target direction
        if (GetTarget() != null)
        {
            if (transform.position.y < GetTarget().transform.position.y)
            {
                //player dibawah enemy
                return 1;
            }
            else
            {
                //player diatas enemy
                return -1;
            }
        }
        else
        {
            return 0;
        }
    }

    //Spawn arrow
    public void SpawnArrow()
    {
        var arrow = GetProjectileParticlePool()._arrowPool.Get();
        arrow.transform.SetParent(GetProjectileParticlePool().transform);
        arrow.transform.position = this.transform.position;
        arrow.targetArrow = GetTarget();
    }
    //release arrow
    public void ReleaseArrow(Arrow arrow)
    {
        GetProjectileParticlePool()._arrowPool.Release(arrow);
    }
    //spawn fireball Spell
    public void SpawnFireBallSpell()
    {
            var fireballSpell = GetProjectileParticlePool()._fireballSpellPool.Get();
            fireballSpell.setCharacterBaseVariable(this);
            fireballSpell.transform.SetParent(GetProjectileParticlePool().transform);
            fireballSpell.transform.position = this.transform.position;
            fireballSpell.targetLastPosition = this.GetTarget();
            fireballSpell.explode = false;
    }
    //release Fireball Spell
    public void ReleaseFireBallSpell(FireBallSpell fireballSpell)
    {
        GetProjectileParticlePool()._fireballSpellPool.Release(fireballSpell);
    }
    //Add this player score from enemy
    public void addScore(int score)
    {
        this.score += score;
    }
    //Get this player score
    public int GetScore()
    {
        return this.score;
    }
    //check if enemy = 0 or player health = 0
    public void CheckVictoryOrDefeat()
    {
        if (enemySpawnPool._firePool.CountActive == 0 && enemySpawnPool._wraithPool.CountActive == 0 && !victoryOrDefeat)
        {
            backSoundManager.PlayCompletionLevelBGM();
            playerState = PlayerState.Victory;
            backSoundManager.GetComponent<BackSoundManager>().GetComponent<AudioSource>().loop = false;
            battleUI.SetActive(false);
            victoryUI.SetActive(true);
            victoryOrDefeatUI.UpdateVictoryOrDefeatScore();
            Time.timeScale = 0f;
        }
        if (Player.instance.GetHealth() <= 0 && !victoryOrDefeat)
        {
            playerState = PlayerState.Dead;
            backSoundManager.PlayGameOverBGM();
            backSoundManager.GetComponent<BackSoundManager>().GetComponent<AudioSource>().loop = false;
            battleUI.SetActive(false);
            defeatUI.SetActive(true);
            victoryOrDefeatUI.UpdateVictoryOrDefeatScore();
            Time.timeScale = 0f;
        }
    }
    //reset cooldown fireball spell
    public void resetCooldownSpell()
    {
        spellTimer = 0;
        spellCooldown = true;
    }
    //set arrow visual on UI with this arrow
    public void CheckArrowVisual()
    {
        OnBowAttackArrowCount?.Invoke(this, new OnBowAttackArrowCountEventArgs { arrow = this.arrow });
    }
    //fireball spell cooldown
    private void SpellCooldown()
    {
        if (spellCooldown)
        {
            spellTimer += Time.deltaTime;
            OnSpellCooldown?.Invoke(this, new OnSpellCooldownEventArgs { cooldown = spellTimer});
            if (spellTimer >= weaponSO[2].cooldown)
            {
                spellCooldown = false;
            }
        }
    }
    //check input from keyboard to switch weapon
    private void SwitchWeapon()
    {
        if (AttackMode == false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                state = WeaponState.Punch;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                state = WeaponState.Bow;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                state = WeaponState.Spell;
            }
        }
    }
    //Attack to enemy 
    private void Attack(WeaponState state)
    {
        if (Input.GetKeyDown(KeyCode.Space) && AttackMode == false)
        {
            playerState = PlayerState.Attack;
            OnPlayerAttack?.Invoke(this, new OnPlayerAttackEventArgs { state = state});
        }
        
    }
    //Player movement method
    private void PlayerMovement()
    {
        
        if (!AttackMode)
        {
            inputVector = new Vector2(0, 0);
            inputVector.x = Input.GetAxisRaw("Horizontal");
            inputVector.y = Input.GetAxisRaw("Vertical");
            OnPlayerMove?.Invoke(this, EventArgs.Empty);

            inputVector = inputVector.normalized;

            moveDirection = new Vector3(inputVector.x, inputVector.y, 0);

            bool checkMoveAble = !Physics2D.BoxCast(transform.position - boxsizeOffset, boxsize, 0f, (Vector2)moveDirection,.1f,layerMask);

            if (!checkMoveAble)
            {
                Vector3 moveDirectionX = new Vector3(inputVector.x, 0, 0).normalized;
                checkMoveAble = moveDirection.x != 0 && !Physics2D.BoxCast(transform.position - boxsizeOffset, boxsize, 0f, moveDirectionX, .1f, layerMask);
                if (checkMoveAble)
                {
                    moveDirection = moveDirectionX;
                }
                else
                {
                    Vector3 moveDirectionY = new Vector3(0, inputVector.y, 0).normalized;
                    checkMoveAble = moveDirection.y != 0 && !Physics2D.BoxCast(transform.position - boxsizeOffset, boxsize, 0f, moveDirectionY, .1f, layerMask);
                    if (checkMoveAble)
                    {
                        moveDirection = moveDirectionY;
                    }
                    else
                    {
                        //Cant move
                    }
                }
            }
            if (checkMoveAble)
            {
                transform.position += moveDirection * Time.deltaTime * speed;
            }
            Flip();

        }

    }
    //flip player rotation
    private void Flip()
    {
        if (inputVector.x == -1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0, 0f));
        }
    }

}
