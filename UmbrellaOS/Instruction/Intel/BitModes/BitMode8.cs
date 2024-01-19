﻿namespace UmbrellaOS.Instruction.Intel.BitModes
{
    public sealed class BitMode8 : InstructionIntelBitMode
    {
        public static readonly BitMode8 Default = new();

        public BitMode8() : base(8) { }
    }
}