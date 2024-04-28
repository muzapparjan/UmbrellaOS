namespace UmbrellaOS.Generic
{
    /**
     * <summary>
     * Represents a bit value which is an element of {0, 1}.
     * </summary>
     */
    public struct Bit : IEquatable<Bit>
    {
        /**
         * <summary>
         * The bit value.
         * </summary>
         * <exception cref="ArgumentException">An ArgumentException may be thrown if the value is not 0 nor 1.</exception>
         */
        public byte Value
        {
            readonly get => value;
            set
            {
                if (value > 1)
                    throw new ArgumentException($"bit value should only be 0 or 1", nameof(value));
                this.value = value;
            }
        }

        private byte value;

        /**
         * <summary>
         * Create a new bit.
         * </summary>
         * <param name="value">The bit value.</param>
         * <exception cref="ArgumentException">An ArgumentException may be thrown if the value is not 0 nor 1.</exception>
         */
        public Bit(byte value)
        {
            Value = value;
        }

        public static implicit operator Bit(byte value) => new(value);
        public static implicit operator Bit(short value) => new((byte)value);
        public static implicit operator Bit(ushort value) => new((byte)value);
        public static implicit operator Bit(int value) => new((byte)value);
        public static implicit operator Bit(uint value) => new((byte)value);
        public static implicit operator Bit(long value) => new((byte)value);
        public static implicit operator Bit(ulong value) => new((byte)value);
        public static implicit operator Bit(bool value) => new(value ? (byte)1 : (byte)0);
        public static implicit operator byte(Bit value) => value.Value;

        public readonly bool Equals(Bit other) => Value.Equals(other.Value);
        public override readonly bool Equals(object? obj) => obj is Bit bit && Equals(bit);
        public static bool operator ==(Bit left, Bit right) => left.Equals(right);
        public static bool operator !=(Bit left, Bit right) => !(left == right);
        public override readonly int GetHashCode() => Value.GetHashCode();
    }
}