namespace UmbrellaOS.Instruction.Encoding.INTEL;

public enum ConditionTest
{
    /** <summary>Overflow</summary> */
    O = 0,
    /** <summary>No overflow</summary> */
    NO = 1,
    /** <summary>Below</summary> */
    B = 2,
    /** <summary>Not above or equal</summary> */
    NAE = 2,
    /** <summary>Not below</summary> */
    NB = 3,
    /** <summary>Above or equal</summary> */
    AE = 3,
    /** <summary>Equal</summary> */
    E = 4,
    /** <summary>Zero</summary> */
    Z = 4,
    /** <summary>Not equal</summary> */
    NE = 5,
    /** <summary>Not zero</summary> */
    NZ = 5,
    /** <summary>Below or equal</summary> */
    BE = 6,
    /** <summary>Not above</summary> */
    NA = 6,
    /** <summary>Not below or equal</summary> */
    NBE = 7,
    /** <summary>Above</summary> */
    A = 7,
    /** <summary>Sign</summary> */
    S = 8,
    /** <summary>Not sign</summary> */
    NS = 9,
    /** <summary>Parity</summary> */
    P = 10,
    /** <summary>Parity even</summary> */
    PE = 10,
    /** <summary>Not parity</summary> */
    NP = 11,
    /** <summary>Parity odd</summary> */
    PO = 11,
    /** <summary>Less than</summary> */
    L = 12,
    /** <summary>Not greater than or equal to</summary> */
    NGE = 12,
    /** <summary>Not less than</summary> */
    NL = 13,
    /** <summary>Greater than or equal to</summary> */
    GE = 13,
    /** <summary>Less than or equal to</summary> */
    LE = 14,
    /** <summary>Not greater than</summary> */
    NG = 14,
    /** <summary>Not less than or equal to</summary> */
    NLE = 15,
    /** <summary>Greater than</summary> */
    G = 15,
}