namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationDirection RotationDirectionComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationDirection>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> RotationDirection
			=> RotationDirectionComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationDirection() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection(Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationDirection() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationSpeed RotationSpeedComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationSpeed>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> RotationSpeed
			=> RotationSpeedComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationSpeed() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.RotationSpeed() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveDirection MoveDirectionComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveDirection>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection
			=> MoveDirectionComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveDirection() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection(Assets.Scripts.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveDirection() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveSpeed MoveSpeedComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveSpeed>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> MoveSpeed
			=> MoveSpeedComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.MoveSpeed() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Common.TransformComponent TransformComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Common.TransformComponent>();
		public UnityEngine.Transform Transform
			=> TransformComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddTransform(UnityEngine.Transform value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Common.TransformComponent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Common.RigidbodyComponent RigidbodyComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Common.RigidbodyComponent>();
		public UnityEngine.Rigidbody Rigidbody
			=> RigidbodyComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddRigidbody(UnityEngine.Rigidbody value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Common.RigidbodyComponent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Common.CharacterControllerComponent CharacterControllerComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Common.CharacterControllerComponent>();
		public UnityEngine.CharacterController CharacterController
			=> CharacterControllerComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCharacterController(UnityEngine.CharacterController value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Common.CharacterControllerComponent() {Value = value});
	}
}
