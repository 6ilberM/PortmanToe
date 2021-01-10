using UnityEngine;

namespace _Game.Player.StateMachine
{
    public class GroundedState : PlayerBaseState
    {
        public override void EnterState(PlayerController player) { player.moveSpeed = PlayerController.GroundSpeed; }

        public override void FixedUpdate(PlayerController player)
        {
            player.GetRigibody.velocity = Vector2.SmoothDamp(player.GetRigibody.velocity, new Vector2(player.targetMoveSpeed.x * 0.69f, player.targetMoveSpeed.y), ref player.m_VelocityVar, player.SmoothingFactor);
        }

        public override void OnCollisionEnter(PlayerController player, Collision2D _collider)
        {
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
            player.ChangeState(player.JumpingState);
        }

        public override void Update(PlayerController player)
        {
            player.CoyoteTimer = PlayerController.CoyoteTime;

            if (!player.GetCollisionsHelper.onGround) { player.ChangeState(player.FallingState); }
        }
    }
}
