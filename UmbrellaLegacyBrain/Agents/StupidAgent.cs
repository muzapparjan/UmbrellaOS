using UmbrellaLegacyBrain.Interfaces;

namespace UmbrellaLegacyBrain.Agents
{
    public sealed class StupidAgent : IAgent
    {
        public IEnumerable<ISensor> Sensors => throw new NotImplementedException();

        public IEnumerable<IBody> Bodies => throw new NotImplementedException();

        public IBrain Brain => throw new NotImplementedException();

        public IEnvironment Reconstruct(IData data)
        {
            throw new NotImplementedException();
        }

        public IData Sense(IEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}