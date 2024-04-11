using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABK_Extract
{
    public class Extract
    {
        private const string BNK_FILE_EXTENSION = ".bnk";
        private const string ABK_FILE_EXTENSION = ".abk";
        private const string BNK_MAGIC_HEADER = "BNK";

        private static short GetBNKNumElements(string inFilename)
        {
            short returnValue = 0;

            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(inFilename, FileMode.Open)))
                {
                    // Seek to the position where the number of elements is located
                    reader.BaseStream.Seek(0x6, SeekOrigin.Begin);

                    // Read the number of elements
                    returnValue = reader.ReadInt16();


                    // Adjust the value (assuming it represents count, subtracting 1)
                    returnValue -= 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                returnValue = -1;
            }

            return returnValue;
        }

        public static void ExtractBNKFromABK(string abkFilePath)
        {
            // Check ABK file extension
            if (Path.GetExtension(abkFilePath).ToLower() != ABK_FILE_EXTENSION)
            {
                Console.WriteLine("ERROR: Input file is not an ABK file.");
                return;
            }

            // Read ABK file
            using (BinaryReader reader = new BinaryReader(File.Open(abkFilePath, FileMode.Open)))
            {
                // Search for BNK data
                byte[] headerBytes = Encoding.ASCII.GetBytes(BNK_MAGIC_HEADER);
                long headerPosition = FindPattern(reader, headerBytes);
                if (headerPosition != -1)
                {
                    // Save the data before the BNK header into a separate file
                    string beforeBNKFileName = Path.GetFileNameWithoutExtension(abkFilePath) + "_beforeBNK" + ".aems";
                    byte[] beforeBNKData = new byte[headerPosition];
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    reader.Read(beforeBNKData, 0, (int)headerPosition);
                    File.WriteAllBytes(beforeBNKFileName, beforeBNKData);
                    Console.WriteLine($"Data before BNK header saved to: {beforeBNKFileName}");

                    // Seek to BNK header position
                    reader.BaseStream.Seek(headerPosition, SeekOrigin.Begin);

                    // Read BNK data until the end of the file
                    long startPosition = reader.BaseStream.Position;
                    long endPosition = reader.BaseStream.Length;

                    // Read BNK data
                    byte[] bnkData = new byte[endPosition - startPosition];
                    reader.Read(bnkData, 0, (int)(endPosition - startPosition));

                    // Write BNK data to a file
                    string bnkFileName = Path.GetFileNameWithoutExtension(abkFilePath) + BNK_FILE_EXTENSION;
                    File.WriteAllBytes(bnkFileName, bnkData);
                    Console.WriteLine($"BNK file extracted: {bnkFileName}");

                    // Execute sx.exe command
                    string outPath = Path.GetDirectoryName(abkFilePath);
                    string filenameBase = Path.GetFileNameWithoutExtension(abkFilePath);
                    string outFilenameSX = Path.Combine(outPath, filenameBase);
                    string sxCommand = $"sx.exe -wave \"{Path.GetFileName(bnkFileName)}\" -=\"{Path.GetFileName(outFilenameSX)}\" -onetomany";
                    Console.WriteLine($"Executing command: {sxCommand}");
                    ExecuteCommand(sxCommand);

                    // Move and rename the generated wav files
                    short bnkNumElements = GetBNKNumElements(bnkFileName);
                    RenameAndMoveWavFiles(outPath, filenameBase, bnkNumElements);
                }
                else
                {
                    Console.WriteLine("BNK data not found in ABK file.");
                }
            }
        }

        private static void RenameAndMoveWavFiles(string outPath, string filenameBase, short bnkNumElements)
        {
            // Create directory with the same name as the ABK file (without extension)
            string directoryName = Path.Combine(outPath, Path.GetFileNameWithoutExtension(filenameBase));
            Directory.CreateDirectory(directoryName);

            // Move and rename the generated wav files
            for (short i = 0; i < bnkNumElements; i++)
            {
                string oldName = $"{filenameBase}.{i + 1}";
                string newName = Path.Combine(directoryName, $"{filenameBase}_{i + 1}.wav");

                // Move and rename the file
                File.Move(oldName, newName);
                Console.WriteLine($"Renaming {oldName} to {newName}");
            }
        }

        private static void ExecuteCommand(string command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while executing the command: " + ex.Message);
            }
        }

        // Function to find a pattern within the binary stream
        private static long FindPattern(BinaryReader reader, byte[] pattern)
        {
            long position = 0;
            long startPosition = reader.BaseStream.Position;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte currentByte = reader.ReadByte();

                if (currentByte == pattern[0])
                {
                    long savedPosition = reader.BaseStream.Position;
                    bool found = true;

                    for (int i = 1; i < pattern.Length; i++)
                    {
                        if (reader.ReadByte() != pattern[i])
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        position = savedPosition - 1; // Adjust for the starting byte of the pattern
                        break;
                    }
                    else
                    {
                        reader.BaseStream.Position = savedPosition; // Restore position
                    }
                }
            }

            reader.BaseStream.Position = startPosition; // Restore original position
            return position;
        }
    }
}
