﻿using System;

namespace NGitHubdater
{
    /// <summary>
    /// Represents the result of the process of installing <i>something</i> (by manipulating files).
    /// </summary>
    public class InstallResult : AbstractFileProcessingResult
    {
        public InstallResult(long bytesInstalled) : base(bytesInstalled) { }
        public InstallResult(long bytesInstalled, Exception error) : base(bytesInstalled, error) { }
    }
}
