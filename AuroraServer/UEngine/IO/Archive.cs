using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraServer.UEngine.IO
{
    abstract class FArchive
    {
        public bool IsError { get; set; }

        public abstract bool AtEnd();

        public virtual bool Close() => !IsError;

        public virtual void SetError() => IsError = true;
    }
}
