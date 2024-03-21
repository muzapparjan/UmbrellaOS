namespace UmbrellaLegacyBrain.Interfaces
{
    public interface IAgent
    {
        public IEnumerable<ISensor> Sensors { get; }
        public IEnumerable<IBody> Bodies { get; }
        public IBrain Brain { get; }

        public IEnumerable<IData> Sense(IEnvironment env);
        public IEnvironment Reconstruct(IEnumerable<IData> data);
    }
}