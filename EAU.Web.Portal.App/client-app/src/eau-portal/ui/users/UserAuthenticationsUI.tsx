import { UrlHelper } from 'cnsys-core';
import { AsyncUIProps, ConfirmationModal, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { appConfig, Constants, resourceManager, UserAuthenticationInfo, ValidationSummaryErrors } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { UserService } from '../../services/UserService';

interface UserAuthenticationsProps extends AsyncUIProps {
}

const UserAuthenticationsImpl: React.FC<UserAuthenticationsProps> = (props) => {

    const [selectedAuthenticationType, setSelectedAuthenticationType] = useState<"KEP" | "eAuth">("KEP");
    const [certificates, setCertificates] = useState<UserAuthenticationInfo[]>([]);
    const [eAuths, setEAuths] = useState<UserAuthenticationInfo[]>([]);
    const userService = new UserService();

    useEffect(() => {
        props.registerAsyncOperation(Promise.all([loadUserCertificates(), loadUserEAuth()]))
    }, [])

    return <div className="page-wrapper" id="ARTICLE-CONTENT">
        <ValidationSummaryErrors asyncErrors={props.asyncErrors} />
        <ul className="nav nav-tabs">
            <li className="nav-item active">
                <button className={`nav-link ${selectedAuthenticationType == "KEP" ? "active" : ""}`} type="button"
                    onClick={() => setSelectedAuthenticationType("KEP")}>{resourceManager.getResourceByKey("GL_KEP_L")}</button>
            </li>
            <li className="nav-item">
                <button className={`nav-link ${selectedAuthenticationType == "eAuth" ? "active" : ""}`} type="button"
                    onClick={() => setSelectedAuthenticationType("eAuth")}>{resourceManager.getResourceByKey("GL_E_AUTH_L")}</button>
            </li>
        </ul>
        {
            selectedAuthenticationType == "KEP"
                ? <section>
                    <div className="alert alert-info"><RawHTML rawHtmlText={resourceManager.getResourceByKey("GL_USER_AUTH_KEP_INFO_I")} /></div>
                    <div className="button-bar button-bar--responsive button-bar--table-actionbar-top">
                        <div className="right-side">
                        </div>
                        <div className="left-side">
                            <button className="btn btn-light" onClick={addKEP}><i className="ui-icon ui-icon-plus mr-1" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_ADD_KEP_BTN_L")}</button>
                        </div>
                    </div>
                    {
                        certificates && certificates.length > 0
                            ? <div className="table-responsive-block">
                                <table className="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>{resourceManager.getResourceByKey("GL_ISSUER_L")}</th>
                                            <th>{resourceManager.getResourceByKey("GL_SERIAL_NUMBER_L")}</th>
                                            <th>{resourceManager.getResourceByKey("GL_EXPIRE_DATE_L")}</th>
                                            <th>{resourceManager.getResourceByKey("GL_HOLDER_INFO_L")}</th>
                                            <th className="text-right">{resourceManager.getResourceByKey("GL_ACTIONS_L")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            certificates.map((certificate) => {

                                                return <tr key={certificate.userAuthenticationId}>
                                                    <th>
                                                        <p className="th-title d-sm-none">{resourceManager.getResourceByKey("GL_ISSUER_L")}</p>
                                                        <p className="td-text">{certificate.issuer}</p>
                                                    </th>
                                                    <td>
                                                        <p className="th-title d-sm-none">{resourceManager.getResourceByKey("GL_SERIAL_NUMBER_L")}</p>
                                                        <p className="td-text">{certificate.serialNumber}</p>
                                                    </td>
                                                    <td>
                                                        <p className="th-title d-sm-none">{resourceManager.getResourceByKey("GL_EXPIRE_DATE_L")}</p>
                                                        <p className="td-text">{certificate.validTo.format(Constants.DATE_FORMATS.dateTime)}</p>
                                                    </td>
                                                    <td>
                                                        <p className="th-title d-sm-none">{resourceManager.getResourceByKey("GL_HOLDER_INFO_L")}</p>
                                                        <p className="td-text">{certificate.subject}</p>
                                                    </td>
                                                    <td className="actions-td">
                                                        <p className="th-title d-sm-none">{resourceManager.getResourceByKey("GL_ACTIONS_L")}</p>
                                                        <ConfirmationModal
                                                            modalTitleKey={"GL_DELETE_KEP_I"}
                                                            modalTextKeys={["GL_DEL_CONFIRM_KEP_I"]}
                                                            noTextKey="GL_CANCEL_L"
                                                            yesTextKey="GL_CONFIRM_L"
                                                            onSuccess={() => deleteEAtuh(certificate.userAuthenticationId, "certificate")}>
                                                            <button className="btn btn-link">{resourceManager.getResourceByKey("GL_DELETE_L")}</button>
                                                        </ConfirmationModal>
                                                    </td>
                                                </tr>
                                            })
                                        }
                                    </tbody>
                                </table>
                            </div>
                            : null
                    }
                </section>
                : <section>
                    <div className="alert alert-info"><p><RawHTML rawHtmlText={resourceManager.getResourceByKey("GL_USER_AUTH_E_AUTH_INFO_I")} /></p></div>
                    <div className="button-bar button-bar--responsive button-bar--table-actionbar-top">
                        <div className="right-side">
                        </div>
                        <div className="left-side">
                            {
                                eAuths && eAuths.length > 0
                                    ? <ConfirmationModal
                                        modalTitleKey={"GL_DELETE_E_AUTH_I"}
                                        modalTextKeys={["GL_DEL_CONFIRM_E_AUTH_I"]}
                                        noTextKey="GL_CANCEL_L"
                                        yesTextKey="GL_CONFIRM_L"
                                        onSuccess={() => deleteEAtuh(eAuths[0].userAuthenticationId, "eAuth")}>
                                        <button className="btn btn-light"><i className="ui-icon ui-icon-times mr-1" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_REMOVE_USER_AUTHENTICATION_L")}</button>
                                    </ConfirmationModal>
                                    : <button className="btn btn-light" onClick={addEAuth}><i className="ui-icon ui-icon-plus mr-1" aria-hidden="true"></i>{resourceManager.getResourceByKey("GL_ADD_USER_AUTHENTICATION_L")}</button>
                            }
                        </div>
                    </div>
                    {
                        eAuths && eAuths.length > 0
                            ? <div className="alert alert-info" role="alert"><p>{resourceManager.getResourceByKey("GL_ALREADY_HAVE_REGISTRATION_DEVICE_EAUTHENTICATION_I")}</p></div>
                            : null
                    }
                </section>
        }
    </div>

    function addKEP() {
        const returnUrl = encodeURIComponent(UrlHelper.urlJoin(window.location.origin, Constants.PATHS.SERVICES));

        window.location.href = `${appConfig.idsrvURL}/account/CertificateRegistrationBegin?returnUrl=${returnUrl}`;
    }

    function addEAuth() {
        const returnUrl = encodeURIComponent(UrlHelper.urlJoin(window.location.origin, Constants.PATHS.SERVICES));

        window.location.href = `${appConfig.idsrvURL}/saml/RegisterAuthentication?returnUrl=${returnUrl}`;
    }

    function deleteEAtuh(userAuthenticationId: number, eAuthType: "certificate" | "eAuth") {
        props.registerAsyncOperation(userService.deleteUserAuthentication(userAuthenticationId).then(() => {
            if (eAuthType == "certificate") {
                setCertificates(certificates.filter(cert => cert.userAuthenticationId != userAuthenticationId))
            } else if (eAuthType == "eAuth") {
                setEAuths(eAuths.filter(eAuth => eAuth.userAuthenticationId != userAuthenticationId))
            }
        }));
    }

    function loadUserCertificates() {
        userService.getCertificates().then((certificatesResult) => {
            setCertificates(certificatesResult)
        })
    }

    function loadUserEAuth() {
        userService.getEAuths().then((eAuths) => {
            setEAuths(eAuths)
        })
    }
}

export const UserAuthenticationsUI = withAsyncFrame(UserAuthenticationsImpl, false)