using Cyphers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRoutingProject.Models
{
    public class CyphersManager
    {
        private Dictionary<string, ICypher> _cyphers;

        public CyphersManager() {

            _cyphers = new Dictionary<string, ICypher>();

            _cyphers["Шифр Цезаря"] = new CeaserCypher(1);
            _cyphers["Инверсивный шифр"] = new InverseCypher();
            _cyphers["Шифр перестановкой"] = ReplacementCypher.Instantiate(1, 3, 2);
        }
        public ICypher GetCypher(string name) { 
            return _cyphers[name];
        }
        public void SetCypher<T>(string name, T cypher) where T:ICypher{
            if (!_cyphers.ContainsKey(name) || !(_cyphers[name] is  T))
            {
                throw new ArgumentException("no such key");
            }
            _cyphers[name] = cypher;
        }
        public IEnumerable<string> GetKeys()=>_cyphers.Keys;
    }
}
