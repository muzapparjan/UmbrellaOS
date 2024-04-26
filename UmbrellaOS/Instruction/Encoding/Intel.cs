namespace UmbrellaOS.Instruction.Encoding;

public static class Intel
{
    public enum B
    {
        _16,
        _32,
        _64
    }
    public enum R
    {
        AH, AL, AX, EAX, RAX,
        CH, CL, CX, ECX, RCX,
        DH, DL, DX, EDX, RDX,
        BH, BL, BX, EBX, RBX,
        SP, ESP, RSP,
        BP, EBP, RBP,
        SI, ESI, RSI,
        DI, EDI, RDI,
    }

    public static byte[] EncodeRegister(R register) => register switch
    {
        R.AH => [1, 0, 0],
        R.AL => [0, 0, 0],
        R.AX => [0, 0, 0],
        R.EAX => [0, 0, 0],
        R.RAX => [0, 0, 0],
        R.CH => [1, 0, 1],
        R.CL => [0, 0, 1],
        R.CX => [0, 0, 1],
        R.ECX => [0, 0, 1],
        R.RCX => [0, 0, 1],
        R.DH => [1, 1, 0],
        R.DL => [0, 1, 0],
        R.DX => [0, 1, 0],
        R.EDX => [0, 1, 0],
        R.RDX => [0, 1, 0],
        R.BH => [1, 1, 1],
        R.BL => [0, 1, 1],
        R.BX => [0, 1, 1],
        R.EBX => [0, 1, 1],
        R.RBX => [0, 1, 1],
        R.SP => [1, 0, 0],
        R.ESP => [1, 0, 0],
        R.RSP => [1, 0, 0],
        R.BP => [1, 0, 1],
        R.EBP => [1, 0, 1],
        R.RBP => [1, 0, 1],
        R.SI => [1, 1, 0],
        R.ESI => [1, 1, 0],
        R.RSI => [1, 1, 0],
        R.DI => [1, 1, 1],
        R.EDI => [1, 1, 1],
        R.RDI => [1, 1, 1],
        _ => throw new ArgumentException($"failed to encode register {register}", nameof(register))
    };
}