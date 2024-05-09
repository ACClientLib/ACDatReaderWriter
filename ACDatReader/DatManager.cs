﻿
using System;
using System.IO;
using ACDatReader.IO;
using ACDatReader.Options;

namespace ACDatReader {

    /// <summary>
    /// A configurable dat manager implementation that loads up all of the dat files
    /// </summary>
    public class DatManager : IDisposable {
        /// <summary>
        /// Options
        /// </summary>
        public DatManagerOptions Options { get; } = new DatManagerOptions();

        /// <summary>
        /// Portal dat database
        /// </summary>
        public DatDatabaseReader Portal { get; }

        /// <summary>
        /// Cell dat database
        /// </summary>
        public DatDatabaseReader Cell { get; }

        /// <summary>
        /// Language dat database
        /// </summary>
        public DatDatabaseReader Language { get; }

        /// <summary>
        /// HighRes dat database
        /// </summary>
        public DatDatabaseReader HighRes { get; }

        /// <summary>
        /// Create a new datmanager
        /// </summary>
        /// <param name="options">Options configuration action</param>
        /// <param name="blockReader">An instance of a block reader to use</param>
        public DatManager(Action<DatManagerOptions>? options = null, IDatBlockReader? blockReader = null) {
            options?.Invoke(Options);

            Portal = new DatDatabaseReader(portalOptions => {
                portalOptions.FilePath = Options.PortalDatPath;
                portalOptions.PreloadFileEntries = Options.PreloadPortalFileEntries;
                portalOptions.CacheDirectories = Options.CachePortalDirectories;
            }, blockReader);

            Cell = new DatDatabaseReader(cellOptions => {
                cellOptions.FilePath = Options.PortalDatPath;
                cellOptions.PreloadFileEntries = Options.PreloadPortalFileEntries;
                cellOptions.CacheDirectories = Options.CacheCellDirectories;
            }, blockReader);

            Language = new DatDatabaseReader(languageOptions => {
                languageOptions.FilePath = Options.PortalDatPath;
                languageOptions.PreloadFileEntries = Options.PreloadPortalFileEntries;
                languageOptions.CacheDirectories = Options.CacheLanguageDirectories;
            }, blockReader);

            HighRes = new DatDatabaseReader(highResOptions => {
                highResOptions.FilePath = Options.HighResDatPath;
                highResOptions.PreloadFileEntries = Options.PreloadHighResFileEntries;
                highResOptions.CacheDirectories = Options.CacheHighResDirectories;
            }, blockReader);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Dispose
        /// </summary>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                Portal?.Dispose();
                Cell?.Dispose();
                Language?.Dispose();
            }
        }
    }
}
