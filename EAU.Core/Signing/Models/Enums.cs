﻿namespace EAU.Signing.Models
{
    /// <summary>
    /// Формати за е-подписа според файла за подписване.
    /// 0 = CAdES; 1 = PAdES; 2 = XAdES; 3 = ASICS; 4 = ASICE;
    /// </summary>
    public enum SigningFormats
    {
        /// <summary>
        /// CAdES.
        /// </summary>
        CAdES = 0,

        /// <summary>
        /// PAdES.
        /// </summary>
        PAdES = 1,

        /// <summary>
        /// XAdES.
        /// </summary>
        XAdES = 2,

        /// <summary>
        /// ASICS.
        /// </summary>
        ASICS = 3,

        /// <summary>
        /// ASICE.
        /// </summary>
        ASICE = 4
    }

    /// <summary>
    /// Статуси за процеса по подписване.
    /// 1 = В процес/InProcess; 2 = Отхвърляне/Rejecting; 3 = Завършване/Completing;
    /// </summary>
    public enum SigningRequestStatuses
    {
        /// <summary>
        /// В процес.
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// Отхвърляне.
        /// </summary>
        Rejecting = 2,

        /// <summary>
        /// Завършване.
        /// </summary>
        Completing = 3,
    }

    /// <summary>
    /// Ниво на полагане на е-подпис.
    /// 0 = Базово ниво на електронния подпис. Осигурява цялост на подписания документ и неотменимост на положения електронен подпис.;
    /// 1 = Към базовото ниво на подпис е добавено удостоверено време на подписване като доказателство, че подписа е съществувал към определен момент.;
    /// 2 = Към базовото ниво на подпис са добавени атрибути, осигуряващи валидността на подписа, чрез проверка единствено на подписания файл. 
    /// Целта на това ниво е да осигури информация за валидността на подписа при дългосрочно съхранение на подписания файл.;
    /// 3 = Чрез добавяне на удостоверени времена позволява валидацията на подписа дълго време след създаването му. 
    /// Целта на това ниво е да осигури цялост на информацията за валидността на подписа при дългосрочно съхранение на подписания файл.;
    /// </summary>
    public enum SigningLevels
    {
        /// <summary>
        /// Базово ниво на електронния подпис. Осигурява цялост на подписания документ и неотменимост на положения електронен подпис.
        /// </summary>
        BASELINE_B = 0,

        /// <summary>
        /// Към базовото ниво на подпис е добавено удостоверено време на подписване като доказателство, че подписа е съществувал към определен момент.
        /// </summary>
        BASELINE_T = 1,

        /// <summary>
        /// Към базовото ниво на подпис са добавени атрибути, осигуряващи валидността на подписа, чрез проверка единствено на подписания файл. 
        /// Целта на това ниво е да осигури информация за валидността на подписа при дългосрочно съхранение на подписания файл.
        /// </summary>
        BASELINE_LT = 2,

        /// <summary>
        /// Чрез добавяне на удостоверени времена позволява валидацията на подписа дълго време след създаването му. 
        /// Целта на това ниво е да осигури цялост на информацията за валидността на подписа при дългосрочно съхранение на подписания файл.
        /// </summary>
        BASELINE_LTA = 3,
    }

    /// <summary>
    /// Тип на пакетиране на е-подписа.
    /// 0 = Опакован подпис – подписът е поделемент в подписания документ. Проложим към формати PAdES, XAdES.;
    /// 1 = Опаковащ подпис – целия подписван обект се намира в рамките на подписа. Приложим към формати CAdES, XAdES.;
    /// 2 = Обособен подпис – подписът се намира в отделен файл. Приложим към формати CAdES, XAdES.;
    /// </summary>
    public enum SigningPackingTypes
    {
        /// <summary>
        /// Опакован подпис – подписът е поделемент в подписания документ. Проложим към формати PAdES, XAdES.
        /// </summary>
        ENVELOPED = 0,

        /// <summary>
        /// Опаковащ подпис – целия подписван обект се намира в рамките на подписа. Приложим към формати CAdES, XAdES.
        /// </summary>
        ENVELOPING = 1,

        /// <summary>
        /// Обособен подпис – подписът се намира в отделен файл. Приложим към формати CAdES, XAdES.
        /// </summary>
        DETACHED = 2,
    }

    /// <summary>
    /// Хеш алгоритам на е-подиса.
    /// 0 = SHA1;
    /// 1 = SHA224;
    /// 2 = SHA256;
    /// 3 = SHA384;
    /// 4 = SHA512;
    /// 5 = RIPEMD160;
    /// 6 = MD2;
    /// 7 = MD5;
    /// </summary>
    public enum DigestMethods
    {
        /// <summary>
        /// SHA1
        /// </summary>
        SHA1 = 0,

        /// <summary>
        /// SHA224
        /// </summary>
        SHA224 = 1,

        /// <summary>
        /// SHA256
        /// </summary>
        SHA256 = 2,

        /// <summary>
        /// SHA384
        /// </summary>
        SHA384 = 3,

        /// <summary>
        /// SHA512
        /// </summary>
        SHA512 = 4,

        /// <summary>
        /// RIPEMD160
        /// </summary>
        RIPEMD160 = 5,

        /// <summary>
        /// MD2
        /// </summary>
        MD2 = 6,

        /// <summary>
        /// MD5
        /// </summary>
        MD5 = 7,
    }
}
