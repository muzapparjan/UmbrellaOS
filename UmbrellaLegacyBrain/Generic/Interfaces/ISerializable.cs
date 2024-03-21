namespace UmbrellaLegacyBrain.Generic.Interfaces
{
    public interface ISerializable
    {
        public void Serialize(Span<byte> data);
    }
}