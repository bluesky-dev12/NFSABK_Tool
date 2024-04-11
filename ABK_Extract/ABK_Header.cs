using ABK_Extract.Binary;
using ABK_Extract.Core;
using System.Reflection;

namespace ABK_Extract
{

    public class ABK_Header
    {
        public static void ReadHeader(string header)
        {
            Reader reader = new Reader(header);
            //ABK Header
            ABKHeader AEMSHeader = new ABKHeader();

            // Read Module Bank Header
            AEMSHeader.MagicID = reader.ReadStringByLenght(4);
            AEMSHeader.Version = reader.ReadByte();
            AEMSHeader.veraimexmajor = reader.ReadByte();
            AEMSHeader.veraimexminor = reader.ReadByte();
            AEMSHeader.veraimexpatch = reader.ReadByte();
            AEMSHeader.Platform = reader.ReadEnum(reader.ReadByte()); //Made it string to automatically know which platform we build.
            AEMSHeader.TargetType = reader.ReadByte();
            AEMSHeader.NumModules = reader.ReadUShort();
            AEMSHeader.DebugCRC = reader.ReadInt32();
            AEMSHeader.UniqueID = reader.ReadInt32();
            AEMSHeader.ABKSize = reader.ReadInt32();
            AEMSHeader.HeaderSize = reader.ReadInt32();
            AEMSHeader.moduleoffset = reader.IntToHex();
            AEMSHeader.sfxbankoffset = reader.IntToHex();
            AEMSHeader.sfxbanksizepadded = reader.ReadInt32();
            AEMSHeader.midibankoffset = reader.IntToHex();
            AEMSHeader.midibanksizepadded = reader.ReadInt32();
            AEMSHeader.funcfixupoffset = reader.IntToHex();
            AEMSHeader.staticdatafixupoffset = reader.IntToHex();
            AEMSHeader.interfaceOffset = reader.IntToHex();
            AEMSHeader.modulebankhandle = reader.ReadInt32();
            AEMSHeader.midibhandle = reader.ReadInt32();
            AEMSHeader.streamfilepath = reader.ReadStringByLenght(4);
            AEMSHeader.streamfileoffset = reader.IntToHex();

            // Print Module Bank Header fields
            Log.WriteData($"MagicID: {AEMSHeader.MagicID}");
            Log.WriteData($"ABK Version: {AEMSHeader.Version}");
            Log.WriteData($"Major Version: {AEMSHeader.veraimexmajor}");
            Log.WriteData($"Minor Version: {AEMSHeader.veraimexminor}");
            Log.WriteData($"Patch Version: {AEMSHeader.veraimexpatch}");
            Log.WriteData($"Platform: {AEMSHeader.Platform}");
            Log.WriteData($"TargetType: {AEMSHeader.TargetType}");
            Log.WriteData($"Number Of Modules: {AEMSHeader.NumModules}");
            Log.WriteData($"DebugCRC: {AEMSHeader.DebugCRC}");
            Log.WriteData($"UniqueID: {AEMSHeader.UniqueID}");
            Log.WriteData($"ABK Full Size: {AEMSHeader.ABKSize} Bytes.");
            Log.WriteData($"Header Size: {AEMSHeader.HeaderSize} Bytes.");
            Log.WriteData($"Module Offset: {AEMSHeader.moduleoffset}");
            Log.WriteData($"BNK Offset: {AEMSHeader.sfxbankoffset}");
            Log.WriteData($"BNK Size Padded: {AEMSHeader.sfxbanksizepadded} Bytes.");
            Log.WriteData($"Midi Bank Offset: {AEMSHeader.midibankoffset}");
            Log.WriteData($"Midi Bank Size Padded: {AEMSHeader.midibanksizepadded} Bytes.");
            Log.WriteData($"Func Fix Up Offset: {AEMSHeader.funcfixupoffset}");
            Log.WriteData($"Func Static Data Offset: {AEMSHeader.staticdatafixupoffset}");
            Log.WriteData($"Func Interface Offset: {AEMSHeader.staticdatafixupoffset}");
            Log.WriteData($"Module Bank Handle: {AEMSHeader.modulebankhandle}");
            Log.WriteData($"MIDI Bank Handle: {AEMSHeader.midibhandle}");
            Log.WriteData($"Stream File Path: {AEMSHeader.streamfilepath}");
            Log.WriteData($"Stream File Offset: {AEMSHeader.streamfileoffset}");
            //ABK_Header


            //AemsDef_Snd10SampleBankHeader
            AemsDef_Snd10SampleBankHeader Snd10SampleBankHeader = new AemsDef_Snd10SampleBankHeader();
            Snd10SampleBankHeader.id = reader.IntToHex();
            Snd10SampleBankHeader.version = reader.ReadByte();
            Snd10SampleBankHeader.pad = reader.ReadByte();
            Snd10SampleBankHeader.serialNumber = reader.ReadUShort();
            Snd10SampleBankHeader.numSamples = reader.ReadInt32();

            Log.WriteData($"MagicID: {Snd10SampleBankHeader.id}");
            Log.WriteData($"Version: {Snd10SampleBankHeader.version}");
            Log.WriteData($"Pad: {Snd10SampleBankHeader.pad}");
            Log.WriteData($"Serial Number: {Snd10SampleBankHeader.serialNumber}");
            Log.WriteData($"Number Of Samples: {Snd10SampleBankHeader.numSamples}");
            //AemsDef_Snd10SampleBankHeader

            //TWEAKHEADER
            AemsDef_TWEAKHEADER TWEAKHEADER = new AemsDef_TWEAKHEADER();
            TWEAKHEADER.id = reader.IntToHex();
            TWEAKHEADER.ver = reader.ReadByte();
            TWEAKHEADER.platform = reader.ReadByte();
            TWEAKHEADER.pad = reader.ReadShort();
            TWEAKHEADER.crc = reader.ReadInt32();
            TWEAKHEADER.numcomponents = reader.ReadInt32();

            Log.WriteData($"MagicID: {TWEAKHEADER.id}");
            Log.WriteData($"Version: {TWEAKHEADER.ver}");
            Log.WriteData($"Platform: {TWEAKHEADER.platform}");
            Log.WriteData($"Pad: {TWEAKHEADER.pad}");
            Log.WriteData($"CRC: {TWEAKHEADER.crc}");
            Log.WriteData($"Number Of Components: {TWEAKHEADER.numcomponents}");
            //TWEAKHEADER
            reader.Position();

        }
    }
}
