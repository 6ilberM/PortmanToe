using UnityEngine;
namespace _Game.Player.StateMachine
{
    public abstract class PlayerBaseState
    {
        public abstract void EnterState(PlayerController player);
        public abstract void Update(PlayerController player);
        public abstract void FixedUpdate(PlayerController player);
        public abstract void OnPlayerJump(PlayerController player);
        public abstract void OnCollisionEnter(PlayerController player, UnityEngine.Collision2D _collider);
        public abstract void OnCollisionExit(PlayerController playerController, Collision2D _collision);
        public abstract void OnCollisionStay2D(PlayerController playerController, Collision2D _collision);
    }
}
