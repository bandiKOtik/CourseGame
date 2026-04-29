using Assets.Scripts.Meta.Features.Wallet;
using Assets.Scripts.Runtime.Gameplay.EntitiesCore.Factory;
using Assets.Scripts.Runtime.Gameplay.Features.InputManagement;
using Assets.Scripts.Runtime.Gameplay.Features.TeamsFeature;
using Assets.Scripts.Utilities.StateMachineCore;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Features.AI.States
{
    public class InputPlantMineState : State, IUpdateableState
    {
        private readonly EntitiesFactory _factory;
        private readonly IInputService _inputService;
        private readonly WalletService _walletService;
        private IReadOnlyDictionary<CurrencyTypes, int> _minePrice;

        public InputPlantMineState(
            EntitiesFactory factory,
            IInputService inputService,
            WalletService walletService,
            IReadOnlyDictionary<CurrencyTypes, int> minePrice)
        {
            _factory = factory;
            _inputService = inputService;
            _walletService = walletService;
            _minePrice = minePrice;
        }

        public void Update(float deltaTime)
        {
            if (_inputService.AttackRequest)
            {
                bool enoughToPlant = true;

                foreach (var price in _minePrice)
                {
                    if (_walletService.Enough(price.Key, price.Value) == false)
                    {
                        enoughToPlant = false;
                        break;
                    }
                }

                if (enoughToPlant == false)
                    return;

                foreach (var price in _minePrice)
                    _walletService.Spend(price.Key, price.Value);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                    _factory.CreateContactTrigger(Teams.MainHero, hit.point);
            }
        }
    }
}