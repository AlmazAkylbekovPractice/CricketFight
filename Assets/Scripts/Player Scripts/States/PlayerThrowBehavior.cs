using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowBehavior : IPlayerBehavior
{
    public void Enter(Player player)
    {
        player.isThrowing = true;
        player.isReady = false;
        player.anim.SetBool(player.THROWING_TAG, true);
    }

    public void Exit(Player player)
    {
        player.isThrowing = false;
        player.isReady = true;
        player.anim.SetBool(player.THROWING_TAG, false);
    }

    public void FixedUpdate(Player player)
    {

    }

    public void Update(Player player)
    {
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
        {
            player.SetBehaviorIdle();
        }

        float time = player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        Debug.Log(time);

        if (time >= 0.54f)
        {
            if (player.currentBall == null)
            {
                player.InstantiateBall();
            }
        }
        else if (time >= 0.1f && time < 0.6f)
        {
            player.currentBall = null;
        }


    }

    private GameObject Instantiate(GameObject ballPrefab, Vector3 position, Quaternion identity)
    {
        throw new NotImplementedException();
    }
}
