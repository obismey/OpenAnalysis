using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vivei.Tools.Core;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// Service  permettant de personnaliser et d'etendre  l'outil de provisionnment
    /// </summary>
    public interface IProvisionnementService : IService
    {
        /// <summary>
        /// Enregistrer une  classe  implemantant l'interface  <see cref="ITriangleDataProcessor"/>
        /// </summary>
        /// <param name="processortype">Type du processeur </param>
        void RegisterTriangleDataProcessor(Type processortype);

        /// <summary>
        /// Enregistrer une  classe  implemantant l'interface  <see cref="ITriangleDataProvider"/> 
        /// </summary>
        /// <param name="providertype">Type du Provider</param>
        void RegistortriangleDataProvider(Type providertype);
    }
}
