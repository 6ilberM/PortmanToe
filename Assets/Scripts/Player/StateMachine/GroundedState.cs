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
            //ToDo: Replace this with  fancy check overlap class
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
            player.GetRigibody.AddForce(PlayerController.jumpForce * Vector3.up, ForceMode2D.Impulse);
            player.ChangeState(player.JumpingState);
        }

        public override void Update(PlayerController player)
        {
            throw new System.NotImplementedException();
        }
    }
}
