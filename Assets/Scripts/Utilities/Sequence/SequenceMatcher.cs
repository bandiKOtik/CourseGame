namespace Assets.Scripts.Utilities.Sequence
{
    public class SequenceMatcher
    {
        public bool IsMatch(string original, string input)
            => original.ToLower() == input.ToLower();
    }
}