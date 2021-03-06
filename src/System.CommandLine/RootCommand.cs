﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Linq;
using System.Reflection;

namespace System.CommandLine
{
    public class RootCommand : Command
    {
        public RootCommand(string description = "") : base(ExecutableName, description)
        {
        }

        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                AddAlias(Name);
            }
        }

        private static readonly Lazy<string> _executablePath = new Lazy<string>(() =>
        {
            return GetAssembly().Location;
        });

        private static readonly Lazy<string> _executableName = new Lazy<string>(() =>
        {
            var location = _executablePath.Value;
            if (string.IsNullOrEmpty(location))
            {
                location = Environment.GetCommandLineArgs().FirstOrDefault();
            }
            return Path.GetFileNameWithoutExtension(location).Replace(" ", "");
        });

        private static Assembly GetAssembly() =>
            Assembly.GetEntryAssembly() ??
            Assembly.GetExecutingAssembly();

        public static string ExecutableName => _executableName.Value;

        public static string ExecutablePath => _executablePath.Value;
    }
}
