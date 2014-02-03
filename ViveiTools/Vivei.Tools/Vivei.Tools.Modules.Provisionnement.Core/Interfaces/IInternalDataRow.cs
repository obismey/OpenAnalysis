using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vivei.Tools.Modules.Provisionnement.Model.Interfaces
{
    /// <summary>
    /// Represente une ligne de la table des donnees
    /// </summary>
    public interface IInternalDataRow
    {
        /// <summary>
        /// Table des donnees
        /// </summary>
        IInternalData InternalData { get; }

        /// <summary>
        /// Recupere la valeur d'une colonne
        /// </summary>
        /// <param name="columnindex">Index de la colonne</param>
        /// <returns></returns>
        object GetValue(int columnindex);

        /// <summary>
        /// Recupere la valeur fortement typee d'une colonne 
        /// </summary>
        /// <param name="columnindex">Index de la colonne</param>
        /// <returns></returns>
        string GetValueAsString(int columnindex);

        /// <summary>
        /// Recupere la valeur  fortement typee  d'une colonne
        /// </summary>
        /// <param name="columnindex">Index de la colonne</param>
        /// <returns></returns>
        DateTime? GetValueAsDate(int columnindex);

        /// <summary>
        /// Recupere la valeur  fortement typee  d'une colonne
        /// </summary>
        /// <param name="columnindex">Index de la colonne</param>
        /// <returns></returns>
        double? GetValueAsNumber(int columnindex);      
    }
}
