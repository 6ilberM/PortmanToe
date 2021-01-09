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
            if (player.targetMoveSpeed.magnitude != 0)
            {
                player.GetRigibody.velocity = Vector2.SmoothDamp(player.GetRigibody.velocity, player.targetMoveSpeed * new Vector2(.76f, 1), ref player.m_VelocityVar, player.SmoothingFactor);
            }
        }

        public override void OnCollisionEnter(PlayerController player, Collision2D _collider)
        {
            if (player.GetCollisionsHelper.onGround)
            {
                player.ChangeState(player.GroundedState);
            }
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
            if (player.CoyoteTimer > 0)
            {
                player.ChangeState(player.JumpingState);
            }
        }

        public override void Update(PlayerController player)
        {
            if (player.CoyoteTimer >= 0) { player.CoyoteTimer -= Time.deltaTime; }

            if (!player.GetCollisionsHelper.onGround)
            {
                player.GetRigibody.velocity += Vector2.up * Physics2D.gravity.y
                * (player.fastFallMultiplier - 1) * Time.deltaTime;
            }
            else { player.ChangeState(player.GroundedState); }
        }
    }
}
