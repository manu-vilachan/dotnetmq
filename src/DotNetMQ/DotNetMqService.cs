/*
DotNetMQ - A Complete Message Broker For .NET
Copyright (C) 2011 Halil ibrahim KALKAN

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System;
using System.Reflection;
using Common.Logging;
using MDS;
using Topshelf;

namespace DotNetMQ
{
    /// <summary>
    /// This service is used to run DotNetMQ as a Windows Service.
    /// </summary>
    public class DotNetMqService : ServiceControl
    {
        /// <summary>
        /// Reference to logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Reference to MDS server instance.
        /// </summary>
        private readonly MDSServer _mdsServer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DotNetMqService()
        {
            _mdsServer = new MDSServer();
        }

        public bool Start(HostControl hostControl)
        {
            try
            {
                _mdsServer.Start();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.Message, ex);
                Stop(hostControl);
            }
            return false;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                _mdsServer.Stop(true);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.Message, ex);
            }
            return false;
        }
    }
}
