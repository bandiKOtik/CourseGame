namespace Assets.Scripts.Runtime.Sequence
{
    public class SequenceMatcher
    {
        public bool IsMatch(string arg1, string arg2)
            => arg1.ToLower() == arg2.ToLower();
    }
}