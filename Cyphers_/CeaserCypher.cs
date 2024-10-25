using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphers
{
    public class CeaserCypher : ICypher
    {
        public readonly ushort Shift;

        public CeaserCypher(ushort shift)
        {
            this.Shift = shift;
        }
        public string Decrypt(string message)
        {
            return EncryptAndDecrypt(message,isEncryptMode:false);
        }

        private string EncryptAndDecrypt(string message, bool isEncryptMode=true)
        {
            ushort limit=char.MaxValue;
            StringBuilder result = new StringBuilder();
            

            for (int i = 0; i < message.Count(); ++i)
            {
                var character = message[i];
                if (character == '\n')
                {
                    result.Append(character);
                    continue;
                }

                int newCharacterCode;
                if (isEncryptMode){
                    newCharacterCode = (character + Shift + limit) % limit;
                }else{
                    newCharacterCode = (character - Shift + limit) % limit;
                }
                
                result.Append((char)newCharacterCode);
            }
            return result.ToString();

        }

        public string Encrypt(string message)
        {
           return EncryptAndDecrypt(message,isEncryptMode:true);
        }
    }
}
