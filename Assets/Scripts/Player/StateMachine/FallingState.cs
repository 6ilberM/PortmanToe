using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Player.StateMachine
{
    public class FallingState : PlayerBaseState
    {
        public override void EnterState(PlayerController player)
        {

        }

        public override void FixedUpdate(PlayerController player)
        {

        }

        public override void OnCollisionEnter(PlayerController player, Collision2D _collider)
        {
            if (player.GetCollisionsHelper.onGround) { player.ChangeState(player.GroundedState); }
        }

        public override void OnCollisionExit(PlayerController playerController, Collision2D _collision)
        {
            throw new System.NotImplementedException();
        }

        public override void OnCollisionStay2D(PlayerController playerController, Collision2D _collision)
        {
            throw new System.NotImplementedException();
        }

        public override void OnPlayerJump(PlayerController player)
        {

        }

        public override void Update(PlayerController player)
        {
            if (!player.GetCollisionsHelper.onGround)
            {
                player.GetRigibody.velocity += Vector2.up * Physics2D.gravity.y
                * (player.fastFallMultiplier - 1) * Time.deltaTime;
            }
            else { player.ChangeState(player.GroundedState); }
        }
    }
}
