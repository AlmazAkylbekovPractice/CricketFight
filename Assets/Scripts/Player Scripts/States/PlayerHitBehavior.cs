using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBehavior : IPlayerBehavior
{
    public void Enter(Player player)
    {
        GameManager.Instance.HittedBall();

        player.isHitting = true;
        player.isReady = false;
        player.anim.SetBool(player.HITTING_TAG, true);
    }

    public void Exit(Player player)
    {
        player.isHitting = false;
        player.isReady = true;
        player.anim.SetBool(player.HITTING_TAG, false);
    }

    public void FixedUpdate(Player player)
    {

    }

    public void Update(Player player)
    {
        if (player.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            player.SetBehaviorIdle();
        }
    }
}
