﻿namespace Octgn.Plugin
{
    using System;

    /// <summary>
    /// Base interface for creating plugins.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Version of the plugin.
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// Required minimum version of OCTGN for this plugin.
        /// </summary>
        Version RequiredByOctgnVersion { get; }
    }
}