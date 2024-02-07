export const Constants = {
    PATHS: {
        Home: '/',

        Noms: '/nomenclatures',
        NomServices: '/nomenclatures/services',
        NomEditService: '/nomenclatures/services/:serviceID',
        NomAddService: '/nomenclatures/services/add',
        NomDeclarations: '/nomenclatures/declarations',
        NomEditDeclaration: '/nomenclatures/declarations/edit/:declarationID',
        NomAddDeclaration: '/nomenclatures/declarations/add',

        NomLabels: '/nomenclatures/labels',
        NomLanguages: '/nomenclatures/languages',

        NomServiceGroups: '/nomenclatures/serviceGroups',
        NomAddServiceGroup: '/nomenclatures/serviceGroups/add',
        NomEditServiceGroup: '/nomenclatures/serviceGroups/:serviceGroupID',

        AppParameters: '/appParameters',

        Content: '/content',
        Pages: '/content/pages',
        PagesEdit: '/content/pages/:pageID',

        DocumentTemplates: '/content/documentTemplates',
        EditDocumentTemplate: '/content/documentTemplates/edit/:documentTemplateID',
        AddDocumentTemplate: '/content/documentTemplates/add',

        Translations: '/translations',
        TranslationsServices: '/translations/services',
        TranslationsEditService: '/translations/services/:serviceID',
        TranslationsAddService: '/translations/services/add',
        TranslationsLabels: '/translations/labels',
        TranslationsLanguages: '/translations/languages',
        TranslationsServiceGroups: '/translations/serviceGroups',
        TranslationsEditServiceGroup: '/translations/serviceGroups/:serviceGroupID/:lang',
        TranslationsAddServiceGroup: '/translations/serviceGroups/add/:lang',

        TranslationsPages: '/translations/pages',
        TranslationsPagesTranslate: '/translations/pages/:pageID',

        Users: '/users',
        InternalUsersRegister: '/users/register',
        InternalUsersProfiles: '/users/profiles',
        InternalUsersProfileEdit: '/users/profiles/edit/:userId',
        Audit: '/users/audit',
        Limits: '/limits',

        payments: '/payments',
        paymentsEpay: '/payments/epay',
        paymentsPep: '/payments/pep',
        PaymentsEditPep: '/payments/pep/edit/:pepID',
        PaymentsAddPep: '/payments/pep/add',

        Reports: '/reports',
        ReportsPayments: '/reports/payments',
        ReportsNotary: '/reports/notary',

        SessionTimeout: '/session_timeout'
    },
    RESOURCES: {
        Home: 'GL_HOME_L',
        Noms: 'GL_NOMENCLATURES_L',
        Services: 'GL_SERVICES_L',
        EditService: 'GL_EDIT_SERVICES_L',
        AddService: 'GL_ADD_SERVICES_L',

        Declarations: 'GL_DECLARATION_LIST_L',
        EditDeclaration: 'GL_EDIT_DECLARATION_L',
        AddDeclaration: 'GL_ADD_DECLARATION_L',

        PredefinedPages: 'GL_PREDEFINED_PAGES_HTML_L',
        EditPredefinedPages: 'GL_PREDEFINED_PAGES_HTML_EDIT_L',
        AddPredefinedPages: 'GL_PREDEFINED_PAGES_HTML_ADD_L',

        Labels: 'GL_LABELS_L',
        EditLabels: 'GL_LABELS_L',
        Languages: "GL_LANGUAGES_L",

        ServiceGroups: 'GL_SERVICE_GROUPS_L',
        AddServiceGroup: "GL_ADD_SERVICE_GROUPS_L",
        EditServiceGroup: "GL_EDIT_SERVICE_GROUPS_L",

        DocumentTemplates: "GL_CONFIGURE_DECLARATION_TEMPLATES_L",
        EditDocumentTemplates: "GL_EDIT_DOCUMENT_TEMPLATES_L",
        AddDocumentTemplates: "GL_ADD_DOCUMENT_TEMPLATES_L",

        AppParameters: "GL_APP_PARAMETERS_L",
        Pages: "GL_PREDEFINED_PAGE_HTML_L",
        PagesEdit: "GL_PREDEFINED_PAGES_HTML_EDIT_L",

        Content: "GL_CONTENT_L",

        Translations: 'GL_TRANSLATIONS_L',
        TranslationsPages: "GL_I18N_PREDEFINED_PAGE_HTML_L",
        TranslationsPagesEdit: "GL_I18N_PREDEFINED_PAGES_HTML_EDIT_L",
        TranslationsServiceGroups: 'GL_I18N_SERVICE_GROUPS_L',
        TranslationsLabels: 'GL_I18N_LABELS_L',
        TranslationsServices: 'GL_TRANSLATION_SERVICES_L',

        Users: 'GL_USERS_L',
        InternalUsersRegister: 'GL_INTERNAL_USERS_REGISTER_L',
        InternalUsersProfiles: 'GL_INTERNAL_USERS_PROFILE_L',
        InternalUsersProfileEdit: 'GL_INTERNAL_USERS_PROFILE_EDIT_L',
        Audit: 'GL_AUDIT_VIEW_L',
        Limits: 'GL_LIMITS_L',

        Payments: 'GL_PAYMENTS_L',
        PaymentsEpay: 'GL_PAYMENTS_EPAY_L',
        PaymentsPep: 'GL_PAYMENTS_LIST_PEP_L',
        PaymentsAddEditPep: 'GL_PAYMENTS_PEP_L',

        Reports: 'GL_REPORTS_L',
        ReportsPayments: 'GL_REPORTS_PAYMENTS_L',
        ReportsNotary: 'GL_REPORTS_NOTARY_L',
    },
    PAYMENT_METHOD_TYPE: {
        1: "GL_EPAY_E",
        2: "GL_PEP_E"
    },
}