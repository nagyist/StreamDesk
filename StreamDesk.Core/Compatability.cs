using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace StreamDesk.Core
{
    public class Compatability
    {
        public enum DatabaseVersion
        {
            /// <summary>
            /// StreamDesk Database Format 1.0
            /// </summary>
            STREAMDESK_DB_1_0,
            /// <summary>
            /// StreamDesk Database Format 1.0m
            /// </summary>
            STREAMDESK_DB_1_0m,
            /// <summary>
            /// StreamDesk Database Format 1.1
            /// </summary>
            STREAMDESK_DB_1_1,
            /// <summary>
            /// StreamDesk Database Format 1.2
            /// </summary>
            STREAMDESK_DB_1_2,
            /// <summary>
            /// StreamDesk Database Format 1.3
            /// </summary>
            STREAMDESK_DB_1_3,
        }

        /// <summary>
        /// This function generates a <see cref="StreamDeskDatabase"/> object that has unused varables removed, sub-providers flattened
        /// or other functions to simplify the <see cref="IDatabaseFormatter.Write"/> function by removing compatability layer generations.
        /// </summary>
        /// <param name="dbVersion"></param>
        /// <param name="databaseToMakeCompatable"></param>
        /// <returns></returns>
        public static StreamDeskDatabase MakeStreamDeskDatabaseCompatable(DatabaseVersion dbVersion, StreamDeskDatabase databaseToMakeCompatable)
        {
            var newDb = new StreamDeskDatabase();

            switch (dbVersion)
            {
                case DatabaseVersion.STREAMDESK_DB_1_0m:
                    {
                        Flatten(newDb, databaseToMakeCompatable.Root, "");
                    }
                    break;
            }

            return newDb;
        }

        private static void Flatten(StreamDeskDatabase newDb, Provider currentProvider, string flattenedParam)
        {
            foreach (var subProvider in currentProvider.SubProviders)
            {
                var newProvider = new Provider
                                      {
                                          Name = flattenedParam + subProvider.Name,
                                          Web = subProvider.Web,
                                          Description = subProvider.Description,
                                          Pinned = subProvider.Pinned
                                      };
                newProvider.Streams.AddRange(subProvider.Streams);
                newDb.Root.SubProviders.Add(newProvider);

                if(subProvider.SubProviders.Count > 0)
                    Flatten(newDb, subProvider, flattenedParam + subProvider.Name + "\\");
            }
        }
    }
}
