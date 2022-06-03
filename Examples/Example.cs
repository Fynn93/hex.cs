using Hex.cs;

Console.WriteLine("As Hex:");
Console.WriteLine(Hexadecimal.GetHex("File1.bin", 0x00, 0x0C));
Console.WriteLine();

Console.WriteLine("As String:");
Console.WriteLine(Hexadecimal.GetString("File1.bin", 0x00, 0x0C));
Console.WriteLine();

Console.WriteLine("As Bits:");
Console.WriteLine(Hexadecimal.GetBits(Hexadecimal.GetHex("File1.bin", 0x00, 0x0C)));