using UmbrellaLegacyBrain.Generic;
using UmbrellaLegacyBrain.Interfaces;

namespace UmbrellaLegacyBrain.Data.Interfaces
{
    public interface IImage : IData
    {
        public uint Width { get; }
        public uint Height { get; }
        public ColorHSV01[] Data { get; }
    }
}