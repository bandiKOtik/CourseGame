namespace Assets.Scripts.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.BodyCollider BodyColliderComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.BodyCollider>();
		public UnityEngine.CapsuleCollider BodyCollider
			=> BodyColliderComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddBodyCollider(UnityEngine.CapsuleCollider value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.BodyCollider() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask ContactsDetectingMaskComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask>();
		public UnityEngine.LayerMask ContactsDetectingMask
			=> ContactsDetectingMaskComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddContactsDetectingMask(UnityEngine.LayerMask value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsColliderBuffer ContactsColliderBufferComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsColliderBuffer>();
		public Assets.Scripts.Utilities.Buffer<UnityEngine.Collider> ContactsColliderBuffer
			=> ContactsColliderBufferComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddContactsColliderBuffer(Assets.Scripts.Utilities.Buffer<UnityEngine.Collider> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsColliderBuffer() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer ContactsEntitiesBufferComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer>();
		public Assets.Scripts.Utilities.Buffer<Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity> ContactsEntitiesBuffer
			=> ContactsEntitiesBufferComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddContactsEntitiesBuffer(Assets.Scripts.Utilities.Buffer<Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.DeathMask DeathMaskComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.DeathMask>();
		public UnityEngine.LayerMask DeathMask
			=> DeathMaskComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDeathMask(UnityEngine.LayerMask value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.DeathMask() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Sensors.IsTouchedDeathMask IsTouchedDeathMaskComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Sensors.IsTouchedDeathMask>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> IsTouchedDeathMask
			=> IsTouchedDeathMaskComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchedDeathMask()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.IsTouchedDeathMask() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsTouchedDeathMask(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Sensors.IsTouchedDeathMask() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.CanRotate CanRotateComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.CanRotate>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition CanRotate
			=> CanRotateComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCanRotate(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.RotationFeature.CanRotate() {Value = value});
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
		public Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.CanMove CanMoveComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.CanMove>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition CanMove
			=> CanMoveComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCanMove(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.CanMove() {Value = value});
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
		public Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.IsMoving IsMovingComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.IsMoving>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> IsMoving
			=> IsMovingComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsMoving()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.IsMoving() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsMoving(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.MovementFeature.IsMoving() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.CurrentHealth CurrentHealthComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.CurrentHealth>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> CurrentHealth
			=> CurrentHealthComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.CurrentHealth() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.CurrentHealth() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MaxHealth MaxHealthComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MaxHealth>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> MaxHealth
			=> MaxHealthComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MaxHealth() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MaxHealth() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.IsDead IsDeadComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.IsDead>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> IsDead
			=> IsDeadComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsDead()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.IsDead() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddIsDead(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.IsDead() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustDie MustDieComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustDie>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition MustDie
			=> MustDieComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMustDie(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustDie() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease MustSelfReleaseComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition MustSelfRelease
			=> MustSelfReleaseComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMustSelfRelease(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime DeathProcessCurrentTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessCurrentTime
			=> DeathProcessCurrentTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime DeathProcessInitialTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessInitialTime
			=> DeathProcessInitialTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.InDeathProcess InDeathProcessComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.InDeathProcess>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> InDeathProcess
			=> InDeathProcessComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.InDeathProcess() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.InDeathProcess() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath DisableCollidersOnDeathComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath>();
		public System.Collections.Generic.List<UnityEngine.Collider> DisableCollidersOnDeath
			=> DisableCollidersOnDeathComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDisableCollidersOnDeath()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath() { Value = new System.Collections.Generic.List<UnityEngine.Collider>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddDisableCollidersOnDeath(System.Collections.Generic.List<UnityEngine.Collider> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.LifeCycle.DisableCollidersOnDeath() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.CanApplyDamage CanApplyDamageComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.CanApplyDamage>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition CanApplyDamage
			=> CanApplyDamageComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCanApplyDamage(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.CanApplyDamage() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageRequest TakeDamageRequestComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageRequest>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageRequest
			=> TakeDamageRequestComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageRequest() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest(Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageRequest() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageEvent TakeDamageEventComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageEvent>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageEvent
			=> TakeDamageEventComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageEvent() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent(Assets.Scripts.Utilities.Reactive.ReactiveEvent<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.DamageFeature.TakeDamageEvent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage BodyContactDamageComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> BodyContactDamage
			=> BodyContactDamageComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddBodyContactDamage()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddBodyContactDamage(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.ContactTakeDamage.BodyContactDamage() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackRequest StartAttackRequestComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackRequest>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent StartAttackRequest
			=> StartAttackRequestComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackRequest()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackRequest() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackRequest(Assets.Scripts.Utilities.Reactive.ReactiveEvent value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackRequest() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackEvent StartAttackEventComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackEvent>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent StartAttackEvent
			=> StartAttackEventComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackEvent()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackEvent() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddStartAttackEvent(Assets.Scripts.Utilities.Reactive.ReactiveEvent value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.StartAttackEvent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.CanStartAttack CanStartAttackComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.CanStartAttack>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition CanStartAttack
			=> CanStartAttackComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddCanStartAttack(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.CanStartAttack() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.EndAttackEvent EndAttackEventComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.EndAttackEvent>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent EndAttackEvent
			=> EndAttackEventComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddEndAttackEvent()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.EndAttackEvent() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddEndAttackEvent(Assets.Scripts.Utilities.Reactive.ReactiveEvent value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.EndAttackEvent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime AttackProcessInitialTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> AttackProcessInitialTime
			=> AttackProcessInitialTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessInitialTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessInitialTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessInitialTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime AttackProcessCurrentTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> AttackProcessCurrentTime
			=> AttackProcessCurrentTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessCurrentTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackProcessCurrentTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackProcessCurrentTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackProcess InAttackProcessComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackProcess>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> InAttackProcess
			=> InAttackProcessComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInAttackProcess()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackProcess() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInAttackProcess(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackProcess() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayTime AttackDelayTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> AttackDelayTime
			=> AttackDelayTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent AttackDelayEndEventComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent AttackDelayEndEvent
			=> AttackDelayEndEventComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackDelayEndEvent(Assets.Scripts.Utilities.Reactive.ReactiveEvent value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackDelayEndEvent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.InstantAttackDamage InstantAttackDamageComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.InstantAttackDamage>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> InstantAttackDamage
			=> InstantAttackDamageComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInstantAttackDamage()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InstantAttackDamage() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInstantAttackDamage(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InstantAttackDamage() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.ShootPoint ShootPointComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.ShootPoint>();
		public UnityEngine.Transform ShootPoint
			=> ShootPointComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddShootPoint(UnityEngine.Transform value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.ShootPoint() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.MustCancelAttack MustCancelAttackComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.MustCancelAttack>();
		public Assets.Scripts.Utilities.Conditions.ICompositeCondition MustCancelAttack
			=> MustCancelAttackComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddMustCancelAttack(Assets.Scripts.Utilities.Conditions.ICompositeCondition value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.MustCancelAttack() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCanceledEvent AttackCanceledEventComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCanceledEvent>();
		public Assets.Scripts.Utilities.Reactive.ReactiveEvent AttackCanceledEvent
			=> AttackCanceledEventComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCanceledEvent()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCanceledEvent() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveEvent() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCanceledEvent(Assets.Scripts.Utilities.Reactive.ReactiveEvent value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCanceledEvent() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime AttackCooldownInitialTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> AttackCooldownInitialTime
			=> AttackCooldownInitialTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownInitialTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownInitialTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownInitialTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime AttackCooldownCurrentTimeComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> AttackCooldownCurrentTime
			=> AttackCooldownCurrentTimeComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownCurrentTime()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddAttackCooldownCurrentTime(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Single> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.AttackCooldownCurrentTime() {Value = value});
		public Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackCooldown InAttackCooldownComp
			=> GetComponent<Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackCooldown>();
		public Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> InAttackCooldown
			=> InAttackCooldownComp.Value;
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInAttackCooldown()
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackCooldown() { Value = new Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		public Assets.Scripts.Runtime.Gameplay.EntitiesCore.Entity AddInAttackCooldown(Assets.Scripts.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
			=> AddComponent(new Assets.Scripts.Runtime.Gameplay.Features.Attack.InAttackCooldown() {Value = value});
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
