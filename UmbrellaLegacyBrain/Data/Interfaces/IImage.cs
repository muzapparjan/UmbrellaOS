using UmbrellaLegacyBrain.Generic;
using UmbrellaLegacyBrain.Interfaces;

namespace UmbrellaLegacyBrain.Data.Interfaces
{
    public interface IImage : IData
    {
        public int Width { get; }
        public int Height { get; }
        public ColorHSV01[,] Data { get; }
    }
}