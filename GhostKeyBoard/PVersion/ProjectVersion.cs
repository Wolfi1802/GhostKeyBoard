using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostKeyBoard.PVersion
{
    public class ProjectVersion
    {
        private readonly Version _version;
        public ProjectVersion() 
        {
            _version = new Version(1,1,2,1);        
        }

        public string GetVersion()
        {
            return _version.ToString();
        }
    }
}
