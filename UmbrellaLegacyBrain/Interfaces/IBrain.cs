namespace UmbrellaLegacyBrain.Interfaces
{
    public interface IBrain
    {
        public IEnvironment Imagine(IData data);
        public IData Describe(IEnvironment env);
        public IEnvironment Wish(IEnvironment env);
        public float Love(IEnvironment env);
        public float Compare(IEnvironment a, IEnvironment b);
    }
}