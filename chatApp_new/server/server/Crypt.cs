using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class Crypt
    {
        public Crypt()
        {


        }


        public static string Encryption(string input)
        {
            string alphabet = "aăâbcdđeêghiklmnoôơpqrstuưvxyzAĂÂBCDĐEÊGHIKLMNOÔƠPQRSTUƯVXYZ0123456789!@#$%^&*";
            int shift = 3;
            StringBuilder encryptedText = new StringBuilder();
            int i = alphabet.Length;

            foreach (char c in input)
            {
                int k = -1;
                if (c == ' ')
                {
                    encryptedText.Append(' ');
                    continue;
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (c == alphabet[j])
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k < 0)
                    {
                        encryptedText.Append(c);
                        continue;
                    }
                    k = (k + shift) % i;
                    encryptedText.Append(alphabet[k]);
                }
            }
            return encryptedText.ToString();
        }
        public static string Decryption(string input)
        {
            string alphabet = "aăâbcdđeêghiklmnoôơpqrstuưvxyzAĂÂBCDĐEÊGHIKLMNOÔƠPQRSTUƯVXYZ0123456789!@#$%^&*";
            int shift = 3;
            StringBuilder decryptedText = new StringBuilder();
            int i = alphabet.Length;

            foreach (char c in input)
            {
                int k = -1;
                if (c == ' ')
                {
                    decryptedText.Append(' ');
                    continue;
                }
                else
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (c == alphabet[j])
                        {
                            k = j;
                            break;
                        }
                    }
                    if (k < 0)
                    {
                        decryptedText.Append(c);
                        continue;
                    }
                    k = (k - shift) % i;
                    decryptedText.Append(alphabet[k]);
                }
            }
            return decryptedText.ToString();
        }

    }
}
