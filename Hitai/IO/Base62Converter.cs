using Base62;

namespace Hitai.IO
{
    public class Base62Converter
    {
        public byte[] FromBase62String(string base62) {
            return base62.FromBase62();
        }

        public string ToBase62String(byte[] original) {
            return original.ToBase62();
        }

        /*
            public const int Radix = 62;

            public char[] CharacterRange = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

            public byte[] FromBase62String(string base62) {
                var count = 0;
                var stream = new BitStream(base62.Length * 6 / 8);

                foreach (char c in base62) {
                    var index = Array.IndexOf(CharacterRange, c);

                    // došli jsme na konec
                    if (count >= base62.Length - 1) {
                        var mod = (int)(stream.Position % 8);
                        if (mod == 0)
                            throw new InvalidDataException("Byl nalezen znak navic");
                        if ((index >> (8 - mod)) > 0)
                            throw new InvalidDataException("Spatny znak na konci retezce");
                        stream.Write(new byte[] { (byte)(index << mod) }, 0, 8 - mod);
                    }
                    else {
                        // pokud je znak indexu 60 nebo 61, zapíšeme jen 5 bitů
                        if (index == 60)
                            stream.Write(new byte[] { 0xf0 }, 0, 5);
                        else if (index == 61)
                            stream.Write(new byte[] { 0xf8 }, 0, 5);
                        else
                            // zapíšeme sextet s offsetem 2
                            stream.Write(new byte[] { (byte)index }, 2, 6);
                    }
                    count++;
                }

                var result = new byte[stream.Position / 8];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(result, 0, result.Length * 8);
                return result;
            }

            public string ToBase62String(byte[] original) {
                var sb = new StringBuilder();
                var stream = new BitStream(original);

                var read = new byte[1];
                while (true) {
                    read[0] = 0;
                    // přečteme sextet (6 bitů) z pole bajtů
                    int length = stream.Read(read, 0, 6);
                    // pokud je délka 6, znamená to, že jsme přečetli celý jeden sextet
                    if (length == 6) {
                        // prvních 5 bitů je 11111
                        if (read[0] >> 3 == 0x1f) {
                            sb.Append(CharacterRange[61]);
                            // šestý bit necháme další iteraci
                            stream.Seek(-1, SeekOrigin.Current);
                        }
                        // prvních 5 bitů je 11110
                        else if (read[0] >> 3 == 0x1e) {
                            sb.Append(CharacterRange[60]);
                            stream.Seek(-1, SeekOrigin.Current);
                        }
                        else {
                            // dostali jsme base62 reprezentaci sextetu
                            sb.Append(CharacterRange[read[0] >> 2]);
                        }
                    }
                    else if (length == 0) break;
                    else {
                        // posuneme, abychom na konci dostali sextet
                        sb.Append(CharacterRange[(read[0] >> (8 - length))]);
                        break;
                    }
                }
                return sb.ToString();
            }
            */
    }
}
