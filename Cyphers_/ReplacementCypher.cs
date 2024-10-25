using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyphers
{
    public class ReplacementCypher : ICypher
    {
        public readonly IList<uint> Order;

        private ReplacementCypher(IList<uint> order)
        {
            this.Order = order;
        }

        public static ReplacementCypher Instantiate(params uint[] order) {
           return Instantiate(order.ToList());
        }
        public static ReplacementCypher Instantiate(IList<uint> order) {
           return ValidateArgs(order);
        }

        private static ReplacementCypher ValidateArgs(IList<uint> order)
        {
            bool isUnique = order.Distinct().Count() == order.Count();
            if (!isUnique)
                throw new ArgumentException("The argument <order> must contain unique values");
            try
            {
                for (int i = 0; i < order.Count(); ++i) {
                    order.First(item=>item==(i+1));
                }
            }
            catch (Exception ex) { 
                throw new ArgumentException("The argument <order> must contain all values from the [1, ..., order.Count()");
            }
            return new ReplacementCypher(order);
        }

       string EncodeAndDecode(string message,bool isDecrypt) {
		    int step = Order.Count();

            StringBuilder result = new StringBuilder(string.Empty);

		    for (int i = 0; i<(message.Count() / step); ++i) {
		    	string subString = message.Substring(i * step, step);
                StringBuilder temp = new StringBuilder(subString);
		    	for (int j = 0; j<step; ++j) {
                    //!!!
                    int position = (int)Order[j] - 1;
                    if (isDecrypt) 
                        temp[position] = subString[j];
                    else
                        temp[j] = subString[position];
                }
		    	result.Append(temp);
		    }
            string unnafectedPartOfString = message.Substring((message.Count() / step) * step);
		    return result.Append(unnafectedPartOfString).ToString();
	}

        public string Decrypt(string message)
        {
            return EncodeAndDecode(message,isDecrypt:true);
        }

        public string Encrypt(string message)
        {
            return EncodeAndDecode(message,isDecrypt:false);
        }
    }
}
