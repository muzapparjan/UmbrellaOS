using System.Buffers.Binary;
using UmbrellaLegacyBrain.Data.Interfaces;
using UmbrellaLegacyBrain.Generic;

namespace UmbrellaLegacyBrain.Data
{
    public sealed class Image(uint width, uint height) : IImage
    {
        public readonly ulong SerializedDataSize = ColorHSV01.SerializedSize * width * height;
        public readonly ulong SerializedSize = ColorHSV01.SerializedSize * width * height + sizeof(uint) * 2;

        public uint Width { get; private set; } = width;
        public uint Height { get; private set; } = height;
        public ColorHSV01[] Data { get; private set; } = new ColorHSV01[width * height];

        public ref ColorHSV01 this[int index] => ref Data[index];
        public ref ColorHSV01 this[int x, int y] => ref Data[x + y * Height];

        public void SetData(Span<byte> data)
        {
            for (var i = 0; i < Data.Length; i++)
                Data[i] = ColorHSV01.Deserialize(data.Slice((int)ColorHSV01.SerializedSize * i, (int)ColorHSV01.SerializedSize));
        }

        public void Serialize(Span<byte> data)
        {
            BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(sizeof(uint) * 0, sizeof(uint)), Width);
            BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(sizeof(uint) * 1, sizeof(uint)), Height);
            var offset = sizeof(uint) * 2;
            for (var i = 0; i < Data.Length; i++)
                Data[i].Serialize(data.Slice(i * (int)ColorHSV01.SerializedSize + offset, (int)ColorHSV01.SerializedSize));
        }
        public static Image Deserialize(Span<byte> data)
        {
            var width = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(sizeof(uint) * 0, sizeof(uint)));
            var height = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(sizeof(uint) * 1, sizeof(uint)));
            var image = new Image(width, height);
            image.SetData(data.Slice(sizeof(uint) * 2, (int)image.SerializedDataSize));
            return image;
        }
    }
}