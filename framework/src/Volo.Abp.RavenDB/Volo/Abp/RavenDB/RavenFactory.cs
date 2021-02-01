using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using System;
using System.Reflection;

namespace Volo.Abp.RavenDB.Volo.Abp.RavenDB
{
    public static class RavenFactory
    {
        public static IDocumentStore Store => LazyStore.Value;

        private static readonly Lazy<IDocumentStore> LazyStore =
            new Lazy<IDocumentStore>(() =>
            {
                var store = CreateStore();
                IndexCreation.CreateIndexes(typeof(RavenFactory).GetTypeInfo().Assembly, store);
                return store;
            });

        private static IDocumentStore CreateStore()
        {
            var store = new DocumentStore
            {
                Urls = new string[] { "http://localhost:8080" },                
                Database = "Abp"
            };

            store.Initialize();

            return store;
        }
    }
}
