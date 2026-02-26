using Assets.Scripts.Utilities.DataManagement;
using Assets.Scripts.Utilities.DataManagement.DataProviders;
using System.Collections.Generic;

namespace Assets.Scripts.Meta.Features.LevelsProgression
{
    public class LevelsProgressionService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private const int FirstLevel = 1;
        private readonly List<int> _completedLevels = new();

        public LevelsProgressionService(PlayerDataProvider provider)
        {
            provider.RegisterReader(this);
            provider.RegisterWriter(this);
        }

        public bool IsLevelCompleted(int index)
            => _completedLevels.Contains(index);

        public void AddLevelToCompleted(int index)
        {
            if (_completedLevels.Contains(index))
                return;

            _completedLevels.Add(index);
        }

        public bool CanPlay(int index)
            => index == FirstLevel || PreviousLevelCompleted(index);

        private bool PreviousLevelCompleted(int index)
            => _completedLevels.Contains(index - 1);

        public void ReadFrom(PlayerData data)
        {
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }

        public void WriteTo(PlayerData data)
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }
    }
}
