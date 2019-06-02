namespace UnityEngine.PostProcessing
{
    public sealed class MinA : PropertyAttribute
    {
        public readonly float min;

        public MinA(float min)
        {
            this.min = min;
        }
    }
}
