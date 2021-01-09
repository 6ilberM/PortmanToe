using System.Collections;
using UnityEngine;

namespace _Game.Player.StateMachine
{
    public class JumpingState : PlayerBaseState
    {
        public override void EnterState(PlayerController player)
        {
            player.CoyoteTimer = 0;
            player.GetRigibody.velocity *= (Vector2.right);
            player.GetRigibody.AddForce(player.GetJumpForce() * Vector3.up, ForceMode2D.Impulse);
        }

        public override void FixedUpdate(PlayerController player)
        {
            if (player.targetMoveSpeed.magnitude != 0)
            {
                //Figure out how to lower the ammount of control we have without decreasing our control in the air
                player.GetRigibody.velocity = Vector2.SmoothDamp(player.GetRigibody.velocity, player.targetMoveSpeed, ref player.m_VelocityVar, player.SmoothingFactor);
            }
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
            // ToDo: Other Particle Effect

            //perhaps check if there is something we can climb or in ??? water?? idk dude haha
            //throw new System.NotImplementedException();
        }

        public override void Update(PlayerController player)
        {
            if (player.GetRigibody.velocity.y < 0)
            {
                player.ChangeState(player.FallingState);
            }
        }
    }
}