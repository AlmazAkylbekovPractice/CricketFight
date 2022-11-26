using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Player : MonoBehaviour
{
    public Animator anim;
    public CharacterController control;

    //State Pattern
    private Dictionary<Type, IPlayerBehavior> behaviorsMap;
    private IPlayerBehavior currentBehavior;

    public string HITTING_TAG = "isHitting";
    public string THROWING_TAG = "isThrowing";

    //States
    public bool isHitting;
    public bool isThrowing;
    public bool isReady;

    private void Start()
    {
        LoadPlayer();
    }

    private void LoadPlayer()
    {
        SetupManagersPlayer();
        LoadComponents();
        InitializeBehabiors();
        SetDefaultBehavior();
    }

    private void LoadComponents()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<CharacterController>();
    }

    private void InitializeBehabiors()
    {
        this.behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        this.behaviorsMap[typeof(PlayerHitBehavior)] = new PlayerHitBehavior();
        this.behaviorsMap[typeof(PlayerThrowBehavior)] = new PlayerThrowBehavior();
        this.behaviorsMap[typeof(PlayerBehaviorIdle)] = new PlayerBehaviorIdle();
    }

    private void SetDefaultBehavior()
    {
        this.SetBehaviorIdle();
    }

    private void SetupManagersPlayer()
    {
        InputManager.Instance.SetPlayer(this);
        GameManager.Instance.SetPlayer(this);
    }


    private void Update()
    {
        if (this.currentBehavior != null)
        {
            this.currentBehavior.Update(this);
        }
    }

    private void FixedUpdate()
    {
        if (this.currentBehavior != null)
        {
            this.currentBehavior.FixedUpdate(this);
        }
    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (this.currentBehavior != null)
            this.currentBehavior.Exit(this);

        this.currentBehavior = newBehavior;
        this.currentBehavior.Enter(this);
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        return this.behaviorsMap[typeof(T)];
    }

    public void SetBehaviorIdle()
    {
        var behavior = this.GetBehavior<PlayerBehaviorIdle>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorHit()
    {
        var behavior = this.GetBehavior<PlayerHitBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorThrow()
    {
        var behavior = this.GetBehavior<PlayerThrowBehavior>();
        this.SetBehavior(behavior);
    }


}
