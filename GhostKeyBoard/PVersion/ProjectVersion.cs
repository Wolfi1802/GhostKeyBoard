using System;

namespace GhostKeyBoard.PVersion
{
    public class ProjectVersion
    {
        private readonly Version _version;
        public ProjectVersion()
        {
            _version = new Version(2, 0, 0, 0);
        }

        public string GetVersion()
        {
#if DEBUG
            return $"DEV {_version.ToString()}";
#else
            return $"Release {_version.ToString()}";
#endif
        }
    }
}
