using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Типове документ
    /// </summary>
    public enum DocumentTypes
    {
        /// <summary>
        /// Електронен
        /// </summary>
        EDocument = 1,
        /// <summary>
        /// Прикачен документ
        /// </summary>
        AttachableDocument = 2
    }

    public enum EkatteTypes
    {
        /// <summary>
        /// Неопеделено.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Област.
        /// </summary>
        District = 1,
        /// <summary>
        /// Община.
        /// </summary>
        Municipality = 2,
        /// <summary>
        /// Град/Село.
        /// </summary>
        Settlement = 3,
        /// <summary>
        /// Кметство
        /// </summary>
        Mayoralty = 4,
        /// <summary>
        /// Район.
        /// </summary>
        Area = 5
    }

    public enum GraoTypes
    {
        /// <summary>
        /// Неопеделено.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Област.
        /// </summary>
        District = 1,
        /// <summary>
        /// Община.
        /// </summary>
        Municipality = 2,
        /// <summary>
        /// Град/Село.
        /// </summary>
        Settlement = 3
    }

    public enum WaysToStartService
    {
        ByAplication = 1,
        ByRedirectToWebPage = 2
    }

    /// <summary>
    /// Типове на предаване на административните услуги
    /// </summary>
    public enum AdmServiceTermType
    {
        /// <summary>
        /// Обикновена
        /// </summary>
        Regular = 1,
        /// <summary>
        /// Бърза
        /// </summary>
        Fast = 2,
        /// <summary>
        /// Експресна
        /// </summary>
        Express = 3,
    }

    /// <summary>
    /// Типове на период на изпълнение
    /// </summary>
    public enum ExecutionPeriodType
    {
        /// <summary>
        /// Часове
        /// </summary>
        Hours = 1,
        /// <summary>
        /// Дни
        /// </summary>
        Days = 2
    }

    public enum Status
    {
        Deactivate = 0,
        Activate = 1            
    }
}
