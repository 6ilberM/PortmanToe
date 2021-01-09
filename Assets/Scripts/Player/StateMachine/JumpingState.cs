using System.Collections;
using UnityEngine;

namespace _Game.Player.StateMachine
{
    public class JumpingState : PlayerBaseState
    {
        public override void EnterState(PlayerController player)
        {
            //throw new System.NotImplementedException();
        }

        public override void FixedUpdate(PlayerController player)
        {
            player.GetRigibody.velocity = Vector2.SmoothDamp(player.GetRigibody.velocity, player.targetMoveSpeed, ref player.m_VelocityVar, player.SmoothingFactor);
        }

        public override void OnCollisionEnter(PlayerController player, Collision2D _collider)
        {
            player.ChangeState(player.GroundedState);
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
            //ToDo: Create class with mutliple Overlap Checks
            throw new System.NotImplementedException();
        }
    }
}