using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphers
{
    public class InverseCypher : ICypher
    {

        public InverseCypher() { }
        public string Decrypt(string message)
        {
            return EncryptAndDecrypt(message);
        }

        public string Encrypt(string message)
        {
            return EncryptAndDecrypt(message);
        }

        string EncryptAndDecrypt(string message)
        {
            ushort limit = char.MaxValue;
            StringBuilder result = new StringBuilder(message);


            for (int i = 0; i < message.Count(); ++i)
            {
                var character = message[i];
                int characterCode = limit - ((ushort)character);
                result[i]= (char)characterCode;
            }
            return result.ToString();
        }
    }
}
