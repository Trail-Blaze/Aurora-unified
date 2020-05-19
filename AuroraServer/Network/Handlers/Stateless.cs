using System;
using System.Collections.Generic;
using System.Text;

// todo: will probably need to be cleaned up before rls
namespace AuroraServer.Network.Handlers
{
    class Stateless
    {

        /// <summary>
        /// Server-side only secret value
        /// </summary>
        private byte[] _handshakeSecret;

        /// <summary>
        /// which secret is active?
        /// </summary>
        private byte _activeSecret;

        public Stateless()
        {
            _handshakeSecret = new byte[2];
            _activeSecret = 255;
        }
    }
}
