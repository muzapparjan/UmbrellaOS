namespace UmbrellaLegacyBrain.Interfaces
{
    public interface ISensor
    {
        public IData? Sense();
    }
    public interface ISensor<T> : ISensor where T : IData
    {
        public new T? Sense();
    }
}