using DubUrl;
using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Reading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Repositories
{
    internal class DatabaseIdentityRepository : IIdentityRepository
    {
        private string ConnectionUrl { get; }

        private DatabaseUrl? databaseUrl;
        protected DatabaseUrl DatabaseUrl { get => databaseUrl ??= InitDatabaseUrl(ConnectionUrl); }

        public DatabaseIdentityRepository(string connectionUrl)
            => ConnectionUrl = connectionUrl;

        private DatabaseUrl InitDatabaseUrl(string connectionUrl)
            => new DatabaseUrl(connectionUrl);

        public void Merge(RecordIdentity record)
        {
            var factory = new DubUrlParameterFactory();
            var parameters = record.Structure.FieldNames.Select(
                    fieldName => factory.Instantiate(fieldName, record.GetValue(fieldName))
                ) ?? throw new ArgumentNullException();

            DatabaseUrl.ReadScalar(new MergeEntityCommand(parameters.ToArray()));
        }

        private class MergeEntityCommand : ParametrizedEmbeddedSqlFileCommand
        {
            public MergeEntityCommand(DubUrlParameter[] parameters)
                : base($"{typeof(MergeEntityCommand).Assembly.GetName().Name}.Query.{nameof(Merge)}", parameters)
            { }
        }
    }
}
