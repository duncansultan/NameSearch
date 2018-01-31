using System.Collections.Generic;
using AutoMapper;
using NameSearch.Models.Entities;
using NameSearch.Repository;
using Serilog;

namespace NameSearch.App.Services
{
    /// <summary>
    /// Search Name Helper
    /// </summary>
    public class NameHelper
    {
        #region Dependencies

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger = Log.Logger.ForContext<NameHelper>();
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEntityFrameworkRepository Repository;
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper Mapper;
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NameHelper"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        public NameHelper(IEntityFrameworkRepository repository,
            IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        /// <summary>
        /// Imports the specified names.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public long Import(IEnumerable<string> names, string group)
        {
            var nameImport = new NameImport
            {
                FileName = group
            };
            Repository.Create(nameImport);
            Repository.Save();
            
            foreach (var name in names)
            {
                var exists = Repository.GetExists<Name>(x => x.Value == name);

                var nameEntity = new Name
                {
                    NameImportId = nameImport.Id,
                    Value = name
                };

                Repository.Create(nameEntity);
                Repository.Save();
            }

            return nameImport.Id;
        }
    }
}
