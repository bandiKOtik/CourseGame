using Assets.Scripts.Runtime.Gameplay.EntitiesCore;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.MainBaseBuilding;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputPlantMineState : State, IUpdateableState
    {
        private Entity _source;

        private readonly IInputService _inputService;
        private readonly EntitiesFactory _factory;

        public InputPlantMineState(Entity source, EntitiesFactory factory, IInputService inputService)
        {
            _source = source;
            _factory = factory;
            _inputService = inputService;
        }

        public void Update(float deltaTime)
        {
            if (_inputService.AttackRequest)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                    _factory.CreateContactTrigger(Teams.MainHero, hit.point);
            }
        }
    }
}
