namespace UmbrellaOS.Instruction.Encoding.INTEL;

public enum Prefix
{
    LOCK = 0xF0,
    REPNE = 0xF2, REPNZ = 0xF2,
    REP = 0xF3, REPE = 0xF3, REPZ = 0xF3,
    BND = 0xF2,
    CSOverride = 0x2E,
    SSOverride = 0x36,
    DSOverride = 0x3E,
    ESOverride = 0x26,
    FSOverride = 0x64,
    GSOverride = 0x65,
    BranchNotTaken = 0x2E,
    BranchTaken = 0x3E,
    OperandSizeOverride = 0x66,
    AddressSizeOverride = 0x67,
}