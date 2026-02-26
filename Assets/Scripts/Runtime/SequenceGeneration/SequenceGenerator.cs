using Assets.Scripts.Runtime.SequenceGeneration;
using UnityEngine;

namespace Assets.Scripts.Utilities.Sequence
{
    public class SequenceGenerator : ISequenceGenerator
    {
        [Min(1)] private readonly int _minLength, _maxLength;
        private readonly string _availableValues;

        public SequenceGenerator(int minLength, int maxLength, string availableValues)
        {
            _minLength = minLength;

            if (maxLength <= minLength)
                _maxLength = minLength;
            else
                _maxLength = maxLength;

            _availableValues = availableValues;
        }

        public string GenerateSequence()
        {
            int iteractions = Random.Range(_minLength, _maxLength);
            char[] values = _availableValues.ToCharArray();
            char[] result = new char[iteractions];

            for (int i = 0; i < iteractions; i++)
                result[i] = values[Random.Range(0, values.Length)];

            return new string(result);
        }
    }
}