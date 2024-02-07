export const Constants = {
    BG_LANG_ID: 1,
    E_DOCUMENT: 1,
    ATTACHABLE_DOCUMENT: 2,
    IDSRV_APP_PARAMETER: "GL_IDSRV_URL",
    PATHS: {
        SERVICES: '/services',
        APPLICATION_PROCESSES: '/services/applicationProcesses/:serviceID/:sectionCode?',
        APPLICATION_PROCESSES_START: '/services/applicationProcesses/:serviceID/start',
        KAT_OBLIGATIONS: '/services/obligations',
        PREVIEW_DOCUMENT_PROCESS_BY_FILE: '/services/PreviewDocument/',
        PREVIEW_DOCUMENT_PROCESS_BY_DOC_URI: '/services/:caseFileURI/PreviewDocument/:documentURI',
        DocumentPreview: '/services/PreviewDocument',
        ServiceInstances: '/my-eservices',
        ServiceInstance: '/my-eservices/:caseFileURI',
        ServiceInstanceDocumentPreview: '/my-eservices/:caseFileURI/documents/:documentURI',
        ServiceInstanceDocumentDownload: '/my-eservices/:caseFileURI/documents/:documentURI/download',
        NotAcknowledgedMessage: '/notAcknowledgedMessage/:notAcknowledgedMessageURI/documentProcessId/:documentProcessId'
    },
    DATE_FORMATS: {
        dateTime: "DD.MM.YYYY г. HH:mm:ss ч.",
        dateTimeHour: "DD.MM.YYYY г. HH ч.",
        dateTimeHourMinute: "DD.MM.YYYY г. HH:mm ч.",
        date8601: "yyyy-mm-dd",
        date: "DD.MM.YYYY г.",
        TimeHm: "HH:mm",
        TimeHms: "HH:mm:ss",
    },
    PAYMENT_METHOD_TYPE: {
        1: "GL_PAYMENT_METHOD_EPAY_L",
        2: "GL_PAYMENT_METHOD_PEPDAEU_L"
    },
    DECLARATIONS: {
        DISABILITIES:'DECL_DISABILITIES'
    }
}