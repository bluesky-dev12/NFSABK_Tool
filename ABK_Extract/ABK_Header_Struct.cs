using System;
using System.Runtime.InteropServices;

public class AemsDef_Snd10SampleBankHeader
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public string id;
    public byte version;
    public byte pad;
    public ushort serialNumber;
    public int numSamples;
}

public class AemsDef_TWEAKHEADER
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public string id;
    public byte ver;
    public byte platform;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public short pad;
    public int crc;
    public int numcomponents;
}

public class CListDNode
{
    public CListDNode pnext;
    public CListDNode pprev;
}

public class ABKHeader
{
    public enum PlatformEnum
    {
        PC = 0,
        Unk1,
        PS2,
        Xbox,
        GC,
        X360,
        CarbonPC_FE,
        Unk7,
        Unk8,
        Unk9,
        PS3 = 10
    }

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public string? MagicID;
    public byte Version;
    public byte veraimexmajor;
    public byte veraimexminor;
    public byte veraimexpatch;
    public string Platform;
    public byte TargetType;
    public ushort NumModules;
    public int DebugCRC;
    public int UniqueID;
    public int ABKSize;
    public int HeaderSize;
    public string moduleoffset;
    public string sfxbankoffset;
    public int sfxbanksizepadded;
    public string midibankoffset;
    public int midibanksizepadded;
    public string funcfixupoffset;
    public string staticdatafixupoffset;
    public string interfaceOffset;
    public int modulebankhandle;
    //public IntPtr pSnd10SampleBankHeader; // Pointer to AemsDef_Snd10SampleBankHeader
    public int midibhandle;
    public string streamfilepath; // Pointer to char
    public string streamfileoffset;
    public CListDNode ln;
    public IntPtr ptweakheader; // Pointer to AemsDef_TWEAKHEADER
}