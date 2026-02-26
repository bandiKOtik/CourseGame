namespace Assets.Scripts.Runtime.SequenceGeneration
{
    public class SequenceMatcher
    {
        public bool IsMatch(string arg1, string arg2)
            => arg1.ToLower() == arg2.ToLower();
    }
}