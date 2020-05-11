using System;
using System.Collections.Generic;
using System.Text;

namespace AuroraServer.Unreal.Serialization
{
    abstract class FArchive
    {
        #region Field Region

        /// <summary>
        /// Whether this archive is for loading data.
        /// </summary>
        public bool IsLoading;

        /// <summary>
        /// Whether this archive saves to persistent storage.
        /// </summary>
        public bool IsPersistent;

        #endregion
    }
}
